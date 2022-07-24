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
    public class CustomerFundsControllerTests
    {
        private readonly CustomerFundsController _controller;
        Mock<CustomerFundsService> servicemock = new Mock<CustomerFundsService>();
        Mock<ILogger<CustomerFunds>> loggerMock = new Mock<ILogger<CustomerFunds>>();

        public CustomerFundsControllerTests()
        {
            _controller = new CustomerFundsController(servicemock.Object, loggerMock.Object);
        }


        [Fact]
        public async Task GetDetails_forSuccess()
        {
            var fid = 1;

            CustomerFunds ExpectedcustomerFund = new CustomerFunds
            {
                Id = fid,
                CustomerId = 1,
                InvestedAmount = 2000,
                InvestedUnits=200,
                FundId =1
            
            };


            servicemock.Setup(x => x.GetDetails(fid)).ReturnsAsync(ExpectedcustomerFund);


            var ActualCustomer = await _controller.Details(fid);
            var result = ActualCustomer as OkObjectResult;

            Assert.IsType<CustomerFunds>(result.Value);
            Assert.Equal(fid, (result.Value as CustomerFunds).Id);

        }


        [Fact]
        public async Task Create_forSuccess()
        {
            var id = 1;

            CustomerFunds ExpectedcustomerFund = new CustomerFunds
            {
                Id = id,
                CustomerId = 2,
                InvestedAmount = 2000,
                InvestedUnits = 200,
                FundId = 1

            };

            servicemock.Setup(x => x.CreateFunds(ExpectedcustomerFund.Id,ExpectedcustomerFund.CustomerId,ExpectedcustomerFund.InvestedAmount,ExpectedcustomerFund
                .FundId)).ReturnsAsync(new { message = "success" });


            var ActualCustomer = await _controller.Create(ExpectedcustomerFund.Id,ExpectedcustomerFund.CustomerId,ExpectedcustomerFund.InvestedAmount,ExpectedcustomerFund.FundId);

            var result = ActualCustomer as OkObjectResult;

            Assert.IsType<OkObjectResult>(result);


        }

        [Fact]
        public async Task Delete_forSuccess()
        {
            var id = 1;

            CustomerFunds ExpectedcustomerFund = new CustomerFunds
            {
                Id = id,
                CustomerId = 2,
                InvestedAmount = 2000,
                InvestedUnits = 200,
                FundId = 1

            };

            servicemock.Setup(x => x.DeleteFunds(id)).ReturnsAsync(new { message = "success" });


            var ActualCustomer = await _controller.DeleteConfirmed(id);

            var result = ActualCustomer as OkObjectResult;

            Assert.IsType<OkObjectResult>(result);


        }


    }
}
