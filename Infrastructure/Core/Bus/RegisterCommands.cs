using System;
using System.Configuration;
using System.Linq;
using System.Text;
using Core.Command;
using Core.SingnalR;
using Microsoft.AspNet.SignalR;
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

                var validationResult = DependencyManager.Resolve<ISendCommandToValidator>().Validate(commandMessage);
                if (validationResult.IsValid)
                {
                    var returnValue = DependencyManager.Resolve<ISendCommandToHandler>().Handle(commandMessage);

                    GlobalHost.ConnectionManager.GetHubContext<CommandHub>()
                        .Clients.User(commandMessage.Current.UserId.ToString()).CommittedCommand(new
                        {
                            command = new {id = commandMessage.Command.CommandId},
                            returnValue = returnValue,
                            validationResult = new {isValid = true}
                        });
                }
                else
                {
                    GlobalHost.ConnectionManager.GetHubContext<CommandHub>()
                        .Clients.User(commandMessage.Current.UserId.ToString()).FailedCommand(new
                        {
                            command = new {id = commandMessage.Command.CommandId},
                            validationResult = validationResult.ToDto()
                        });
                }
            };

            channel.BasicConsume(queue: queueName, noAck: true, consumer: consumer);
        }
    }
}