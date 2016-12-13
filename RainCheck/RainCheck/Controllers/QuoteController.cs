using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RainCheck.Controllers
{
    public class QuoteController : Controller
    {
        // GET: Quote
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetQuote()
        {
            return View();
        }

        public ActionResult QuoteResult()
        {
            return View();
        }
    }
}