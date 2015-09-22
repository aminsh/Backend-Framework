using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core;
using Core.ApiResult;
using Security.Services;

namespace Presentation.Controllers
{
    public class LoginController : Controller
    {

        private readonly AuthenticationService _authenticationService;
        private readonly IValidationResult _validationResult;

        public LoginController()
        {
            _authenticationService = DependencyManager.Resolve<AuthenticationService>();
            _validationResult = DependencyManager.Resolve<IValidationResult>();

        }
        public ActionResult Index(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        public ActionResult Index(UserLogin model, string returnUrl)
        {
            _authenticationService.Authenticate(model.UserName, model.Password);
            if (_validationResult.IsValid)
                return Redirect(returnUrl);

            ModelState.AddModelError("", "نام کاربری یا کلمه عبور صحیح نیست");

            return View(model);
        }
    }

   
}