﻿using System.Text;
using DevStorm.Infrastructure.Core.CQRS;
using DevStorm.Infrastructure.Utility;
using RabbitMQ.Client;

namespace DevStorm.Infrastructure.CQRS
{
    public class EventBus : IEventBus
    {
     
        public void SendToBusiness<TEvent>(TEvent @event) where TEvent : ISendEvent
        {
            var eventName = @event.GetType().Name;
            var message = new { eventName = eventName, data = @event.Serialize() }.Serialize();

            var factory = new ConnectionFactory { HostName = "localhost" };

            using (RabbitMQ.Client.IConnection connection = factory.CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare(eventName, "fanout");
                    byte[] pl = Encoding.UTF8.GetBytes(message);
                    channel.BasicPublish(eventName, "", null, pl);
                }
            }    
        }
    }
}
