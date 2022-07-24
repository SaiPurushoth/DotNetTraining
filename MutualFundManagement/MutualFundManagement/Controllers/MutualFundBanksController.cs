using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MutualFundManagement.Models;
using MutualFundManagement.Services;

namespace MutualFundManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MutualFundBanksController : ControllerBase
    {
        private readonly MutualFundBanksService _mutualFundBanksService;

        private readonly ILogger<MutualFundBank> _serilog;

        public MutualFundBanksController(MutualFundBanksService mutualFundBanksService, ILogger<MutualFundBank> serilog)
        {
          _mutualFundBanksService = mutualFundBanksService;
            _serilog = serilog;
        }

 

        [HttpGet]
        [Route("[Action]")]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return Ok(_mutualFundBanksService.AllValues());
                         
        }


 



        [HttpPost]
       [Route("[Action]")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(int FundId,string FundName,float NAV)
        {


            if (ModelState.IsValid)
            {

                var MutualFundObject = await _mutualFundBanksService.CreateMutualFund(FundId,FundName,NAV);
                if (MutualFundObject == null)
                {
                    return BadRequest("Data Not Present");
                }
                return Ok(MutualFundObject);
            }

            _serilog.LogError("Fund cannot be created due to constraints not met");
            return BadRequest("Constraints not met");
        }

    
        [HttpPost]
        [Route("[Action]/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id,float NAV)
        {

            if (ModelState.IsValid)
            {
                var MutualFundObject =await  _mutualFundBanksService.EditMutualFund(id,NAV);
                if (MutualFundObject == null)
                {
                    return BadRequest("Data Not Present");
                }
                return Ok(MutualFundObject);

            }
            _serilog.LogError("Fund cannot be created due to constraints not met");
            return BadRequest("Constraints not met");
        }

        [HttpGet]
        [Route("[Action]")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int id)
        {
            var MutualFundObject = await _mutualFundBanksService.GetDetails(id);
            if (MutualFundObject == null)
            {
                return BadRequest("Data Not Present");
            }

            return Ok(MutualFundObject);

        }

        [HttpPost, ActionName("Delete")]
        [Route("[Action]")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
           var MutualFundObject = await _mutualFundBanksService.DeleteMutualFund(id);
            if (MutualFundObject == null)
            {
                return BadRequest("Data Not Present");
            }
            return Ok(MutualFundObject);

        }


    }
}
