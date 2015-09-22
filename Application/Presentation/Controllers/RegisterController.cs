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
    public class RegisterController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult RegisterByName()
        {
            return View();
        }

        public ActionResult RegisterByEmployee()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegisterByName(UserRegisterByName model)
        {
            if (ModelState.IsValid)
            {
                DependencyManager.Resolve<UserService>().RegisterByName(model.UserName, model.Password,model.Name);
                return RedirectToAction("Index", "Login");
            }

            return View(model);
        }
        [HttpPost]
        public ActionResult RegisterByEmployee(UserRegisterByEmployee model)
        {
            if (ModelState.IsValid)
            {
                DependencyManager.Resolve<UserService>()
                    .ReqisterByEmployee(model.UserName, model.Password, model.EmployeeId);
                return RedirectToAction("Index", "Login");
            }

            return View(model);
        }
    }

    
}