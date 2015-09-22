using System;
using Core.Command;

namespace Commands
{
    public class CreateEmployeeCommand : ICommand
    {
        public Guid CommandId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
