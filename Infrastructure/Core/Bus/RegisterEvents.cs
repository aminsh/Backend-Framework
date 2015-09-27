using System;
using System.Linq;
using System.Text;
using Core.Command;
using Core.Event;
using Newtonsoft.Json.Linq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Utility;

namespace Core.Bus
{
    public static class RegisterEvents
    {
        public static void Execute()
        {
            AppDomain.CurrentDomain.Load("Events");

            var eventTypes = AppDomain.CurrentDomain.GetAssemblies()
                .First(a => a.GetName().Name == "Events")
                .GetTypes().Where(t => t.GetInterfaces().Any(its => its == typeof(IReceiveEvent)));

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

            AppDomain.CurrentDomain.Load("Events");
            var type =
                AppDomain.CurrentDomain.GetAssemblies()
                    .First(a => a.GetName().Name == "Events")
                    .GetTypes()
                    .First(t => t.Name == typeName);

            AppDomain.CurrentDomain.Load("EventHandlers");

            var @event = ObjectExtention.Deserialize(token["data"].ToString(), type).As<IReceiveEvent>();

            var eventType = @event.GetType();

            var eventHandlerType = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => a.GetName().Name == "EventHandlers")
                .SelectMany(a => a.GetTypes())
                .Single(t => t.GetInterfaces().Any(ifc =>
                    ifc.IsGenericType &&
                    ifc.GetGenericTypeDefinition() == typeof(IEventHandler<>) &&
                    ifc.GetGenericArguments().First() == eventType));

            var handleMethod =
                eventHandlerType.GetMethods()
                    .Single(m =>
                        m.Name == "Handle" &&
                        m.GetParameters().First().ParameterType == eventType);
            var arguments =
                eventHandlerType.GetConstructors()[0].GetParameters()
                    .Select(p => DependencyManager.Resolve(p.ParameterType)).ToArray();
            handleMethod.Invoke(Activator.CreateInstance(eventHandlerType, arguments), new object[] { @event });
        }

    }
}
