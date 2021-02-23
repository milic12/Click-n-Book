using System;
using System.ComponentModel.DataAnnotations;

namespace Click_and_Book.CustomAttributes
{
    public class FromNowAttribute : ValidationAttribute
    {

        public string GetErrorMessage() => $"Date must be set past {DateTime.Now.AddDays(-1).ToString("dd-MM-yyyy")}";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var date = (DateTime)value;

            if (DateTime.Compare(date.Date, DateTime.Now.Date) < 0)
                return new ValidationResult(GetErrorMessage());
            else
                return ValidationResult.Success;
        }
    }
}