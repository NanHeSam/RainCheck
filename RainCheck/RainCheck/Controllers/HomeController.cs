using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RainCheck.Models;

namespace RainCheck.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult getQuote()
        {
            // when validation finished
            TempData["zipcode"] = Request.Form["Zipcode"];

            return RedirectToAction("../Quote/GetQuote");
        }

        public ActionResult getSavedQuotePage()
        {
            return View("getSavedQuote");
        }

        public ActionResult getSavedQuote()
        {
            //check if saved quote exists
            string referenceNum = Request.Form["Reference_number"];
            RainCheckServerEntities entity = new RainCheckServerEntities();
            List<quote> targets = entity.quotes.Where(x => x.Reference_number.Equals(referenceNum)).ToList();

            if (targets.Count() == 0)
            {
                TempData["Message"] = "No such user";
                return RedirectToAction("getSavedQuotePage");
            }
            else
            {
                TempData["quoteId"] = targets[0].quote_id;
                return RedirectToAction("../Quote/QuoteResult");

            }
            
            
        }
    }
}