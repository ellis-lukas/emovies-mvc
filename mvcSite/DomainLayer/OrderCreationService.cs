using mvcSite.IEnumerableExtensions;
using mvcSite.Models;
using mvcSite.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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

        public void CreateOrders(StagedDataForWriting stagedData)
        {
            Customer customerToWrite = stagedData.Customer;
            IEnumerable<OrderLine> orderLinesOrderIDsUnassigned = stagedData.OrderLines;

            WriteCustomer(customerToWrite);

            int writtenCustomerAssignedID = GetLastWrittenCustomerID();
            Order orderToWrite = new Order
            {
                CustomerID = writtenCustomerAssignedID,
                DateCreated = DateTime.Now,
                Total = orderLinesOrderIDsUnassigned.Total()
            };

            WriteOrder(orderToWrite);

            int writtenOrderAssignedID = GetLastWrittenOrderID();
            IEnumerable<OrderLine> orderLinesToWrite = orderLinesOrderIDsUnassigned.AssignWithOrderID(writtenOrderAssignedID);

            WriteOrderLines(orderLinesToWrite);
        }

        private void WriteCustomer(Customer customerToWrite)
        {
            _customerRepository.WriteCustomer(customerToWrite);
        }

        private int GetLastWrittenCustomerID()
        {
            return _customerRepository.GetLastWrittenEntryAssignedID();
        }

        private void WriteOrder(Order orderToWrite)
        {
            _orderRepository.WriteOrder(orderToWrite);
        }

        private int GetLastWrittenOrderID()
        {
            return _orderRepository.GetLastWrittenEntryAssignedID();
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