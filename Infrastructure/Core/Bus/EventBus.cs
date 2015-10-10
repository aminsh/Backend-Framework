using System.Text;
using Core.Event;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using RabbitMQ.Client;
using Utility;

namespace Core.Bus
{
    public class EventBus : IEventBus
    {
     
        public void SendToBussiness<TEvent>(TEvent @event) where TEvent : ISendEvent
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
