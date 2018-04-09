using MayCalendar.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace MayCalendar.Controllers
{
    public class HomeController : Controller
    {
        private readonly string[] correctAnswers;

        public HomeController(IConfiguration configuration)
        {
            correctAnswers = configuration.GetSection("Answers")?.Get<string[]>();
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new HomeViewModel());
        }

        [HttpPost]
        public IActionResult Index(string answer)
        {
            if (IsAnswerCorrect(answer))
            {
                return RedirectToAction("Success");
            }
            return View(new HomeViewModel()
            {
                WereWrongTry = !string.IsNullOrEmpty(answer)
            });
        }

        private bool IsAnswerCorrect(string answer)
        {
            return correctAnswers.Contains(answer.ToLowerInvariant());
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
