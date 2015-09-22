using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core;
using Core.ApiResult;
using Security.Services;

namespace Presentation.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Index(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        public JsonResult Login(UserLogin model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var token = DependencyManager.Resolve<AuthenticationService>()
                    .Authenticate(model.UserName, model.Password);
                if (token != Guid.Empty)
                    return Json(new {success = true});
                return Json(new
                {
                    success = false,
                    errors = new List<object>
                    {
                        new {propertyName = "", errors = new List<string> {"نام کاربری یا کلمه عبور صحیح نیست"}}
                    }
                });
            }
            var errors =
                ModelState.ToList()
                .Where(m => m.Value.Errors.Any())
                    .Select(m => new
                    {
                        propertyName = m.Key,
                        errors = m.Value.Errors.Select(err => err.ErrorMessage).ToList()
                    }).ToList();
            return Json(new { success = false, errors = errors });
        }

        public ActionResult RegisterByEmployee(UserRegisterByEmployee model)
        {
            if (ModelState.IsValid)
            {
                DependencyManager.Resolve<UserService>()
                  .ReqisterByEmployee(model.UserName, model.Password, model.EmployeeId);
                return Json(new
                {
                    success = true
                });
            }
            var errors =
                ModelState.ToList()
                .Where(m=> m.Value.Errors.Any())
                    .Select(m => new
                    {
                        propertyName = m.Key, 
                        errors = m.Value.Errors.Select(err => err.ErrorMessage).ToList()
                    }).ToList();
            return Json(new {success = false , errors = errors});
        }

        public ActionResult RegisterByName(UserRegisterByName model)
        {
            if (ModelState.IsValid)
            {
                DependencyManager.Resolve<UserService>()
                  .RegisterByName(model.UserName, model.Password,model.Name);
                return Json(new
                {
                    success = true,
                    errors = new List<object>
                    {
                        new
                        {
                            propertyName = "",
                            errors = (List<string>) DependencyManager.Resolve<IValidationResult>().Errors.Select(err => err.Message)
                        }
                    }
                });
            }
            var errors =
                ModelState.ToList()
                .Where(m => m.Value.Errors.Any())
                    .Select(m => new
                    {
                        propertyName = m.Key,
                        errors = m.Value.Errors.Select(err => err.ErrorMessage).ToList()
                    }).ToList();
            return Json(new { success = false, errors = errors });
        }

        public ActionResult SignOut()
        {
            DependencyManager.Resolve<AuthenticationService>().SignOut();
            return RedirectToAction("Index");
        }
	}

    public class UserLogin
    {
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "نام کاربری را وارد کنید")]
        public string UserName { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "کلمه عبور را وارد کنید")]
        public string Password { get; set; }
    }

    public class UserRegisterByName
    {
        [Display(Name = "نام و نام خانوادگی")]
        [Required(ErrorMessage = "نام و نام خانوادگی اجباری است")]
        public string Name { get; set; }

        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "نام کاربری اجباری است")]
        public string UserName { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "کلمه عبور اجباری است")]
        [StringLength(100, ErrorMessage = "کلمه عبور باید بین {0} تا {1} کاراکتر باشد", MinimumLength = 6)]
        public string Password { get; set; }

        [Display(Name = "تایید کلمه عبور")]
        [Required(ErrorMessage = "تایید کلمه عبور اجباری است")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "کلمه عبور و تایید کلمه عبور برابر نیست")]
        public string ConfirmPassword { get; set; }
    }

    public class UserRegisterByEmployee
    {
        [Display(Name = "نام پرسنلی")]
        [Required(ErrorMessage = "نام پرسنلی اجباری است")]
        public Guid EmployeeId { get; set; }

        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "نام کاربری اجباری است")]
        public string UserName { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "کلمه عبور اجباری است")]
        [StringLength(100, ErrorMessage = "کلمه عبور باید بین {0} تا {1} کاراکتر باشد", MinimumLength = 6)]
        public string Password { get; set; }

        [Display(Name = "تایید کلمه عبور")]
        [Required(ErrorMessage = "تایید کلمه عبور اجباری است")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "کلمه عبور و تایید کلمه عبور برابر نیست")]
        public string ConfirmPassword { get; set; }
    }
}