using System;
using System.Linq;
using System.Web.Security;
using DevStorm.Infrastructure.Core.Api;
using DevStorm.Infrastructure.Core.DataAccess;
using DevStorm.Infrastructure.Core.Security;

namespace DevStorm.Infrastructure.Security
{
    public class AccountService : IAccountService
    {
        private readonly IRepository<Account> _accountRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidationResult _validationResult;

        public AccountService(
            IRepository<Account> accountRepository, 
            IUnitOfWork unitOfWork, 
            IValidationResult validationResult)
        {
            _accountRepository = accountRepository;
            _unitOfWork = unitOfWork;
            _validationResult = validationResult;
        }

        public Account Register(string userName, string password, string name)
        {
            if (_accountRepository.Query().Any(u => u.UserName.ToLower() == userName.ToLower()))
                _validationResult.AddError("نام کاربری تکراری میباشد");

            if (!_validationResult.IsValid)
                return null;

            var encryptedPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5");
            var newAccount = new Account
            {
                Id = Guid.NewGuid(),
                Name = name,
                Password = encryptedPassword,
                UserName = userName
            };

            _accountRepository.Add(newAccount);
            _unitOfWork.Commit();

            return newAccount;
        }

        public void ChangePassword(int userId, string password)
        {
            var user = _accountRepository.FindById(userId);
            var encryptedPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5");

            user.Password = encryptedPassword;

            _unitOfWork.Commit();
        }
    }
}
