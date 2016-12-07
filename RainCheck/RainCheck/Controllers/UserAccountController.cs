using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RainCheck.Controllers
{
    public class UserAccountController : Controller
    {
        // GET: UserAccount
        public ActionResult UserMain()
        {
            Models.RainCheckConnectionString obj = new Models.RainCheckConnectionString();
            return View();

        }
    }
}