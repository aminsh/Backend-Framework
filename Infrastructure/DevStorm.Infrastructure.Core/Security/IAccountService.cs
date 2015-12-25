using DevStorm.Infrastructure.Core.Api;

namespace DevStorm.Infrastructure.Core.Security
{
    public interface IAccountService
    {
        Account Register(string userName, string password, string name);
        void ChangePassword(int userId, string password);
    }
}
