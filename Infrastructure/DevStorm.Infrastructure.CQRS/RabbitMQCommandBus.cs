﻿using System.Configuration;
using System.Text;
using DevStorm.Infrastructure.Core.CQRS;
using DevStorm.Infrastructure.Core.Domain;
using DevStorm.Infrastructure.Core.IOC;
using DevStorm.Infrastructure.Utility;
using RabbitMQ.Client;

namespace DevStorm.Infrastructure.CQRS
{
    public class RabbitMQCommandBus : ICommandBus
    {
        public void Send<TCommand>(TCommand cmd) where TCommand : ICommand
        {
            var commandName = cmd.GetType().Name;
            var exchangeName = ConfigurationManager.AppSettings["ApplicationName"] + commandName;

            var message = new CommnadMessage(cmd , commandName , DependencyManager.Resolve<ICurrent>()).Serialize();

            var factory = new ConnectionFactory { HostName = "localhost" };

            using (IConnection connection = factory.CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(exchangeName, "fanout");
                    byte[] pl = Encoding.UTF8.GetBytes(message);
                    channel.BasicPublish(exchangeName, "", null, pl);
                }
            }              
        }
    }
}
