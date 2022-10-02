using Microsoft.AspNetCore.Mvc;
using NanoCorpWebAppMVCNoAuth.Models;
using System.Diagnostics;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;

namespace NanoCorpWebAppMVCNoAuth.Controllers
{
    public class PersonController : Controller
    {
        private readonly ILogger<PersonController> _logger;
        private readonly IStringLocalizer<PersonController> _localizer;

        public PersonController(ILogger<PersonController> logger, IStringLocalizer<PersonController> localizer)
        {
            _logger = logger;
            _localizer = localizer;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = _localizer["Person Title"];

            return View();
        }

        [HttpPost]
        public IActionResult Index(Person person)
        {
            return View();
        }

        public record Person([Required] string Name, [Range(0, 150)] int Age);
    }
}
