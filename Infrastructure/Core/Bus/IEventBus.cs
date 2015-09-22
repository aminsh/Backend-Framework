using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace Core.Event
{
    public interface IEventBus
    {
        void SendToBussiness<TEvent>(TEvent @event) where TEvent : ISendEvent;
        void SendToClient<TEvent>(TEvent @event) where TEvent : B2CEvent;
        IHubConnectionContext<dynamic> HubContext<THub>() where THub : Hub;
    }
}
