using FluentValidation;
using System;
using System.Data;
using AutoMapper;
using System.Collections.Generic;

namespace EnterpriseBilling.Models
{
    public class CustomerProfile
    {
        public int CustId { get; set; }
        public string AccountManagerName { get; set; }
        public string AddressDetails { get; set; }
        public string CompanyName { get; set; }
    
    }
}