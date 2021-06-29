using EnterpriseBilling.Models;
using EnterpriseBilling.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Net;

namespace EnterpriseBilling.Services.Customer.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerInfoController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<CustomerInfoController> _logger;
        private readonly ICustomerRepository _customerRepository;
        public CustomerInfoController(ILogger<CustomerInfoController> logger, ICustomerRepository customerRepository)
        {
            _logger = logger;
            _customerRepository = customerRepository;
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IEnumerable<CustomerProfile> GetList()
        {

            return _customerRepository.GetCustomers();

        }
    }
}
