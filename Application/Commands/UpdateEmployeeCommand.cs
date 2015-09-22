using System;
using Core.Command;

namespace Commands
{
    public class UpdateEmployeeCommand : ICommand
    {
        public Guid CommandId { get; set; }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
