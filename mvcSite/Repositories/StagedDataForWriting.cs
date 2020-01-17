using mvcSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvcSite.Repositories
{
    public class StagedDataForWriting
    {
        public Customer Customer { get; set; }

        public IEnumerable<OrderLine> OrderLines { get; set; }

        public decimal Total { get; set; }
    }
}