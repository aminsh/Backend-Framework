using System;
using Core.Command;

namespace Commands
{
    public class RemoveEmployeeCommand : ICommand
    {
        public Guid CommandId { get; set; }

        public int Id { get; set; }
    }
}
