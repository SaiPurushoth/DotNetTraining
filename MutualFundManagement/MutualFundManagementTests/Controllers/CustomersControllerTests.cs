using Castle.Core.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using MutualFundManagement.Controllers;
using MutualFundManagement.Models;
using MutualFundManagement.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MutualFundManagementTests.Services
{
    public class CustomersControllerTests
    {

        private readonly CustomersController _controller;
        Mock<CustomerService> servicemock = new Mock<CustomerService>(); 
        Mock<ILogger<Customers>> loggerMock = new Mock<ILogger<Customers>>();
        public CustomersControllerTests()
        {
            _controller = new CustomersController(servicemock.Object,loggerMock.Object);

        }



        [Fact]
        public async Task GetDetails_forSuccess()
        {
            var customerid = 1;
            Customers Expectedcustomer = new Customers
            {
                CustomerId = customerid,
                CustomerName="sai",
                Email="sai@gmail.com",
                Password="gsp@2000",
                IsAdmin=false,
                  

            };

            servicemock.Setup(x=>x.GetDetails(customerid)).ReturnsAsync(Expectedcustomer); 
            
            var ActualCustomer = await _controller.Details(customerid);

            var result = ActualCustomer as OkObjectResult;

            Assert.IsType<Customers>(result.Value);
            Assert.Equal(customerid, (result.Value as Customers).CustomerId);

        }

        [Fact]
        public async Task Create_forSuccess()
        {
            var customerid = 1;
            Customers Expectedcustomer = new Customers
            {
                CustomerId = customerid,
                CustomerName = "sai",
                Email = "sai@gmail.com",
                Password = "gsp@2000",
                IsAdmin = false,


            };

            servicemock.Setup(x => x.CreateCustomer(Expectedcustomer)).ReturnsAsync(new {message ="success"});
            var ActualCustomer = await _controller.Register(Expectedcustomer);

            var result = ActualCustomer as OkObjectResult;

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Delete_forSuccess()
        {
            var customerid = 1;
            Customers Expectedcustomer = new Customers
            {
                CustomerId = customerid,
                CustomerName = "sai",
                Email = "sai@gmail.com",
                Password = "gsp@2000",
                IsAdmin = false,


            };

            servicemock.Setup(x => x.DeleteCustomer(customerid)).ReturnsAsync(new { message = "success" });

            var ActualCustomer = await _controller.DeleteConfirmed(customerid);

            var result = ActualCustomer as OkObjectResult;

            Assert.IsType<OkObjectResult>(result);


        }





    }




}
