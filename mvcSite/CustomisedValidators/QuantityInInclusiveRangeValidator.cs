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
    public class QuantitiesInInclusiveRangeAttribute : ValidationAttribute
    {
        public int LowerBound;
        public int UpperBound;

        public QuantitiesInInclusiveRangeAttribute(int lowerBound, int upperBound)
        {
            LowerBound = lowerBound;
            UpperBound = upperBound;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                IEnumerable<MovieOrderRow> movieOrderRows = value as IEnumerable<MovieOrderRow>;

                foreach(MovieOrderRow movieOrderRow in movieOrderRows)
                {
                    int quantity = movieOrderRow.Quantity;

                    if (quantity < LowerBound || quantity > UpperBound)
                    {
                        string outOfRangeErrorMessage = $"Quantity field not within acceptable range";
                        return new ValidationResult(outOfRangeErrorMessage);
                    }
                }

                return ValidationResult.Success;
            }

            return ValidationResult.Success;
        }
    }
}