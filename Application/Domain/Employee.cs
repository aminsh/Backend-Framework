using System;
using Core.Domain.Contract;

namespace Domain
{
    public class Employee : IEntity
    {
        public Guid Id { get; set; }

        public string Code { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
