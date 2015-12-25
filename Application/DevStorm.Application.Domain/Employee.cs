using System;
using DevStorm.Infrastructure.Core.Domain;

namespace DevStorm.Application.Domain
{
    public class Employee : IEntity, IRemovable
    {
        public Guid Id { get; set; }

        public string Code { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public bool IsDeleted { get; set; }

        public byte[] RowVersion { get; set; }
    }
}
