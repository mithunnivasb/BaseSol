

using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Linq;
using System.Security.Claims;

namespace EnterpriseBilling.Extensions
{
    public class FluentValidationInterceptor : IValidatorInterceptor
    {
        private readonly ILogger<FluentValidationInterceptor> _logger;
        public FluentValidationInterceptor(ILogger<FluentValidationInterceptor> logger)
        {
            _logger = logger;
        }

        public ValidationResult AfterAspNetValidation(ActionContext actionContext, IValidationContext validationContext, ValidationResult result)
        {
            if (!result.IsValid)
            {
                int userId = 0;
                var obj = validationContext.InstanceToValidate;

                int.TryParse(actionContext.HttpContext.User.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"), out userId);

                _logger.LogError($"{JsonConvert.SerializeObject(obj)} - {string.Join(";", result.Errors.Select(item => item.ErrorMessage))}");

            }
            return result;
        }        

        public IValidationContext BeforeAspNetValidation(ActionContext actionContext, IValidationContext commonContext)
        {
            return commonContext;
        }
    }
}
