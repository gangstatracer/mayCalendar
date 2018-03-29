using Microsoft.AspNetCore.Mvc;

namespace MayCalendar.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
