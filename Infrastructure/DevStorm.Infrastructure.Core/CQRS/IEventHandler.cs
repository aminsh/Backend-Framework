namespace DevStorm.Infrastructure.Core.CQRS
{
    public interface IEventHandler<in TEvent> where TEvent : IReceiveEvent
    {
        void Handle(TEvent @event);
    }
}
