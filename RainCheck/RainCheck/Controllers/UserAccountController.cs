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
            var policies = new policyVM(1);
            if (!string.IsNullOrEmpty(save))
            {
                var policy = _context.policy_tbl.Where(o => o.user_id == 1).OrderByDescending(o => o.start_date).First();
                
                policy.self_body = System.Convert.ToDecimal(Request.Form["Self_body"]);
                policy.self_property = System.Convert.ToDecimal(Request.Form["Self_property"]);
                policy.opposite_body = System.Convert.ToDecimal(Request.Form["op_body"]);
                policy.opposite_property = System.Convert.ToDecimal(Request.Form["op_property"]);
                _context.SaveChanges();
                return RedirectToAction("UserMain", policies);
            } else if (!string.IsNullOrEmpty(cancel))
            {
                return View("UserMain", policies);
            }
            return View("submit");

        }


    }
}