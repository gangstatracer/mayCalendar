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
            var isAnswerValid = ValidateAnswer(answer, out var error);
            if (isAnswerValid)
            {
                return RedirectToAction("Success");
            }
            return View(new HomeViewModel()
            {
                Answer = answer,
                ValidationError = error,
            });
        }

        private bool ValidateAnswer(string answer, out string validationError)
        {
            validationError = null;

            if (correctAnswers.Contains(Normalize(answer)))
            {
                return true;
            }

            if (!string.IsNullOrEmpty(answer))
            {
                if (answer?.IndexOf(' ') < 0)
                {
                    validationError = "Не забудьте расставить пробелы";
                }
                else
                {
                    validationError = "Неправильно, попробуйте еще раз";
                }
            }
            return false;
        }

        private string Normalize(string original)
        {
            return new Regex(@"[\s\.,'`’]+").Replace(original ?? "", " ")?.ToLowerInvariant().Trim();
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
