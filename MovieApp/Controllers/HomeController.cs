using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieApp.Models;

namespace MovieApp.Controllers
{
    public class HomeController : Controller
    {
        UserDataAccessLayer UserDataAccessLayer = new UserDataAccessLayer();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult User()
        {
            return View();
        }
        [HttpPost]
        public ActionResult User(User user)
        {
            bool success = UserDataAccessLayer.CheckLogin(user);
            if (success)
            {
                HttpContext.Session.SetString("Email", user.Email);

                return RedirectToAction("Index", "Movie");
            }
            else
            {
                ViewData["Error"] = "Invalid Details !";
                return View();
            }
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(User user)
        {

            if (ModelState.IsValid)
            {
                bool success = UserDataAccessLayer.AddUser(user);
                if (success)
                {
                    UserDataAccessLayer.AddUser(user);
                    return View("User");
                }
                else
                {
                    ViewData["Error"] = "Registration Failed User Already Exists";
                    return View("User");
                }
            }
            else
            {
                return View("Index");
            }

        }
        public IActionResult UpdatePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UpdatePassword(User user)
        {
            bool success = UserDataAccessLayer.ChangePassword(user.Email, user.Password);
            if (success)
            {
                return RedirectToAction("User", "Home");
            }
            else
            {
                return View();
            }
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
