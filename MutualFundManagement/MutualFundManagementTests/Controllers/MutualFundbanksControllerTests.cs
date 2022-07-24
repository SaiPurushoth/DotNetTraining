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
    public class MutualFundbanksControllerTests
    {

        private readonly MutualFundBanksController _controller;
        Mock<MutualFundBanksService> servicemock = new Mock<MutualFundBanksService>();
        Mock<ILogger<MutualFundBank>> loggerMock = new Mock<ILogger<MutualFundBank>>();


        public MutualFundbanksControllerTests()
        {
            _controller = new MutualFundBanksController(servicemock.Object, loggerMock.Object);
        }


        [Fact]
        public async Task GetDetails_forSuccess()
        {
            var fundid = 1;
            MutualFundBank Expectedfund = new MutualFundBank
            {
                FundId = fundid,
                FundName="poc return policy",
                NAV=15,
                TotalInvestment=3000,
                TotalUnits=15



            };
            servicemock.Setup(x => x.GetDetails(fundid)).ReturnsAsync(Expectedfund);

            var ActualCustomer = await _controller.Details(fundid);
            var result = ActualCustomer as OkObjectResult;

            Assert.IsType<MutualFundBank>(result.Value);

            Assert.Equal(fundid, (result.Value as MutualFundBank).FundId);
         

        }

        [Fact]

        public async Task Create_forSuccess()
        {

            var fundid = 1;
            MutualFundBank Expectedfund = new MutualFundBank
            {
                FundId = fundid,
                FundName = "poc return policy",
                NAV = 15,
                TotalInvestment = 3000,
                TotalUnits = 15

            };

            servicemock.Setup(x => x.CreateMutualFund(Expectedfund.FundId,Expectedfund.FundName,Expectedfund.NAV)).ReturnsAsync(new { message = "success" });

            var ActualCustomer = await _controller.Create(Expectedfund.FundId,Expectedfund.FundName,Expectedfund.NAV);

            var result = ActualCustomer as OkObjectResult;

            Assert.IsType<OkObjectResult>(result);


        }

        [Fact]
        public async Task Delete_forSuccess()
        {

            var fundid = 1;
            MutualFundBank Expectedfund = new MutualFundBank
            {
                FundId = fundid,
                FundName = "poc return policy",
                NAV = 15,
                TotalInvestment = 3000,
                TotalUnits = 15

            };

            servicemock.Setup(x => x.DeleteMutualFund(fundid)).ReturnsAsync(new { message = "success" });

            var ActualCustomer = await _controller.DeleteConfirmed(fundid);

            var result = ActualCustomer as OkObjectResult;

            Assert.IsType<OkObjectResult>(result);


        }



    }



}
