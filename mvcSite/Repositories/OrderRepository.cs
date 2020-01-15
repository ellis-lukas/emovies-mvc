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

        public void WriteOrder(Order order)
        {
            _orderWriter.WriteOrder(order);
        }

        public int GetLastWrittenEntryAssignedID()
        {
            return _orderWriter.GetLastWrittenEntryAssignedID();
        }
    }
}