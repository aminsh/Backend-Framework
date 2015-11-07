using System;
using Core.Domain.Contract;

namespace Domain
{
    public class Employee : IEntity, IRemovable
    {
        public Guid Id { get; set; }

        public string Code { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public bool IsDeleted { get; set; }
    }
}
