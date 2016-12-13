using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RainCheck.Models;
using System.Data.Entity;

namespace RainCheck.Controllers
{
    public class UserAccountController : Controller
    {
        private RainCheckConnectionString _context;
        public UserAccountController()
        {
            _context = new RainCheckConnectionString();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: UserAccount
        public ActionResult UserMain()
        {
            Session["logged_username"] = "Sam";
            var policies = new policyVM(1);
            return View(policies);

        }
        
        public ActionResult submit()
        {
            List<coverage_level> coverage = _context.coverage_level.ToList<coverage_level>();
            return View("ChangePolicy", coverage);
        }

        [HttpPost]
        public ActionResult saveQuote(string save, string cancel)
        {
            if (!string.IsNullOrEmpty(save))
            {
                //Request.Form[""];
                return RedirectToAction("UserMain");
            } else if (!string.IsNullOrEmpty(cancel))
            {
                return View();
            }
            return View();

        }


    }
}