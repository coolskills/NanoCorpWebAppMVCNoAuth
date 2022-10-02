using Microsoft.AspNetCore.Mvc;
using NanoCorpWebAppMVCNoAuth.Models;
using System.Diagnostics;
using Microsoft.Extensions.Localization;

namespace NanoCorpWebAppMVCNoAuth.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IStringLocalizer<HomeController> _localizer;

        public HomeController(ILogger<HomeController> logger, IStringLocalizer<HomeController> localizer)
        {
            _logger = logger;
            _localizer = localizer;
        }

        public IActionResult Index()
        {
            ViewData["Welcome"] = _localizer["Welcome"];
            ViewData["Title"] = _localizer["Home Page"];
            ViewData["Message"] = _localizer["HelloHtml", "Adriano"];

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Jobs()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
