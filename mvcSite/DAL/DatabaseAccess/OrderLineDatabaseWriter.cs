using mvcSite.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace mvcSite.DAL.DatabaseAccess
{
    public class OrderLineDatabaseWriter : IOrderLineWriter
    {
        private SqlParameter ReturnParameter;

        public void WriteOrderLine(OrderLine orderLine)
        {
            SqlCommand orderLineWriteCommand = new SqlCommand
            {
                Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["database"].ConnectionString),
                CommandType = CommandType.StoredProcedure,
                CommandText = "AddNewOrderLine"
            };

            AddParametersToCommandFromOrderLineData(orderLineWriteCommand, orderLine);

            ReturnParameter = new SqlParameter("@RETURN_VALUE", SqlDbType.Int)
            {
                Direction = ParameterDirection.ReturnValue
            };
            orderLineWriteCommand.Parameters.Add(ReturnParameter);

            orderLineWriteCommand.Connection.Open();

            using (orderLineWriteCommand)
            {
                orderLineWriteCommand.ExecuteReader();
            }
        }

        private void AddParametersToCommandFromOrderLineData(SqlCommand orderLineWriteCommand, OrderLine orderLine)
        {
            orderLineWriteCommand.Parameters.Add("@OrderId", SqlDbType.Int).Value = orderLine.OrderID;
            orderLineWriteCommand.Parameters.Add("@MovieId", SqlDbType.Int).Value = orderLine.MovieID;
            orderLineWriteCommand.Parameters.Add("@Price", SqlDbType.Decimal).Value = orderLine.Price;
            orderLineWriteCommand.Parameters.Add("@Quantity", SqlDbType.TinyInt).Value = orderLine.Quantity;
        }

        public int GetLastWrittenEntryAssignedID()
        {
            int lastAddedEntryID = Convert.ToInt32(ReturnParameter.Value);
            return lastAddedEntryID;
        }
    }
}