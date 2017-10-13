using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RainCheck;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace RainCheck.Models
{
    public class UserVM
    {      
        public car cr { get; set; }
        public city ct { get; set; }
        public UserVM(car c, city st)
        {
            cr = c;
            ct = st;
        }
    }
}