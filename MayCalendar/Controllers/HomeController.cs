using Microsoft.AspNetCore.Mvc;

namespace MayCalendar.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string answer)
        {
            return View();
        }
    }
}
