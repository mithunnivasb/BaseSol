using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnterpriseBilling.DataAccess
    {
    public class AppConfiguration
    {
        public string ConnectionString { get; set; }
    }
}
