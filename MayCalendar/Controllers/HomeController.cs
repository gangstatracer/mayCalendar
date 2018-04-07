using MayCalendar.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace MayCalendar.Controllers
{
    public class HomeController : Controller
    {
        private readonly string correctAnswer;

        public HomeController(IConfiguration configuration)
        {
            correctAnswer = configuration.GetSection("Answer")?.Value;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new HomeViewModel());
        }

        [HttpPost]
        public IActionResult Index(string answer)
        {
            if (answer?.ToLowerInvariant() == correctAnswer?.ToLowerInvariant())
            {
                return RedirectToAction("Success");
            }
            return View(new HomeViewModel()
            {
                WereWrongTry = !string.IsNullOrEmpty(answer)
            });
        }

        [HttpGet]
        [Route("success")]
        public IActionResult Success()
        {
            return View();
        }

        [Route("error/{code:int}")]
        public IActionResult Error()
        {
            return View();
        }
    }
}
