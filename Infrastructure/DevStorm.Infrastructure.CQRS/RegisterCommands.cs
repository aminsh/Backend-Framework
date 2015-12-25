using System;
using System.Configuration;
using System.Linq;
using System.Text;
using DevStorm.Infrastructure.Core.CQRS;
using DevStorm.Infrastructure.Core.IOC;
using DevStorm.Infrastructure.Utility;
using RabbitMQ.Client.Events;

namespace DevStorm.Infrastructure.CQRS
{
    public static class RegisterCommands
    {
        public static void Execute()
        {
            var commandTypes = TypeService.GetTypes(e => e.GetInterfaces().Any(it => it == typeof (ICommand)));
                
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

                    //GlobalHost.ConnectionManager.GetHubContext<CommandHub>()
                    //    .Clients.User(commandMessage.Current.UserId.ToString()).CommittedCommand(new
                    //    {
                    //        command = new {id = commandMessage.Command.CommandId},
                    //        returnValue = returnValue,
                    //        validationResult = new {isValid = true}
                    //    });
                }
                else
                {
                    //GlobalHost.ConnectionManager.GetHubContext<CommandHub>()
                    //    .Clients.User(commandMessage.Current.UserId.ToString()).FailedCommand(new
                    //    {
                    //        command = new {id = commandMessage.Command.CommandId},
                    //        validationResult = validationResult.ToDto()
                    //    });
                }
            };

            channel.BasicConsume(queue: queueName, noAck: true, consumer: consumer);
        }
    }
}