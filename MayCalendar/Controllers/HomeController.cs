using MayCalendar.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Text.RegularExpressions;

namespace MayCalendar.Controllers
{
    public class HomeController : Controller
    {
        private readonly string[] correctAnswers;
        private readonly ILogger<HomeController> logger;

        public HomeController(IConfiguration configuration, ILogger<HomeController> logger)
        {
            this.logger = logger;
            correctAnswers = configuration.GetSection("Answers")
                ?.Get<string[]>()
                .Select(s => Normalize(s))
                .ToArray();
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
            return correctAnswers.Contains(Normalize(answer));
        }

        private string Normalize(string original)
        {
            return new Regex(@"[\s\.,'`’]+").Replace(original ?? "", " ")?.ToLowerInvariant();
        }

        [HttpGet]
        [Route("success")]
        public IActionResult Success()
        {
            return View();
        }

        [Route("error/{code:int}")]
        public IActionResult Error(int code)
        {
            var exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
            if(exception != null)
            {
                logger.LogError(exception, $"Error {code} occured");
            }
            return View();
        }
    }
}
