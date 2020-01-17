using mvcSite.DAL;
using mvcSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvcSite.Repositories
{
    public class OrderLineRepository
    {
        private readonly IOrderLineWriter _orderLineWriter;

        public OrderLineRepository(IOrderLineWriter orderLineWriter)
        {
            _orderLineWriter = orderLineWriter;
        }

        public void WriteOrderLine(OrderLine orderLine)
        {
            _orderLineWriter.WriteOrderLine(orderLine);
        }
    }
}