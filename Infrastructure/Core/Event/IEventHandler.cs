namespace Core.Event
{
    public interface IEventHandler<in TEvent> where TEvent : IReceiveEvent
    {
        void Handle(TEvent @event);
    }
}
