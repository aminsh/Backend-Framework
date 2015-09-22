using System;

namespace Core.Security.Contracts
{
    public interface IAuthentication
    {
        void Authenticate(Guid token);
    }
}
