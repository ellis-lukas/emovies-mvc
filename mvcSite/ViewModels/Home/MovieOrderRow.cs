using mvcSite.Models;
using mvcSite.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using mvcSite.CustomisedValidators;

namespace mvcSite.ViewModels.Home
{
    public class MovieOrderRow
    {
        public int MovieID { get; set; }

        public string Name { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}