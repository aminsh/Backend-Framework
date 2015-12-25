using System;

namespace DevStorm.Infrastructure.Core.CQRS
{
    public interface ICommand
    {
        Guid CommandId { get; set; }
    }
}
