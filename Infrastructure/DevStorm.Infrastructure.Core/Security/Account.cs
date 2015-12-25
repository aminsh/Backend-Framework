using System;
using DevStorm.Infrastructure.Core.Domain;

namespace DevStorm.Infrastructure.Core.Security
{
    public class Account : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
