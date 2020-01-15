using Microsoft.AspNetCore.Mvc;
using mvcSite.ViewModels.Home;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;

namespace mvcSite.CustomisedValidators
{
    public class QuantitiesNotAllZeroAttribute : ValidationAttribute
    {
        public QuantitiesNotAllZeroAttribute() { }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                IEnumerable<MovieOrderRow> movieOrderRows = value as IEnumerable<MovieOrderRow>;

                foreach (MovieOrderRow movieOrderRow in movieOrderRows)
                {
                    int quantity = movieOrderRow.Quantity;
                    if(quantity != 0)
                    {
                        return ValidationResult.Success;
                    }
                }

                return new ValidationResult("Please select a movie");
            }

            return ValidationResult.Success;
        }
    }
}