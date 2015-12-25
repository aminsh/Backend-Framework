using System;

namespace DevStorm.Infrastructure.Core.Domain
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}
