using Microsoft.AspNetCore.Mvc;

namespace Backend_Api.Controllers
{


    public class HomeController : Controller
    {

        [HttpGet]
        public JsonResult GetUsers(int id)

        {
            if (id == 0)
            {
                return Json(new {error ="Error loading"});
            }
            else
            {
                return Json(new { success = "Valid" });
            }
        }
    }
}
