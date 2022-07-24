using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MutualFundManagement.Models;
using MutualFundManagement.Services;

namespace MutualFundManagement.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class CustomerFundsController : ControllerBase
    {

        private readonly CustomerFundsService _customerFundsService;
        private readonly ILogger<CustomerFunds> _serilog;

        public CustomerFundsController(CustomerFundsService customerFundsService, ILogger<CustomerFunds> serilog)
        {
            _customerFundsService = customerFundsService;
            _serilog = serilog;
        }

        [HttpGet]
        [Route("[Action]")]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return Ok(_customerFundsService.AllValues());
        }



        [HttpPost]
        [Route("[Action]")]
        [Authorize]
        public async Task<IActionResult> Create(int Id,int CustomerId,float InvestedAmout,int FundId)
        {
           
            if (ModelState.IsValid)
            {
                var FundObject = await _customerFundsService.CreateFunds(Id,CustomerId,InvestedAmout,FundId);
                    if (FundObject == null)
                {
                    return BadRequest("Data Not Present");
                }

                return Ok(FundObject);
            }
            _serilog.LogError("customer cannot be created due to constraints not met");
            return BadRequest("Constraints not met");
        }




        [HttpPost]
        [Route("[Action]/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, int CustomerId, float InvestedAmout, int FundId)
        {


            if (ModelState.IsValid)
            {
                var FundObject = await _customerFundsService.EditFunds(id,CustomerId,InvestedAmout,FundId);
                if (FundObject == null)
                {
                    return BadRequest("Data Not Present");
                }

                return Ok(FundObject);
            }
            _serilog.LogError("customerFund cannot be created due to constraints not met");
            return BadRequest("Constraints not met");
        }

        [HttpGet]
        [Route("[Action]")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int id)
        {
            var FundObject = await _customerFundsService.GetDetails(id);
            if (FundObject == null)
            {
                return BadRequest("Data Not Present");
            }

            return Ok(FundObject);
        }


        [HttpPost, ActionName("Delete")]
        [Route("[Action]")]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var FundObject = await _customerFundsService.DeleteFunds(id);
            if (FundObject == null)
            {
                return BadRequest("Data Not Present");
            }

            return Ok(FundObject);
        }


    }
}
