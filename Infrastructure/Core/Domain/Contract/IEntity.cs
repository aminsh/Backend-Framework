using System;

namespace Core.Domain.Contract
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}
