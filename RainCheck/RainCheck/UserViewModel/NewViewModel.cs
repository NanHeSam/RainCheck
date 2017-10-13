using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RainCheck.UserViewModel
{
    public class NewViewModel
    {
        public NewViewModel(customer_tbl ct, user_tbl ut, policy_tbl pt, login ln, quote qt)
        {
            custTable = ct;
            uTable = ut;
            pTable = pt;
            logTable = ln;
            quoteTable = qt;
        }
        public customer_tbl custTable { get; set; }
        public user_tbl uTable { get; set; }
        public policy_tbl pTable { get; set; }
        public login logTable { set; get; }
        public quote quoteTable { set; get; }
    }

}
