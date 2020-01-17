using mvcSite.DAL;
using mvcSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvcSite.Repositories
{
    public class OrderRepository
    {
        private readonly IOrderWriter _orderWriter;

        public OrderRepository(IOrderWriter orderWriter)
        {
            _orderWriter = orderWriter;
        }

        public int WriteOrder(Order order)
        {
            return _orderWriter.WriteOrder(order);
        }
    }
}