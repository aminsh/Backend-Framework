using RabbitMQ.Client;

namespace DevStorm.Infrastructure.CQRS
{
    public static class  RabbitMqFactory
    {
        static RabbitMqFactory()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            Connection = factory.CreateConnection();
            Channel = Connection.CreateModel();
        }

        public static IModel Channel { get; set; }
        public static IConnection Connection { get; set; }

    }
}
