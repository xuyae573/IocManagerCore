using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Example.Models;
using IocManagerCore;
using System.Runtime.Serialization;

namespace Example.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IGreetingService _greetingService;

        public HomeController(ILogger<HomeController> logger, IGreetingService greetingService)
        {
            _logger = logger;
            _greetingService = greetingService;
        }

        public IActionResult Index()
        {
            var content = _greetingService.SayHello();
            return Content(content);
        }

        public IActionResult Privacy()
        {
            var content = IocManager.Instance.GetService<IGreetingService>().SayHello();
            return Content(content);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
