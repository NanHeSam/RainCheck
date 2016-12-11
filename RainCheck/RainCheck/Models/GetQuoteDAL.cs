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
      
        
        public Array Getmodels(string make_name)
        {
            ObjectResult<string> model_name = rcsobject.RCSgetmodel(make_name);
            string[] models = model_name.ToArray<string>();
            return models;
         }

        public Array GetStates()
        {
            ObjectResult<string> getstates = rcsobject.SP_GetState();
            string[] states = getstates.ToArray<string>();
            return states;
        }

        public Array GetCities(string statename)
        {
            ObjectResult<string> getcities = rcsobject.SP_GetCity(statename);
            string[] cities = getcities.ToArray<string>();
            return cities;
        }

        public bool InsertUser(string firstname, string middlename, string lastname, decimal phone, string ssn, System.DateTime dob, string email, string address, string city, string state, decimal zip, bool marital_Status, bool gender, string education_Level, string employment, bool accident_Records_5YEARS, bool traffic_Tickets_5YEARS, bool dUIs_10_YEARS, bool suspensions_5YEARS, bool defensive_Drive_2YEARS, bool military_Affiliation, bool veteran_Status, decimal rank_ID)
        {
            int UserInfoInserted = rcsobject.SP_InsertUserInfo(firstname, middlename, lastname, phone, ssn, dob, email, address, city, state, zip, marital_Status, gender, education_Level, employment, accident_Records_5YEARS, traffic_Tickets_5YEARS, dUIs_10_YEARS, suspensions_5YEARS, defensive_Drive_2YEARS, military_Affiliation, veteran_Status, rank_ID);
            if (UserInfoInserted > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}