using mvcSite.DAL;
using mvcSite.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace mvcSite.Repositories
{
    public class CustomerRepository
    {
        private readonly ICustomerWriter CustomerWriter;

        public CustomerRepository(ICustomerWriter customerWriter) {
            CustomerWriter = customerWriter;
        }

        public int WriteCustomer(Customer customer)
        {
           return CustomerWriter.WriteCustomer(customer);
        }
    }
}