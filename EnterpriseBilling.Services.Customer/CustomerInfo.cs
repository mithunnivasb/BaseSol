using System;

namespace EnterpriseBilling.Services.Customer
{
    public class CustomerInfo
    {
        public DateTime SignUpDate { get; set; }

        public int CustomerID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }
    }
}
