using System;
using DevStorm.Application.Domain;
using DevStorm.Infrastructure.Core.Domain;

namespace Domain
{
    public class User : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Employee Employee { get; set; }
    }
}