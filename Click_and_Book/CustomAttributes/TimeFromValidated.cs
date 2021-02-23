using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Click_and_Book.CustomAttributes
{
    public class TimeFromValidated : ValidationAttribute
    {
        protected override ValidationResult
                IsValid(object value, ValidationContext validationContext)
        {
            var model = (Models.Reservation)validationContext.ObjectInstance;
            DateTime EndDate = Convert.ToDateTime(model.TimeTo);
            DateTime StartDate = Convert.ToDateTime(value);

            if (StartDate.Date >= EndDate.Date)
                return new ValidationResult("Check in date must lower than check out date");
            else
                return ValidationResult.Success;
        }
    }
}
