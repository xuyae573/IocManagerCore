using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Example.Controllers
{
    public class GreetController : Controller
    {
        private readonly ILogger<GreetController> _logger;

        private readonly IGreetingService _greetingService;

        public GreetController(ILogger<GreetController> logger, IGreetingService greetingService)
        {
            _logger = logger;
            _greetingService = greetingService;
        }
        public IActionResult Index()
        {
            var content = _greetingService.SayHello();
            return Content(content);
        }
    }
}
