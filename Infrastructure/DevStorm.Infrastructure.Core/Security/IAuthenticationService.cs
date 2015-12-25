using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevStorm.Infrastructure.Core.Security
{
    public interface IAuthenticationService
    {
        Guid Authenticate(string userName, string password);
        void SignOut();
    }
}
