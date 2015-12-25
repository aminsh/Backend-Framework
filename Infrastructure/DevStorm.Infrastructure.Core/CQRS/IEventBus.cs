namespace DevStorm.Infrastructure.Core.CQRS
{
    public interface IEventBus
    {
        void SendToBusiness<TEvent>(TEvent @event) where TEvent : ISendEvent;
    }
}
