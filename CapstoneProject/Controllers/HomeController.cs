using CapstoneProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;

namespace CapstoneProject.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        // Inject logger for diagnostics
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // Home page (dashboard)
        public IActionResult Index()
        {
            return View();
        }

        // Privacy policy page
        public IActionResult Privacy()
        {
            return View();
        }

        // Shown when user is denied access
        public IActionResult AccessDenied()
        {
            return View();
        }

        // Error page with request ID for diagnostics
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var requestId = Activity.Current?.Id ?? HttpContext?.TraceIdentifier ?? string.Empty;
            return View(new ErrorViewModel { RequestId = requestId });
        }
    }
}
