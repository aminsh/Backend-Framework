using System;

namespace Core.Event
{
    public interface IEvent
    {
        Guid EventId { get; set; }
    }
}
