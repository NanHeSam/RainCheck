using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RainCheck.Models
{
    public class policyVM
    {
        public IEnumerable<policy_tbl> policies { get; set; }
        public user_tbl user { get; set; }
        public string userName
        {
            get
            {
                return user.user_F_name + " " + user.user_M_name;
            }
        }
    }
}