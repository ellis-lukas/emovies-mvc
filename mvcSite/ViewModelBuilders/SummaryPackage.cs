using mvcSite.Models;
using mvcSite.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvcSite.ViewModelBuilders
{
    public class SummaryPackage
    {
        public Customer Customer { get; set; }

        public IEnumerable<OrderLine> OrderLines { get; set; }
    }
}