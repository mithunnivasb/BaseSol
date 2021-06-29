using EnterpriseBilling.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnterpriseBilling.Models.Interfaces
{
    public interface ICustomerRepository
    {
        List<CustomerProfile> GetCustomers();
        CustomerProfile GetCustomer(int accountId);
    }
}
