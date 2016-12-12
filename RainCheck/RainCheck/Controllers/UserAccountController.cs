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
            var policies = _context.policy_tbl.ToList();
            return View(policies);

        }
    }
}