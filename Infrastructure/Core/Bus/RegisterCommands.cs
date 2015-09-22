using System;
using System.Configuration;
using System.Linq;
using System.Text;
using Core.Command;
using Newtonsoft.Json.Linq;
using RabbitMQ.Client.Events;
using Utility;

namespace Core.Bus
{
    public static class RegisterCommands
    {
        public static void Execute()
        {
            AppDomain.CurrentDomain.Load("Commands");

            var commandTypes = AppDomain.CurrentDomain.GetAssemblies()
                .First(a => a.GetName().Name == "Commands")
                .GetTypes().Where(t => t.GetInterfaces().Any(its => its == typeof(ICommand)));

            commandTypes.ForEach(Subscribe);
        }

        private static void Subscribe(Type commandType)
        {
            var commandName =  ConfigurationManager.AppSettings["ApplicationName"] + commandType.Name;
            var channel = RabbitMqFactory.Channel;

            channel.ExchangeDeclare(exchange: commandName, type: "fanout");
            var queueName = channel.QueueDeclare().QueueName;
            channel.QueueBind(queue: queueName, exchange: commandName, routingKey: "");


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
            var typeName = token["CommandName"].ToString();
            var current = ObjectExtention.Deseianlize<CurrentForBus>(token["Current"].ToString());
            
            AppDomain.CurrentDomain.Load("Commands");
            var type =
                AppDomain.CurrentDomain.GetAssemblies()
                    .First(a => a.GetName().Name == "Commands")
                    .GetTypes()
                    .First(t => t.Name == typeName);

            AppDomain.CurrentDomain.Load("CommandHandlers");

            var command = ObjectExtention.Deseianlize(token["Command"].ToString(), type).As<ICommand>();

            var commandType = command.GetType();

            var commnadHandlerType = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => a.GetName().Name == "CommandHandlers")
                .SelectMany(a => a.GetTypes())
                .Single(t => t.GetInterfaces().Any(ifc =>
                    ifc.IsGenericType &&
                    ifc.GetGenericTypeDefinition() == typeof(ICommandHandler<>) &&
                    ifc.GetGenericArguments().First() == commandType));

            var handleMethod =
                commnadHandlerType.GetMethods()
                    .Single(m =>
                        m.Name == "Handle" &&
                        m.GetParameters().First().ParameterType == commandType);
            var arguments =
                commnadHandlerType.GetConstructors()[0].GetParameters()
                    .Select(p => DependencyManager.Resolve(p.ParameterType)).ToArray();

            var instance = Activator.CreateInstance(commnadHandlerType, arguments);

            commnadHandlerType.GetProperty("Current").SetValue(instance, current);

            handleMethod.Invoke(instance, new object[] { command });
        }

    }
}
