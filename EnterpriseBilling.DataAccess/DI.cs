using AutoMapper;
using AutoMapper.Data;
using EnterpriseBilling.DataAccess.Implementation;
using EnterpriseBilling.Models.Interfaces;
using EnterpriseBilling.Models.Mappers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EnterpriseBilling.DataAccess
{
    public static class DI
    {
        public static IServiceCollection AddInfratructure(this IServiceCollection services, IConfiguration configuration) 
        {
            var config = new AppConfiguration();
            configuration.GetSection("AppSettings").Bind(config);
            services.AddSingleton(config);
            services.AddTransient<IDataAcessService, DataAccessService>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            return services;
        }

        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            var mapperconfig = new MapperConfiguration(mc =>
            {
                mc.AddDataReaderMapping();
                mc.AddProfile(new CustomerProfileMapping());
            });
            IMapper mapper = mapperconfig.CreateMapper();
            services.AddSingleton(mapper);
            return services;
        }
    }
}
