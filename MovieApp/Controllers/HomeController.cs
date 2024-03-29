﻿using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieApp.Models;
using MediatR;
using MovieApp.Models.Handlers;

namespace MovieApp.Controllers
{
    public class HomeController : Controller
    {
        private IMediator _mediator;
       
        public HomeController(IMediator mediator)
        { 
            _mediator = mediator;
        }
        [ActionName("Index")] //we given actionname same but it is generally used to give different name to action method than mentioned.
        public ViewResult Index()
        {
            return View();
        }
        [HttpGet]
        public ViewResult User()
        {
            return View();
        }
        [HttpPost]

        public ActionResult User(RequestModel requestModel)
        {
            var result = _mediator.Send(requestModel); 
            if (result.Result.Success)
            {
                HttpContext.Session.SetString("Email", requestModel.Email);
                TempData["User"] = requestModel.Email;
                return RedirectToAction("Index", "Movie");  
            }
            else
            {
                ViewData["Error1"] = "Invalid Details!";
                return View();
            }
        }
        public ViewResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(RegisterRequestModel registerRequestModel)
        {
            if (ModelState.IsValid)
            {
                var result = _mediator.Send(registerRequestModel);
                if (result.Result.success)
                {
                    return View("User");
                }
                else
                {
                    ViewData["Error"] = "Registration Failed User Already Exists";
                    return View();
                }
            }
            else
            {
                return View("Index");
            }
        }
        public ViewResult UpdatePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UpdatePassword(UpdatePasswordRequestModel updatePasswordRequestModel)
        {
           // bool success = UserDataAccessLayer.ChangePassword(user.Email, user.Password);
            var result = _mediator.Send(updatePasswordRequestModel) ;
            if (result.Result.success)
            {
                return RedirectToAction("User", "Home");
            }
            else
            {
                return View();
            }
        }

        public ViewResult Privacy()
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
