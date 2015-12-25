using System;
using DevStorm.Infrastructure.Core.Security;

namespace DevStorm.Infrastructure.Security
{
    public class AuthenticationToken
    {
        public Guid Token { get; set; }
        public Account Account { get; set; }
    }
}
