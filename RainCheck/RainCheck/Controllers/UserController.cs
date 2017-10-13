﻿using RainCheck.common;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.Mvc;
using RainCheck.Models;

namespace RainCheck.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Retrieve()
        {
            UserVM uv = new UserVM(new car(), new city());
            return View("ViewModel", uv);
        }
        public ActionResult Load()
        {
            customer_tbl ct = new customer_tbl {customer_id = 1, userid = 2, driver_license_number = 22221982 , join_date = Convert.ToDateTime("12/12/1982")};
            return View("LoadData",ct);
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "StartPolicy")]
        public ActionResult StartPolicy(customer_tbl ob)
        {
            if (ModelState.IsValid)
            {
                /*  -----------------------------------------------------------------------  */
                decimal dl = ob.driver_license_number;
                TempData["DriverLicence"] = dl;
                RainCheckServerEntities obContext = new RainCheckServerEntities();
                bool uniqDL = true;
                List<customer_tbl> ctb = obContext.customer_tbl.ToList();
                foreach (customer_tbl c in ctb)
                {                
                    if (c.driver_license_number == dl)
                    {
                        uniqDL = false;
                        ViewBag.UND = "This Driver has already a policy, Try a diffrent ID";
                    }
                }
                if(uniqDL)
                {
                    policy_tbl pl = new policy_tbl { car_id = 1, start_date = Convert.ToDateTime("12/12/1982"), end_date = Convert.ToDateTime("12/12/1982"), opposite_body = 1, policy_amount = 200, policy_id = 2, self_body = 1, policy_number = 2443662, opposite_property = 2, self_property = 1, user_id = 1 };
                    return View("NewPolicy", pl);
                }
                else
                {
                    return View("LoadData", ob);
                }
            }
            else
            {
                return View("LoadData", ob);
            }
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "PreviousAccount")]
        public ActionResult PreviousAccount()
        {            
                policy_tbl pl = new policy_tbl { car_id = 1, start_date = Convert.ToDateTime("12/12/1982"), end_date = Convert.ToDateTime("12/12/1982"), opposite_body = 1, policy_amount = 200, policy_id = 2, self_body = 1, policy_number = 2443662, opposite_property = 2, self_property = 1, user_id = 1 };
                return View("NewPolicy", pl);            
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "CreateAccount")]
        public ActionResult CreateAccount(policy_tbl p)
        {
            bool validDate = false;
            string s1 = Request.Form["start_date"].ToString();
            string s2 = Request.Form["end_date"].ToString();
            if (s1 != "" && s2 != "")
            {
                DateTime d1 = Convert.ToDateTime(s1);
                DateTime d2 = Convert.ToDateTime(s2);
                if (DateTime.Compare(d1, d2) == 0 || DateTime.Compare(d1, d2) > 0)
                {
                    validDate = false;
                    ViewBag.vlid = "The End Date must be greater than Start Date";
                }
                else
                {
                    validDate = true;
                }
            }
            else
            {
                validDate = false;
            }
            

            if (ModelState.IsValid && validDate)
            {
                TempData["SD"] = Request.Form["start_date"];
                TempData["ED"] = Request.Form["end_date"];
                TempData["SP"] = Request.Form["SelfProperty"];
                TempData["SB"] = Request.Form["SelfBody"];
                TempData["OP"] = Request.Form["OppositeProperty"];
                TempData["OB"] = Request.Form["OppositeBody"];                

                login l = new login { login_id = 1, customer_id = 1, password = " ", user_name = " " };                
                return View("NewAccount",l);
            }
            else
            {
                return View("NewPolicy", p);
            }
        }
        //form submission
        [HttpPost]
        [MultipleButton(Name = "action", Argument = "Submit")]
        public ActionResult Submit(login ls)
        {
            //RainCheckServerEntities obContext = new RainCheckServerEntities();


            if (ModelState.IsValid)
            {                
                RainCheckServerEntities objContext = new RainCheckServerEntities();             

                bool confirm = false;
                bool samUser = false;
                bool samPassword = false;

                List<login> lgs = objContext.logins.ToList();
                ViewBag.UN = "";
                ViewBag.PW = "";
                ViewBag.CPW = "";
                if (ls.password == Request.Form["confirms"].ToString())
                {
                    confirm = true;
                }
                else
                {
                    ViewBag.CPW = "Your password does not match, please confim it";
                }
                foreach (login l in lgs)
                {
                    if (String.Equals(l.user_name, ls.user_name))
                    {
                        samUser = true;
                        ViewBag.UN = "This Username has been taken, please choose another one";
                    }
                    if (String.Equals(l.password, ls.password))
                    {
                        samPassword = true;
                        ViewBag.PW = "This Password has been taken, please choose another one";
                    }
                }
                if (!samPassword && !samUser && confirm)
                {
                    decimal userID = 0;
                    string quoteReference = "1000";
                    List<quote> qs = objContext.quotes.ToList();
                    quote newQuote = new quote();
                    foreach (quote q in qs)
                    {
                        if ((q.Reference_number).Equals(quoteReference))
                        {
                            userID = q.userid;
                            newQuote = q;
                        }
                    }
                    //set up the amount on each coverage for the customer
                    decimal policy_amount = 0;
                    decimal p1 = getAmount(TempData.Peek("SP").ToString());
                    decimal p2 = getAmount(TempData.Peek("SB").ToString());
                    decimal p3 = getAmount(TempData.Peek("OP").ToString());
                    decimal p4 = getAmount(TempData.Peek("OB").ToString());

                    //set up the coverage level for the customer
                    decimal selfPro = getCoverage(TempData.Peek("SP").ToString());
                    decimal selfBod = getCoverage(TempData.Peek("SB").ToString());
                    decimal oppPro = getCoverage(TempData.Peek("OP").ToString());
                    decimal oppBod = getCoverage(TempData.Peek("OB").ToString());

                    Random rnd = new Random();
                    decimal polID = rnd.Next(100000000, 1000000000);
                    ViewBag.randm = polID;

                    string startDate = TempData.Peek("SD").ToString();
                    string endDate = TempData.Peek("ED").ToString();
                    ViewBag.st = startDate;
                    ViewBag.ed = endDate;
                    decimal total = policy_amount + newQuote.quote_amount + p1 + p2 + p3 + p4;
                    ViewBag.Amount = policy_amount + newQuote.quote_amount + p1 + p2 + p3 + p4;

                    using (RainCheckServerEntities context = new RainCheckServerEntities())
                    {
                        try
                        {
                            //Create a new customer in the database  *******************************************/   
                            string dl = TempData.Peek("DriverLicence").ToString();
                            customer_tbl ct = new customer_tbl();
                            ct.userid = userID;
                            ct.join_date = Convert.ToDateTime(startDate);
                            objContext.customer_tbl.Add(ct);
                            ct.driver_license_number = Decimal.Parse(dl);
                            try
                            {
                                context.SaveChanges();
                            }
                            catch (OptimisticConcurrencyException ex)
                            {
                                ex.ToString();
                            }
                        }
                        catch (UpdateException ex)
                        {
                            Console.WriteLine(ex.ToString());
                        }
                    }

                    //Create a user Login ****************************************************************************/
                    decimal custid = 0;
                    string un = Request.Form["user_name"].ToString();
                    string pw = Request.Form["password"].ToString();
                    TempData["PSW"] = pw;
                    TempData["USN"] = un;
                    TempData["CPW"] = pw;
                    List<customer_tbl> customerTable = objContext.customer_tbl.ToList();
                    foreach (customer_tbl cs in customerTable)
                    {
                        if (cs.customer_id > custid)
                        {
                            custid = cs.customer_id;
                        }
                    }

                    login lg = new login();
                    lg.customer_id = custid;
                    lg.user_name = un;
                    lg.password = pw;
                    objContext.logins.Add(lg);
                    // objContext.SaveChanges();
                    try
                    {
                        objContext.SaveChanges();
                    }
                    catch (DbEntityValidationException ex)
                    {
                        foreach (DbEntityValidationResult item in ex.EntityValidationErrors)
                        {
                            // Get entry

                            DbEntityEntry entry = item.Entry;
                            string entityTypeName = entry.Entity.GetType().Name;

                            // Display or log error messages

                            foreach (DbValidationError subItem in item.ValidationErrors)
                            {
                                string message = string.Format("Error '{0}' occurred in {1} at {2}",
                                         subItem.ErrorMessage, entityTypeName, subItem.PropertyName);
                                Console.WriteLine(message);
                            }
                        }
                    }

                    //Create a new Policy with a new coverage *********************************************************************************/

                    //Retrieve the car associted with this policy[getting the car object through session]                
                    List<car> crs = objContext.cars.ToList();
                    car cr = new car();
                    // decimal carId = Decimal.Parse(TempData["CarINFO"].ToString());
                    decimal vin = 5;
                    decimal carid = 0;
                    foreach (car x in crs)
                    {
                        if (x.vin_number == vin)
                            carid = x.car_id;
                    }
                    // objContext.cars.Add(cr);
                    //objContext.SaveChanges();  
                    //set up the policy for a car
                    Random rd = new Random();
                    policy_tbl policy = new policy_tbl();
                    policy.policy_number = polID;
                    policy.car_id = 6; policy.user_id = userID; policy.start_date = Convert.ToDateTime(startDate);
                    policy.end_date = Convert.ToDateTime(endDate); policy.policy_amount = total; policy.self_property = selfPro;
                    policy.self_body = selfBod; policy.opposite_property = oppPro; policy.opposite_body = oppBod;
                    objContext.policy_tbl.Add(policy);
                    objContext.SaveChanges();

                    return View("WelcomePage");
                }
                else
                {
                    return View("NewAccount", ls);
                }
            }
            else
            {
                return View("NewAccount", ls);
            }
        }
        //Get the coverage Level
        public decimal getCoverage(string coverage)
        {
            int cov = 1;
            if (coverage == "0")
            {
                cov = 1;
            }
            else if (coverage == "10000")
            {
                cov = 2;
            }
            else if (coverage == "25000")
            {
                cov = 3;
            }
            else if (coverage == "50000")
            {
                cov = 4;
            }
            else if (coverage == "100000")
            {
                cov = 5;
            }
            return cov;
        }

        //Get the Sucharge Amount for a given policy
        public decimal getAmount(string amt)
        {
            decimal amnt = 0;
            if (amt == "0")
            {
                amnt = 0;
            }
            else if (amt == "10000")
            {
                amnt = 10;
            }
            else if (amt == "25000")
            {
                amnt = 25;
            }
            else if (amt == "50000")
            {
                amnt = 50;
            }
            else if (amt == "100000")
            {
                amnt = 100;
            }

            return amnt;
        }
    }
}