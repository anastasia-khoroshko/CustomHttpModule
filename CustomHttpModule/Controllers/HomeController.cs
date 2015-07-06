using CustomHttpModule.Models;
using CustomHttpModule.ORM;
using CustomHttpModule.ServiceLogic;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;

namespace CustomHttpModule.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserLoginModel model, string returnUrl)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    HttpContext.Handler = new ServiceLogic.Handlers.LoginHandler();
                    HttpContext.Handler.ProcessRequest(System.Web.HttpContext.Current);
                    if (System.Web.HttpContext.Current.Response.StatusCode==200)
                    {
                        if (Url.IsLocalUrl(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
                ModelState.AddModelError("", "Wrong login or password");
                return View(model);
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        public ActionResult LogOff()
        {
            HttpContext.Handler = new ServiceLogic.Handlers.LogOffHandler();
            HttpContext.Handler.ProcessRequest(System.Web.HttpContext.Current);
            if (System.Web.HttpContext.Current.Response.StatusCode == 200)
                return RedirectToAction("Index", "Home");
            else return View("Error");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserRegisterModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    HttpContext.Handler = new ServiceLogic.Handlers.RegisterHandler();
                    HttpContext.Handler.ProcessRequest(System.Web.HttpContext.Current);
                    if (System.Web.HttpContext.Current.Response.StatusCode == 200)
                    {
                        return RedirectToAction("Login", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "User with this email already exists");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Wrong email or password");
                }
                return View(model);
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }
    }
}
