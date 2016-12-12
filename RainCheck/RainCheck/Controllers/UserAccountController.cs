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
            var policies = from e in _context.policy_tbl
                           group e by e.policy_number into groups
                           select groups.OrderBy(p => p.start_date).FirstOrDefault();

            return View(policies);

        }
    }
}