using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using mvcSite.CustomisedValidators;

namespace mvcSite.ViewModels.Home
{
    public class HomeViewModel 
        //: IValidatableObject
    {
        [QuantitiesNotAllZero]
        [QuantitiesInInclusiveRange(0, 254)]
        public IEnumerable<MovieOrderRow> MovieOrderRows { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Total { get; set; }
    }
}