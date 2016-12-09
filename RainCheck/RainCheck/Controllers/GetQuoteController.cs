using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RainCheck.Models;

namespace RainCheck.Controllers
{
    public class GetQuoteController : Controller
    {
        // GET: GetQuote
        public ActionResult Get()
        {

            GetQuoteDAL modelObj = new GetQuoteDAL();
            modelObj.Getmake("volvo");
            return View("GetQuote",modelObj);
        }
            [HttpPost]
            public JsonResult MyAjaxCall(string value)
            {
            GetQuoteDAL modelObject = new GetQuoteDAL();
               // modelObject.Getmake(value);
                return Json(modelObject.Getmake(value));
             }



        public ActionResult Submit()
                 {
            
            GetQuoteBLL qobj = new GetQuoteBLL();
             qobj.FirstName = Request.Form["firstname"];
             qobj.MiddleName = Request.Form["middlename"];
             qobj.LastName = Request.Form["lastname"];
             qobj.Address = Request.Form["streetaddress" + "apt"];
             qobj.City = Request.Form["city"];
             qobj.State = Request.Form["state"];
             qobj.Zip = Int32.Parse(Request.Form["zip"]);
             qobj.DOB = Request.Form["dob"];
             qobj.Year = Int32.Parse(Request.Form["Year"]);
             qobj.Make = Request.Form["Make"];
             qobj.Model = Request.Form["Model"];
             qobj.CarRelation = Request.Form["vehiclerel"];
             qobj.Usage = Request.Form["usage"];
             qobj.Milage = Int32.Parse(Request.Form["mileage"]);
             try
             {
                 qobj.Marital = Convert.ToBoolean(Request.Form["marital"]);
                 qobj.Military = Convert.ToBoolean(Request.Form["military"]);
                 qobj.Veteran = Convert.ToBoolean(Request.Form["veteran"]);
                 qobj.Accidents = Convert.ToBoolean(Request.Form["accidentradio"]);
                 qobj.Tickets = Convert.ToBoolean(Request.Form["ticketradio"]);
                 qobj.DUIs = Convert.ToBoolean(Request.Form["duiradio"]);
                 qobj.Suspension = Convert.ToBoolean(Request.Form["suspensionradio"]);
                 qobj.DefensiveDriver = Convert.ToBoolean(Request.Form["defensiveradio"]);
             }
             catch (FormatException)
             {
                 Console.WriteLine("The value doesn't match the boolean format!!!");
             }
             qobj.Gender = Request.Form["optradio"];
             qobj.Education = Request.Form["education"];
             qobj.Employment = Request.Form["employment"];
             qobj.Email = Request.Form["email"];
             qobj.PhoneNum = long.Parse(Request.Form["phonenumber"]);
             
            return View("QuoteView", qobj);
        }
    }
}