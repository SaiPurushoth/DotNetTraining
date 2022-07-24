using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MutualFundManagement.Models;
using MutualFundManagement.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MutualFundManagement.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class CustomersController : ControllerBase
    {



        private readonly CustomerService _customerService;

        private readonly ILogger<Customers> _serilog;
        public CustomersController(CustomerService customerService, ILogger<Customers> serilog)
        {
            _customerService = customerService;
            _serilog = serilog; 
        }


        [HttpGet]
        [Authorize]
        [Route("[Action]")]
        public async Task<IActionResult> Index()
        {
            return Ok(_customerService.AllValues());
        }



        [HttpPost]
        [Route("[Action]")]
        public async Task<IActionResult> Login(CustomerDTO user)
        {
            var customerObject = await _customerService.Verify(user.Email, user.Password);
            if (customerObject == null)
            {
                return BadRequest("Data Not Present");
            }
            var token = createToken((Customers)customerObject);
            return Ok(token);
        }



        [HttpPost]
        [Route("[Action]")]
        public async Task<IActionResult> Register(Customers customers)
        {
            if (ModelState.IsValid)
            {
                var customerObject = await _customerService.CreateCustomer(customers);
                if (customerObject == null)
                {
                    return BadRequest("Data Not Present");
                }

                var token = createToken((Customers)customerObject);
                return Ok(token);

            }
            _serilog.LogError("customer cannot be created due to constraints not met");

            return BadRequest("Constraints not met");


        }


        [HttpPost]
        [Authorize]
        [Route("[Action]/{id}")]
        public async Task<IActionResult> Edit(int id, string CustomerName, string Email, string Password, bool isAdmin)
        {


            if (ModelState.IsValid)
            {
                var customerObject = await  _customerService.EditCustomer(id,CustomerName,Email,Password,isAdmin);
                if (customerObject == null)
                {
                    return BadRequest("Data Not Present");
                }
                return Ok(new {message = "success"});

            }
            _serilog.LogError("customer cannot be created due to constraints not met");
            return BadRequest("Constraints not met");
        }



        [HttpGet]
        [Authorize]
        [Route("[Action]")]
        public async Task<IActionResult> Details(int id)
        {
            var customerObject = await _customerService.GetDetails(id);
            if (customerObject == null)
            {
                return BadRequest("Data Not Present");
            }

            return Ok(customerObject);


        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [Route("[Action]")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customerObject = await _customerService.DeleteCustomer(id);
            if (customerObject == null)
            {
                return BadRequest("Data Not Present");
            }
            return Ok(new {message = "success"});

        }



        private string createToken(Customers obj)
        {

            List<Claim> claims;

            if (obj.IsAdmin == false)
            {
                claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,obj.Email),

            };
            }
            else
            {
                claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,obj.Email),

                new Claim(ClaimTypes.Role,"Admin")

            };
            }

        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("This is my top secret key for jwt token"));


            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials:cred
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

       
    }
}
