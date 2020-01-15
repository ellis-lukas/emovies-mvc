using mvcSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvcSite.DAL
{
    public class DataStagedForWrite
    {
        public Customer CustomerData;

        public Order CustomerOrderData;

        public IEnumerable<OrderLine> OrderLines;
    }
}
