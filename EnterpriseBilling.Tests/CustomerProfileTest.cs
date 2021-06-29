using EnterpriseBilling.Models.Interfaces;
using EnterpriseBilling.Services.Customer.Controllers;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace EnterpriseBilling.Tests
{
    public class CustomerProfileTest
    {
        private CustomerInfoController _customerInfoController;
        private Mock<ILogger<CustomerInfoController>> _loggerMock;
        private Mock<ICustomerRepository> _customerRepositoryMock;

        [SetUp]
        public void Setup()
        {
            _loggerMock = new Mock<ILogger<CustomerInfoController>>();
            _customerRepositoryMock = new Mock<ICustomerRepository>();
            _customerInfoController = new CustomerInfoController(_loggerMock.Object, _customerRepositoryMock.Object);
        }

        [Test]
        public void GetCustomerProfileTest()
        {
            var customerList = new System.Collections.Generic.List<Models.CustomerProfile>(){
                new Models.CustomerProfile() {
                    Id=1,
                    FirstName="FName",
                    LastName="LName",
                    EmailAddress="test@test.com"
                }};

            _customerRepositoryMock.Setup(x => x.GetCustomers()).Returns(customerList);

            var result = _customerInfoController.GetList();

            Assert.AreEqual(customerList,result);
        }
    }
}