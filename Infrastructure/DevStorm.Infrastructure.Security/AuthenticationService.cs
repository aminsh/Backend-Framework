using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Security;
using DevStorm.Infrastructure.Core.DataAccess;
using DevStorm.Infrastructure.Core.Security;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace DevStorm.Infrastructure.Security
{
    public class AuthenticationService : IAuthenticationService
    {
        private static List<AuthenticationToken> Tokens { get; set; }
        private readonly IRepository<Account> _accountRepository;

        static AuthenticationService()
        {
            Tokens = new List<AuthenticationToken>();
        }

        public AuthenticationService(
            IRepository<Account> accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public Guid Authenticate(string username, string password)
        {
            var encryptedPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5");
            var account = _accountRepository.Find(u => u.UserName == username && u.Password == encryptedPassword);

            if (account == null)
                return Guid.Empty;

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,account.Id.ToString()),
                new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier",account.Id.ToString(),"http://www.w3.org/2001/XMLSchema#string"),
                new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "ASP.NET Identity", "http://www.w3.org/2001/XMLSchema#string")
            };

            var claimsIndetity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

            var authentication = HttpContext.Current.GetOwinContext().Authentication;
            authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            authentication.SignIn(new AuthenticationProperties { IsPersistent = false }, claimsIndetity);

            HttpContext.Current.User = new ClaimsPrincipal(authentication.AuthenticationResponseGrant.Principal);

            Tokens.Add(new AuthenticationToken { Token = account.Id, Account = account });
            return account.Id;
        }

        public void SignOut()
        {
            HttpContext.Current.GetOwinContext().Authentication.SignOut();
        }
        public bool IsTokenValid(Guid token)
        {
            return Tokens.Any(t => t.Token == token);
        }

        public void RemoveToken(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
