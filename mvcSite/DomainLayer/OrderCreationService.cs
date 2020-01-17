using mvcSite.Models;
using mvcSite.Repositories;
using System;
using System.Collections.Generic;

namespace mvcSite.DAL
{
    public class OrderCreationService
    {
        private readonly CustomerRepository _customerRepository;

        private readonly OrderRepository _orderRepository;

        private readonly OrderLineRepository _orderLineRepository;

        public OrderCreationService(
                CustomerRepository customerRepository,
                OrderRepository orderRepository,
                OrderLineRepository orderLineRepository
            )
        {
            _customerRepository = customerRepository;
            _orderRepository = orderRepository;
            _orderLineRepository = orderLineRepository;
        }

        public void CreateOrders(StagedDataForWriting stagedData)//orderlines not being saved to the database. fix.
        {
            Customer customerToWrite = stagedData.Customer;
            IEnumerable<OrderLine> orderLines = stagedData.OrderLines;
            decimal orderTotal = stagedData.Total;

            int IDAssignedToLastWrittenCustomer = WriteCustomer(customerToWrite);

            Order orderToWrite = new Order
            {
                CustomerID = IDAssignedToLastWrittenCustomer,
                DateCreated = DateTime.UtcNow,
                Total = orderTotal
            };

            int IDAssignedToLastWrittenOrder = WriteOrder(orderToWrite);

            AssignOrderLinesWithOrderID(orderLines, IDAssignedToLastWrittenOrder);
            WriteOrderLines(orderLines);
        }

        private int WriteCustomer(Customer customerToWrite)
        {
            return _customerRepository.WriteCustomer(customerToWrite);
        }

        private int WriteOrder(Order orderToWrite)
        {
            return _orderRepository.WriteOrder(orderToWrite);
        }

        private void AssignOrderLinesWithOrderID(IEnumerable<OrderLine> orderLinesOrderIDsUnassigned, int orderID)
        {
            foreach (OrderLine orderLineOrderIDUnassigned in orderLinesOrderIDsUnassigned)
            {
                orderLineOrderIDUnassigned.OrderID = orderID;
            }
        }

        private void WriteOrderLines(IEnumerable<OrderLine> orderLinesToWrite)
        {
            foreach (OrderLine orderLineIDUnassigned in orderLinesToWrite)
            {
                _orderLineRepository.WriteOrderLine(orderLineIDUnassigned);
            }
        }
    }
}