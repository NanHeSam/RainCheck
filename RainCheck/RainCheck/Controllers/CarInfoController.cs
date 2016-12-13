﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RainCheck.Models;
using System.Text;

namespace RainCheck.Controllers
{
    public class CarInfoController : Controller
    {
        // GET: CarInfo
        public ActionResult CarInfo()
        {
            return View("CarInfo");
        }

        [HttpPost]
        public JsonResult MyAjaxCall(string value)
        {
            GetQuoteDAL modelObject = new GetQuoteDAL();
            return Json(modelObject.Getmodels(value));
        }

        public ActionResult SubmitCarInfo()
        {
            GetQuoteDAL dalObject = new GetQuoteDAL();
            
                int vin = Int32.Parse(Request.Form["vin"]);
                int year = Int32.Parse(Request.Form["Year"]);
                string make = Request.Form["Make"];
                string model = Request.Form["Model"];
                string body_type = Request.Form["body"];
                string usage = Request.Form["usage"];
                int annum_mileage = Int32.Parse(Request.Form["mileage"]);
                int check = Int32.Parse(Request.Form["vehiclerel"]);
                 int carID;
                if (check == 0)
                {
                    bool ownership = false;
                    carID = dalObject.InsertCar(vin, make, model, body_type, usage, annum_mileage, ownership, year);
                }
                else
                {
                   bool ownership = true;
                   carID = dalObject.InsertCar(vin, make, model, body_type, usage, annum_mileage, ownership, year);
                  }
                
                
            
               
                TempData["Carid"] = carID;
            
           
            return RedirectToAction("CarInfo", "CarInfo");

        }
    }
}