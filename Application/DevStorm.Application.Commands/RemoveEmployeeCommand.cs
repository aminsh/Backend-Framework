using System;
using DevStorm.Infrastructure.Core.CQRS;

namespace DevStorm.Application.Commands
{
    public class RemoveEmployeeCommand : ICommand
    {
        public Guid CommandId { get; set; }

        public int Id { get; set; }
    }
}
