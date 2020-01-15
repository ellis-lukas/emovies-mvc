using mvcSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using mvcSite.IEnumerableExtensions;
using mvcSite.CustomisedValidators;
using System.Web.Mvc;

namespace mvcSite.ViewModels.Home
{
    public class HomeViewModel : IValidatableObject
    {
        [QuantitiesNotAllZero]
        [QuantitiesInInclusiveRange(0, 254)]
        public IEnumerable<MovieOrderRow> MovieOrderRows { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Total { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            IEnumerable<MovieOrderRow> movieOrderRows = this.MovieOrderRows;

            foreach (MovieOrderRow movieOrderRow in movieOrderRows)
            {
                if (movieOrderRow.Quantity < 0)
                {
                    yield return new ValidationResult("Quantities can not be negative");
                }

                if (movieOrderRow.Quantity > 254)
                {
                    yield return new ValidationResult("Maximum quantity permitted is 254");
                }
            }
        }
    }
}