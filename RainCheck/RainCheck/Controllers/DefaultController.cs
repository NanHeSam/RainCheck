using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using RainCheck.Models;

namespace RainCheck.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        public ActionResult Index() 
        {
            Models.RainCheckConnectionString obj = new Models.RainCheckConnectionString();
            List<state> stateObject = obj.states.ToList<state>();
            return View(stateObject);
        }
    }
}