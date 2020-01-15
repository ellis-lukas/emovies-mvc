using mvcSite.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace mvcSite.DAL.DatabaseAccess
{
    public class CustomerDatabaseWriter : ICustomerWriter
    {
        private SqlParameter ReturnParameter;

        public void WriteCustomer(Customer customer)
        {
            SqlCommand customerWriteCommand = new SqlCommand
            {
                Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["database"].ConnectionString),
                CommandType = CommandType.StoredProcedure,
                CommandText = "AddNewCustomer"
            };

            AddParametersToCommandFromCustomerData(customerWriteCommand, customer);

            ReturnParameter = new SqlParameter("@RETURN_VALUE", SqlDbType.Int)
            {
                Direction = ParameterDirection.ReturnValue
            };
            customerWriteCommand.Parameters.Add(ReturnParameter);

            customerWriteCommand.Connection.Open();

            using (customerWriteCommand)
            {
                customerWriteCommand.ExecuteReader();
            }
        }

        private void AddParametersToCommandFromCustomerData(SqlCommand customerWriteCommand, Customer customer)
        {
            customerWriteCommand.Parameters.Add("@Name", SqlDbType.NVarChar).Value = customer.Name;
            customerWriteCommand.Parameters.Add("@Email", SqlDbType.NVarChar).Value = customer.Email;
            customerWriteCommand.Parameters.Add("@CreditCardNumber", SqlDbType.NVarChar).Value = customer.CardNumber;
            customerWriteCommand.Parameters.Add("@CreditCardType", SqlDbType.NVarChar).Value = customer.CardType;
            customerWriteCommand.Parameters.Add("@FuturePromotions", SqlDbType.Bit).Value = customer.FuturePromotions;
            customerWriteCommand.Parameters.Add("@DateCreated", SqlDbType.DateTime).Value = DateTime.Now;
        }

        public int GetLastWrittenEntryAssignedID()
        {
            int lastAddedEntryID = Convert.ToInt32(ReturnParameter.Value);
            return lastAddedEntryID;
        }
    }
}