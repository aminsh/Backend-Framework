using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Security;
using Core.DataAccess;
using Core.Security.Models;
using DataAccess;
using Domain;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace Security.Services
{
    public class AuthenticationService
    {
        private static List<AuthenticationToken> Tokens { get; set; }
        private readonly IRepository<User> _userRepository;
        static AuthenticationService()
        {
            Tokens = new List<AuthenticationToken>();
        }

        public AuthenticationService(
            IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public Guid Authenticate(string username, string password)
        {
            var encryptedPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5");
            var user = _userRepository.Find(u => u.UserName == username && u.Password == encryptedPassword);

            if (user == null)
                return Guid.Empty;

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.Id.ToString()),
                new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier",user.Id.ToString(),"http://www.w3.org/2001/XMLSchema#string"),
                new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "ASP.NET Identity", "http://www.w3.org/2001/XMLSchema#string")
            };

            var claimsIndetity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

            var authentication = HttpContext.Current.GetOwinContext().Authentication;
            authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            authentication.SignIn(new AuthenticationProperties { IsPersistent = false }, claimsIndetity);

            HttpContext.Current.User = new ClaimsPrincipal(authentication.AuthenticationResponseGrant.Principal);

            Tokens.Add(new AuthenticationToken { Token = user.Id, User = user });
            return user.Id;
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
            //var token = Tokens.FirstOrDefault(t => t.User.Id == userId);

            //if(token == null) return;

            //Tokens.Remove(token);
        }
    }
}
