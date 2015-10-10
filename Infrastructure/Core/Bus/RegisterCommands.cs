using System;
using System.Configuration;
using System.Linq;
using System.Text;
using Core.ApiResult;
using Core.Command;
using Core.Domain;
using Microsoft.AspNet.SignalR;
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
                .GetTypes().Where(t => t.GetInterfaces().Any(its => its == typeof (ICommand)));

            commandTypes.ForEach(Subscribe);
        }

        private static void Subscribe(Type commandType)
        {
            var commandName = ConfigurationManager.AppSettings["ApplicationName"] + commandType.Name;
            var channel = RabbitMqFactory.Channel;

            channel.ExchangeDeclare(exchange: commandName, type: "fanout");
            var queueName = channel.QueueDeclare().QueueName;
            channel.QueueBind(queue: queueName, exchange: commandName, routingKey: "");


            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body);
                var commandMessage = CommnadMessage.Deserialize(message);

                var validationResult = Validate(commandMessage);
                if (validationResult.IsValid)
                {
                    Handle(commandMessage);
                }
                else
                    ; //GlobalHost.ConnectionManager.GetHubContext("").Clients.User("")[""](new {});
            };

            channel.BasicConsume(queue: queueName, noAck: true, consumer: consumer);
        }

        private static IValidationResult Validate(CommnadMessage message)
        {
            AppDomain.CurrentDomain.Load("CommandValidation");
            var commandType = message.Command.GetType();

            var commnadValidationType = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => a.GetName().Name == "CommandValidation")
                .SelectMany(a => a.GetTypes())
                .SingleOrDefault(t => t.GetInterfaces().Any(ifc =>
                    ifc.IsGenericType &&
                    ifc.GetGenericTypeDefinition() == typeof (ICommandValidator<>) &&
                    ifc.GetGenericArguments().First() == commandType));

            if (commnadValidationType == null)
                return new ValidationResult();

            var validatorMethod =
                commnadValidationType.GetMethods()
                    .Single(m =>
                        m.Name == "Validate" &&
                        m.GetParameters().First().ParameterType == commandType);

            var arguments =
                commnadValidationType.GetConstructors()[0].GetParameters()
                    .Select(p => DependencyManager.Resolve(p.ParameterType)).ToArray();

            var instance = Activator.CreateInstance(commnadValidationType, arguments);
            commnadValidationType.GetProperty("Current").SetValue(instance, message.Current);

            validatorMethod.Invoke(instance, new object[] {message.Command});

            return instance.As<DomainValidator>().ValidationResult;
        }

        private static object Handle(CommnadMessage message)
        {
            var command = message.Command;
            var current = message.Current;

            AppDomain.CurrentDomain.Load("CommandHandlers");

            var commandType = command.GetType();

            var commnadHandlerType = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => a.GetName().Name == "CommandHandlers")
                .SelectMany(a => a.GetTypes())
                .Single(t => t.GetInterfaces().Any(ifc =>
                    ifc.IsGenericType &&
                    ifc.GetGenericTypeDefinition() == typeof (ICommandHandler<>) &&
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

            handleMethod.Invoke(instance, new object[] {command});

            return instance.As<DomainService>().ReturnValue;
        }
    }
}