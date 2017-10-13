using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace RainCheck.Controllers
{
    public class CustomerController : Controller
    {        
        public ActionResult StartApp()
        {
            // return View("Licence", new customer_tbl());
            //return View("PolicyInfromation");
            return View("Fresh");
        }
        public ActionResult Begins(customer_tbl ct)
        {
           
                return View("PolicyInfromation");
            
        }
        public ActionResult Purchase(policy_tbl pl)
        {
                return View("StartPurchase");
            
        }      
        public ActionResult Submit(login obj)
        {
            if (ModelState.IsValid)
            {
                RainCheckServerEntities objContext = new RainCheckServerEntities();
                List<quote> qs = objContext.quotes.ToList();
                quote newQuote = new quote();
                foreach (quote q in qs)
                {
                    if (q.Reference_number == Request.Form["reference"])
                        newQuote = q;
                }
                decimal userID = newQuote.userid;
                user_tbl newUser = new user_tbl();
                List<user_tbl> ut = objContext.user_tbl.ToList();
                foreach (user_tbl x in ut)
                {
                    if (x.userid == userID)
                        newUser = x;
                }
                //To Show the User's Info
                ViewBag.USER = newUser;
                //set up the amount on each coverage for the customer
                decimal policy_amount = 0;
                decimal p1 = getAmount(Request.Form["SelfProperty"]);
                decimal p2 = getAmount(Request.Form["SelfBody"]);
                decimal p3 = getAmount(Request.Form["OppositeProperty"]);
                decimal p4 = getAmount(Request.Form["OppositeBody"]);

                //set up the coverage level for the customer
                decimal selfPro = getCoverage(Request.Form["SelfProperty"]);
                decimal selfBod = getCoverage(Request.Form["SelfBody"]);
                decimal oppPro = getCoverage(Request.Form["OppositeProperty"]);
                decimal oppBod = getCoverage(Request.Form["OppositeBody"]);

                Random rnd = new Random();
                ViewBag.randm = rnd.Next(100000000, 1000000000);
                string startDate = Request.Form["start"];
                string endDate = Request.Form["end"];
                string DL = Request.Form["licence"];
                ViewBag.st = startDate;
                ViewBag.ed = endDate;
                decimal total = policy_amount + newQuote.quote_amount + p1 + p2 + p3 + p4;
                ViewBag.Amount = policy_amount + newQuote.quote_amount + p1 + p2 + p3 + p4;
                //Create a new customer in the database  *******************************************/      
                customer_tbl ct = new customer_tbl();
                ct.userid = userID;
                //ct.join_date = Convert.ToDateTime(startDate);/////////////////////////////////////////
                //ct.driver_license_number = Decimal.Parse(DL);/////////////////////////////////////////////
                objContext.customer_tbl.Add(ct);
                //objContext.SaveChanges();
                decimal custid = 0;
                List<customer_tbl> customerTable = objContext.customer_tbl.ToList();
                foreach (customer_tbl cs in customerTable)
                {
                    if (cs.customer_id > custid)
                    {
                        custid = cs.customer_id;
                    }
                }
                //Create a user Login ****************************************************************************/
                string un = Request.Form["user_name"];
                string pw = Request.Form["password"];
                bool sameUser = false;
                bool samePassword = false;
                List<login> lns = objContext.logins.ToList();
                login lg = new login();
                foreach (login l in lns)
                {
                    if (String.Equals(l.user_name, un))
                    {
                        sameUser = true;
                    }
                }
                foreach (login l in lns)
                {
                    if (String.Equals(l.password, pw))
                    {
                        samePassword = true;
                    }
                }
                lg.customer_id = custid;
                lg.user_name = Request.Form["username"];
                lg.password = Request.Form["password"];
                if (!(sameUser) && !(samePassword))
                {
                    objContext.logins.Add(lg);
                    // objContext.SaveChanges();
                }
                //Create a new Policy with a new coverage *********************************************************************************/

                //Retrieve the car associted with this policy
                List<car> crs = objContext.cars.ToList();
                //decimal vin = Decimal.Parse(Request.Form["VIN"]);/////////////////////////////////////////////////
                decimal vin = 5;//////////////////////////////////////////////////////////////////////
                decimal carid = 0;
                foreach (car x in crs)
                {
                    if (x.vin_number == vin)
                        carid = x.car_id;
                }
                //set the policy for a car
                Random rd = new Random();
                policy_tbl policy = new policy_tbl();
                policy.policy_number = rnd.Next(100000000, 1000000000);
                // policy.car_id = carid; policy.user_id = userID; policy.start_date = Convert.ToDateTime(startDate);
                // policy.end_date = Convert.ToDateTime(endDate); policy.policy_amount = total; policy.self_property = selfPro;
                policy.self_body = selfBod; policy.opposite_property = oppPro; policy.opposite_body = oppBod;
                objContext.policy_tbl.Add(policy);
                // objContext.SaveChanges();              
               
               return View("NewPolicy");
              }
              else
              {
                ViewBag.Temprory = TempData.Peek("Test1").ToString();
                return View("StartPurchase",obj);
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
            else if(coverage == "10000")
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

        //Get the Sucharge Amount for given policy
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
