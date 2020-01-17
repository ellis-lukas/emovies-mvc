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
        public void WriteOrderLine(OrderLine orderLine)
        {
            SqlConnection databaseConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["database"].ConnectionString);

            using (databaseConnection)
            {
                databaseConnection.Open();

                SqlCommand orderLineWriteCommand = new SqlCommand
                {
                    Connection = databaseConnection,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "AddNewOrderLine"
                };

                AddParametersToCommandFromOrderLineData(orderLineWriteCommand, orderLine);

                SqlParameter returnParameter = new SqlParameter("@RETURN_VALUE", SqlDbType.Int)
                {
                    Direction = ParameterDirection.ReturnValue
                };
                orderLineWriteCommand.Parameters.Add(returnParameter);

                using (orderLineWriteCommand)
                {
                    orderLineWriteCommand.ExecuteReader();
                }
            }
        }

        private void AddParametersToCommandFromOrderLineData(SqlCommand orderLineWriteCommand, OrderLine orderLine)
        {
            orderLineWriteCommand.Parameters.Add("@OrderId", SqlDbType.Int).Value = orderLine.OrderID;
            orderLineWriteCommand.Parameters.Add("@MovieId", SqlDbType.Int).Value = orderLine.MovieID;

            orderLineWriteCommand.Parameters.Add("@Price", SqlDbType.Decimal);
            orderLineWriteCommand.Parameters["@Price"].Precision = 19;
            orderLineWriteCommand.Parameters["@Price"].Scale = 4;
            orderLineWriteCommand.Parameters["@Price"].Value = orderLine.Price;

            orderLineWriteCommand.Parameters.Add("@Quantity", SqlDbType.TinyInt).Value = orderLine.Quantity;

            

        }
    }
}