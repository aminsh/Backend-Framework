using System;
using System.Linq;
using System.Text;
using DevStorm.Infrastructure.Core;
using DevStorm.Infrastructure.Core.CQRS;
using DevStorm.Infrastructure.Core.IOC;
using DevStorm.Infrastructure.Utility;
using Newtonsoft.Json.Linq;
using RabbitMQ.Client.Events;

namespace DevStorm.Infrastructure.CQRS
{
    public static class RegisterEvents
    {
        public static void Execute()
        {
            var eventTypes = TypeService.GetTypes(t => t.GetInterfaces().Any(its => its == typeof (IReceiveEvent)));

            eventTypes.ForEach(Subscribe);
        }

        private static void Subscribe(Type eventType)
        {
            var eventName = eventType.Name;
            var channel = RabbitMqFactory.Channel;

            channel.ExchangeDeclare(exchange: eventName, type: "fanout");
            var queueName = channel.QueueDeclare().QueueName;
            channel.QueueBind(queue: queueName, exchange: eventName, routingKey: "");


            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body);
                Handle(message);
            };

            channel.BasicConsume(queue: queueName, noAck: true, consumer: consumer);
        }

        private static void Handle(string message)
        {
            var token = JObject.Parse(message);
            var typeName = token["eventName"].ToString();

            var type = TypeService.GetType(e => e.Name == typeName);

            var @event = ObjectExtention.Deserialize(token["data"].ToString(), type).As<IReceiveEvent>();

            var eventType = @event.GetType();

            var eventHandlerType = TypeService.GetType(t => t.GetInterfaces().Any(ifc =>
                ifc.IsGenericType &&
                ifc.GetGenericTypeDefinition() == typeof (IEventHandler<>) &&
                ifc.GetGenericArguments().First() == eventType));

            var handleMethod =
                eventHandlerType.GetMethods()
                    .Single(m =>
                        m.Name == "Handle" &&
                        m.GetParameters().First().ParameterType == eventType);
            var arguments =
                eventHandlerType.GetConstructors()[0].GetParameters()
                    .Select(p => DependencyManager.Resolve(p.ParameterType)).ToArray();
            handleMethod.Invoke(Activator.CreateInstance(eventHandlerType, arguments), new object[] {@event});
        }
    }
}