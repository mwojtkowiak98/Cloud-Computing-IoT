using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ChatApp2.Web.Models;
using Microsoft.AspNetCore.Http;

namespace ChatApp2.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new SignInVMcs());
        }

        [HttpPost]
        public IActionResult Index(SignInVMcs vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            HttpContext.Session.SetString("UserName", vm.UserName);
            return RedirectToAction("Chat");
        }

        [HttpGet("chat")]
        public IActionResult Chat()
        {
            var UserName = HttpContext.Session.GetString("UserName");
            if (string.IsNullOrEmpty(UserName))
            {
                return RedirectToAction("Index");
            }

            return View("Chat", UserName);
        }

        public IActionResult Privacy()
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
