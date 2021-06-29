using FluentValidation;
using System;
using System.Data;
using AutoMapper;
using System.Collections.Generic;

namespace EnterpriseBilling.Models
{
    public class CustomerProfile
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public string EmailAddress { get; set; }
    }
}