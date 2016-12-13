﻿using System;
using System.ComponentModel.DataAnnotations;

namespace RainCheck
{
    internal class ValidateDateRangeAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // your validation logic
            if(value==null)
            {
                return new ValidationResult("Date is required");
            }
            else if ((DateTime)value >= Convert.ToDateTime(DateTime.Now.Date.ToShortDateString()) && (DateTime)value <= Convert.ToDateTime("01/12/2026"))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Date is not in range of [" + DateTime.Now.Date.ToShortDateString()+"]" + "[01/12/2026]");
            }
        }
    }
}