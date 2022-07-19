using Microsoft.AspNetCore.Mvc;

namespace MVCDeploy.Controllers
{
    public class MainController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
