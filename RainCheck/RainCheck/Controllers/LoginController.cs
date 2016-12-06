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

        public ActionResult LoginSubmit()
        {
            var username = Request.Form["Username"];
            var password = Request.Form["Password"];
            return View("LoginPage");
        }


        public ActionResult ForgetUsername()
        {
            return View();
        }

        public ActionResult DisplayUsername()
        {
            var email = Request.Form["Email"];
            var policyNum = Request.Form["Policy"];
            // if inputed information is incorrect
            // return View("ForgetUsername");

            // else
            return View();
        }        

        public ActionResult ForgetPassword()
        {
            return View();
        }
        public ActionResult SendEmailResetPassword()
        {
            var username = Request.Form["Username"];
            var policyNum = Request.Form["Policy"];
            // if inputed information is incorrect
            // return View("ForgetPassword");

            // else
            // email verification code to user email address and then
            // display reset password form, with verification code.
            return View("EnterVerifyCode");
        }
        public ActionResult verifyCode() {
            if (Convert.ToInt32(Request.Form["VerificationCode"]) == 0)
                return View("EnterVerifyCode");
            else
                return View("ResetPassword");
        }
        public ActionResult ResetPassword()
        {
            return View("ResetPasswordResult");
        }
    }
}