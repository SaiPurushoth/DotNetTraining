using MassTransit;
using Microsoft.AspNetCore.Mvc;
using ProducerApi.MessageBrokers;
using ProducerApi.Models;

namespace ProducerApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {
 
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create(Students student)
        {
            if (student != null)
            {
                RabbitMQProducer mq = new();

                mq.SendMessage(student);
                return Ok();
            }
            return BadRequest();
        }
    }
}
