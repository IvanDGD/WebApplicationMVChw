using AuthTaskManager.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AuthTaskManager.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Notes");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return RedirectToAction("Register", "Account");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return RedirectToAction("Login", "Account");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

