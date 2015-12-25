using System;

namespace DevStorm.Infrastructure.Core.CQRS
{
    public interface IEvent
    {
        Guid EventId { get; set; }
    }
}
