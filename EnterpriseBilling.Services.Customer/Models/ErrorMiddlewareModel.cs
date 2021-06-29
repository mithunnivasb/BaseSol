using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnterpriseBilling.Services.Customer.Models
{
    public class ErrorMiddlewareModel
    {
        public int ErrorCode { get; set; }
        public string Error { get; set; }
        public string ErrorDescription { get; set; }
    }
}
