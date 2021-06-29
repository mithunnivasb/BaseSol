using DatabaseOperations.Implementation;
using Eb.Dal.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Eb.Dal.Implementation
{
    public class OrderDetails : IOrderDetails
    {
        public void GetOrderDetails(int orderId, int accountId)
        {
            string connectionString = "";
            string storedProcedure = "";
            SqlParameter[] orderParams = new SqlParameter[] { 
                new SqlParameter(){ParameterName = "@OrderId", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = orderId },
                new SqlParameter(){ParameterName = "@AccountId", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = accountId }
            };


            DataTable dtOrderDetail = DataAccess.getDataTable(connectionString, orderParams, storedProcedure);

            if (dtOrderDetail.Rows.Count>0)
            {

            }

        }

        public void GetOrders(int accountId)
        {
            throw new NotImplementedException();
        }
    }
}
