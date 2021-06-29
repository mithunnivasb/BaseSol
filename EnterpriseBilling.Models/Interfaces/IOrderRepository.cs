using System;
using System.Collections.Generic;
using System.Text;

namespace EnterpriseBilling.Models.Interfaces
{
    public interface IOrderRepository
    {
        void GetOrders(int accountId);
        void GetOrderDetails(int orderId, int accountId);
    }
}
