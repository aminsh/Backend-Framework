using Core.Event;

namespace Core.Bus
{
    public interface IEventBus
    {
        void SendToBusiness<TEvent>(TEvent @event) where TEvent : ISendEvent;
    }
}
