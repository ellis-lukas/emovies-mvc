using mvcSite.Models;
using mvcSite.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvcSite.IEnumerableExtensions
{
    public static class IEnumerableOrderLineExtension
    {
        public static decimal Total(this IEnumerable<OrderLine> orderLines)
        {
            decimal total = 0.0m;

            foreach(OrderLine orderLine in orderLines)
            {
                total += orderLine.Quantity * orderLine.Price;
            }

            return total;
        }

        public static IEnumerable<OrderLine> AssignWithOrderID(this IEnumerable<OrderLine> orderLinesOrderIDsUnassigned, int orderID)
        {
            List<OrderLine> orderLinesOrderIDsAssigned = new List<OrderLine>();

            foreach (OrderLine orderLineOrderIDUnassigned in orderLinesOrderIDsUnassigned)
            {
                orderLineOrderIDUnassigned.OrderID = orderID;
            }

            return orderLinesOrderIDsAssigned;
        }

        public static IEnumerable<OrderLine> RemoveZeroQuantityOrderLines(this IEnumerable<OrderLine> orderLines)
        {
            List<OrderLine> nonZeroQuantityOrderLines = orderLines.ToList();

            foreach(OrderLine orderLine in orderLines)
            {
                if(orderLine.Quantity == 0)
                {
                    nonZeroQuantityOrderLines.Remove(orderLine);
                }
            }

            return nonZeroQuantityOrderLines;
        }
    }
}