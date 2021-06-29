using AutoMapper;
using EnterpriseBilling.Models;
using EnterpriseBilling.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;

namespace EnterpriseBilling.DataAccess.Implementation
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IMapper _mapper;
        private AppConfiguration _appSettings;
        private readonly IDataAcessService _dataAcessService;

        public CustomerRepository(IMapper mapper, AppConfiguration appSettings, IDataAcessService dataAcessService) {
            _mapper = mapper;
            _appSettings = appSettings;
            _dataAcessService = dataAcessService;
        }

        public List<CustomerProfile> GetCustomers()
        {            
            string storedProcedure = "GetCustomers";            
            DataTable Customers = _dataAcessService.getDataTable(_appSettings.ConnectionString, null, storedProcedure);
            List<CustomerProfile> result = new List<CustomerProfile>();
            if (Customers.Rows.Count > 0)
            {
                foreach (DataRow row in Customers.Select()) {
                    result.Add(_mapper.Map<DataRow, CustomerProfile>(row));
                }
            }
            return result;
        }


        public CustomerProfile GetCustomer(int accountId)
        {
            throw new NotImplementedException();
        }
    }
}
