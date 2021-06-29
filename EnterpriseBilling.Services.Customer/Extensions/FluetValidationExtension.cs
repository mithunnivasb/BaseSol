using EnterpriseBilling.Models.Validators;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace EnterpriseBilling.Extensions
{
    public static class FluetValidationExtension
    {
        public static IMvcBuilder AddValidator(this IMvcBuilder builder)
        {
            builder.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CustomerProfileValidator>());
            return builder;
        }
    }
}
