using System;
using DevStorm.Infrastructure.Core.CQRS;

namespace DevStorm.Application.Commands
{
    public class CreateEmployeeCommand : ICommand
    {
        public Guid CommandId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
