using System;
using Domain;

namespace Core.Security.Models
{
    public class AuthenticationToken
    {
        public Guid Token { get; set; }
        public User User { get; set; }
    }
}
