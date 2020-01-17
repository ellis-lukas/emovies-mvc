using mvcSite.Models;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace mvcSite.DAL.DatabaseAccess
{
    public class CustomerDatabaseWriter : ICustomerWriter
    {
        public int WriteCustomer(Customer customer)
        {
            SqlConnection databaseConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["database"].ConnectionString);

            using (databaseConnection)
            {
                databaseConnection.Open();

                SqlCommand customerWriteCommand = new SqlCommand
                {
                    Connection = databaseConnection,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "AddNewCustomer"
                };

                AddParametersToCommandFromCustomerData(customerWriteCommand, customer);

                SqlParameter returnParameter = new SqlParameter("@RETURN_VALUE", SqlDbType.Int)
                {
                    Direction = ParameterDirection.ReturnValue
                };
                customerWriteCommand.Parameters.Add(returnParameter);

                using (customerWriteCommand)
                {
                    customerWriteCommand.ExecuteReader();
                }

                int IDAssignedToLastWrittenCustomer = GetAssignedID(returnParameter);



                return IDAssignedToLastWrittenCustomer;
            }
        }

        private void AddParametersToCommandFromCustomerData(SqlCommand customerWriteCommand, Customer customer)
        {
            customerWriteCommand.Parameters.Add("@Name", SqlDbType.NVarChar).Value = customer.Name;
            customerWriteCommand.Parameters.Add("@Email", SqlDbType.NVarChar).Value = customer.Email;
            customerWriteCommand.Parameters.Add("@CreditCardNumber", SqlDbType.NVarChar).Value = customer.CardNumber;
            customerWriteCommand.Parameters.Add("@CreditCardType", SqlDbType.NVarChar).Value = customer.CardType;
            customerWriteCommand.Parameters.Add("@FuturePromotions", SqlDbType.Bit).Value = customer.FuturePromotions;
            customerWriteCommand.Parameters.Add("@DateCreated", SqlDbType.DateTime).Value = DateTime.UtcNow;//uct.now
        }

        private int GetAssignedID(SqlParameter returnParameter)
        {
            int lastAddedEntryID = Convert.ToInt32(returnParameter.Value);
            return lastAddedEntryID;
        }
    }
}