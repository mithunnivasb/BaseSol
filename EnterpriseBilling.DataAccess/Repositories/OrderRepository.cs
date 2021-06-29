using EnterpriseBilling.Models.Interfaces;
using System;

namespace EnterpriseBilling.DataAccess.Implementation
{
    public class OrderRepository : IOrderRepository
    {
        public void GetOrderDetails(int orderId, int accountId)
        {
            //string connectionString = "";
            //string storedProcedure = "";
            //SqlParameter[] orderParams = new SqlParameter[] { 
            //    new SqlParameter(){ParameterName = "@OrderId", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = orderId },
            //    new SqlParameter(){ParameterName = "@AccountId", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Input, Value = accountId }
            //};


            //DataTable dtOrderDetail = DataAccessService.getDataTable(connectionString, orderParams, storedProcedure);

            //if (dtOrderDetail.Rows.Count>0)
            //{

            //}

        }

        public void GetOrders(int accountId)
        {
            throw new NotImplementedException();
        }
    }
}
