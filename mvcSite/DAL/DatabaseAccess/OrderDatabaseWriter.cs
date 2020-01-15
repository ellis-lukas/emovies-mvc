using mvcSite.Models;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace mvcSite.DAL.DatabaseAccess
{
    public class OrderDatabaseWriter : IOrderWriter
    {
        private SqlParameter ReturnParameter;

        public void WriteOrder(Order order)
        {
            SqlCommand orderWriteCommand = new SqlCommand
            {
                Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["database"].ConnectionString),
                CommandType = CommandType.StoredProcedure,
                CommandText = "AddNewOrder"
            };

            AddParametersToCommandFromOrderData(orderWriteCommand, order);

            ReturnParameter = new SqlParameter("@RETURN_VALUE", SqlDbType.Int)
            {
                Direction = ParameterDirection.ReturnValue
            };
            orderWriteCommand.Parameters.Add(ReturnParameter);

            orderWriteCommand.Connection.Open();

            using (orderWriteCommand)
            {
                orderWriteCommand.ExecuteReader();
            }
        }

        private void AddParametersToCommandFromOrderData(SqlCommand orderWriteCommand, Order order)
        {
            orderWriteCommand.Parameters.Add("@DateCreated", SqlDbType.DateTime).Value = DateTime.Now;
            orderWriteCommand.Parameters.Add("@Total", SqlDbType.Decimal).Value = order.Total;
            orderWriteCommand.Parameters.Add("@CustomerId", SqlDbType.Int).Value = order.CustomerID;
        }

        public int GetLastWrittenEntryAssignedID()
        {
            int lastAddedEntryID = Convert.ToInt32(ReturnParameter.Value);
            return lastAddedEntryID;
        }
    }
}