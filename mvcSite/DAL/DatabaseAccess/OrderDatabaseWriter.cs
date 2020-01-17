using mvcSite.Models;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace mvcSite.DAL.DatabaseAccess
{
    public class OrderDatabaseWriter : IOrderWriter
    {
        public int WriteOrder(Order order)
        {
            SqlConnection databaseConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["database"].ConnectionString);

            using (databaseConnection)
            {
                databaseConnection.Open();

                SqlCommand orderWriteCommand = new SqlCommand
                {
                    Connection = databaseConnection,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "AddNewOrder"
                };

                AddParametersToCommandFromOrderData(orderWriteCommand, order);

                SqlParameter returnParameter = new SqlParameter("@RETURN_VALUE", SqlDbType.Int)
                {
                    Direction = ParameterDirection.ReturnValue
                };
                orderWriteCommand.Parameters.Add(returnParameter);

                using (orderWriteCommand)
                {
                    orderWriteCommand.ExecuteReader();
                }

                int IDAssignedToLastWrittenOrder = GetAssignedID(returnParameter);

                return IDAssignedToLastWrittenOrder;
            }
        }

        private void AddParametersToCommandFromOrderData(SqlCommand orderWriteCommand, Order order)
        {
            orderWriteCommand.Parameters.Add("@DateCreated", SqlDbType.DateTime).Value = DateTime.UtcNow;
            orderWriteCommand.Parameters.Add("@Total", SqlDbType.Decimal).Value = order.Total;
            orderWriteCommand.Parameters.Add("@CustomerId", SqlDbType.Int).Value = order.CustomerID;
        }

        public int GetAssignedID(SqlParameter returnParamter)
        {
            int lastAddedEntryID = Convert.ToInt32(returnParamter.Value);
            return lastAddedEntryID;
        }
    }
}