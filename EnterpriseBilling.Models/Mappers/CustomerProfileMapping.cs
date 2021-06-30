using AutoMapper;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace EnterpriseBilling.Models.Mappers

{
    public class CustomerProfileMapping : Profile
    {
        public CustomerProfileMapping()
        {
            CreateMap<DataRow, CustomerProfile>()
             .ForMember(dest => dest.CustId, orig => orig.MapFrom(row => (int)row["CustId"]))
             .ForMember(dest => dest.AccountManagerName, orig => orig.MapFrom(row => row["AccountManagerName"].ToString()))
             .ForMember(dest => dest.AddressDetails, orig => orig.MapFrom(row => row["AddressDetails"].ToString()))
             .ForMember(dest => dest.CompanyName, orig => orig.MapFrom(row => row["CompanyName"].ToString()));
        }
    }
}
