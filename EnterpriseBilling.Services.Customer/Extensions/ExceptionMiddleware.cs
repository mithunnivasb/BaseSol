using EnterpriseBilling.Services.Customer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace EnterpriseBilling.Extensions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHostEnvironment _environment;
        private readonly ILogger<ExceptionMiddleware> _logger;
        public ExceptionMiddleware(RequestDelegate next, IHostEnvironment environment, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _environment = environment;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                //Logging logic goes here
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            //incase of exception, middleware will wrap the exception with status code 500            

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var error = new ErrorMiddlewareModel() { Error= "Internal Server Error.", ErrorCode = context.Response.StatusCode };

            if (_environment.IsDevelopment())
            {
                error.ErrorDescription = exception.Message;                
            }
            else
            {
                error.ErrorDescription = error.Error;
            }

            await context.Response.WriteAsync(JsonConvert.SerializeObject(error));
        }
    }

    
}
