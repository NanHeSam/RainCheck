using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;

namespace RainCheck.Models
{
    public class GetQuoteDAL
    {
        RainCheckServerEntities1 rcsobject = new RainCheckServerEntities1();
        public ObjectResult<RCSgetmodel_Result> model_name;
        public Array Getmake(string make_name)
        {
         model_name = rcsobject.RCSgetmodel(make_name);
            RCSgetmodel_Result[] array = model_name.ToArray<RCSgetmodel_Result>();
            return array;
            //return model_name;
         }
       

    }
}