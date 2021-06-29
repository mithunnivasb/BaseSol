using System;
using System.Collections.Generic;
using System.Text;

namespace Eb.Dal.Interfaces
{
    public interface IOrderDetails
    {
        void GetOrders(int accountId);
        void GetOrderDetails(int orderId, int accountId);
    }
}
