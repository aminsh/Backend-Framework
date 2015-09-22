using System;

namespace Core.Command
{
    public interface ICommand
    {
        Guid CommandId { get; set; }
    }
}
