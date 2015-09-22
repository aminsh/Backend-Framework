using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;

namespace Hubs
{
    public class EmployeeHub : Hub
    {
        public override Task OnConnected()
        {
            return base.OnConnected();
        }

        public void EmployeeCreatedEvent(object data)
        {
            Clients.All.EmployeeCreatedEvent(data);
        }
    }
}