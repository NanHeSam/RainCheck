using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RainCheck.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult LoginPage()
        {
            return View();
        }
        public ActionResult CheckLogin()
        {
            return View("LoginPage");
        }


        public ActionResult ForgetUsername() { return View(); }
        public ActionResult SendEmailResetPassword() { return View(); }

        public ActionResult ForgetPassword() { return View(); }
        public ActionResult ResetPassword() { return View(); }
    }
}