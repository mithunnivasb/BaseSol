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
             .ForMember(dest => dest.Id, orig => orig.MapFrom(row => (int)row["Id"]))
             .ForMember(dest => dest.FirstName, orig => orig.MapFrom(row => row["FirstName"].ToString()))
             .ForMember(dest => dest.LastName, orig => orig.MapFrom(row => row["LastName"].ToString()))
             .ForMember(dest => dest.CompanyName, orig => orig.MapFrom(row => row["Company"].ToString()))
             .ForMember(dest => dest.EmailAddress, orig => orig.MapFrom(row => row["Email"].ToString()));
        }
    }
}
