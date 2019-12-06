using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MovieApp.Models;
using MovieApp.Models.DataAccessLayers;
using Microsoft.AspNetCore.Http;
using MovieApp.Models.Handlers;
using MediatR;
using AutoMapper;

namespace MovieApp.Controllers
{
    public class MovieController : Controller
    {
      
        private IMediator _mediator;
        private IMapper _mapper;
        
        public MovieController(IMediator mediator,IMapper mapper)
        {
            _mapper = mapper;
            _mediator = mediator;
            
        }

        public IActionResult Index()
        {
           if (checkInvalidSession())
            {
                return RedirectToAction("Index", "Home");
            }
            ViewData["User"] = HttpContext.Session.GetString("Email");
            return View(_mediator.Send(new GetAllMoviesRequestModel()).Result.Movies);

        }
        [HttpPost]
        public ActionResult Index(string username1)
        {
            ViewBag.user = username1;
            return View();
        }
        public IActionResult Create()
        {
            if (checkInvalidSession())
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Create( AddMovieRequestModel movie)
        {
            if (checkInvalidSession())
            {
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
                 _mediator.Send(movie);
                return RedirectToAction("Index","Movie");
            }
        else
            return View(movie);
        }
        public IActionResult Edit(int? id)
        {
            if (checkInvalidSession())
            {
                return RedirectToAction("Index", "Home");
            }
          
            var movie=_mediator.Send(new GetMovieRequestModel {MovieId=id });
            return View(_mapper.Map<UpdateMovieRequestModel>(movie.Result));
        }

        [HttpPost]
        public IActionResult Edit(UpdateMovieRequestModel movie)
        {
            if (checkInvalidSession())
            {
                return RedirectToAction("Index", "Home");
            }
    
                _mediator.Send(movie);
                return RedirectToAction("Index", "Movie");
            
           
        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (checkInvalidSession())
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null)
            {
                return NotFound();
            }
            var movie = _mediator.Send(new GetMovieRequestModel { MovieId = id });
            
            return View(_mapper.Map<Movie>(movie.Result));
        }
        [HttpPost,ActionName("Delete")]
        public IActionResult DeleteConfirmed(int? id)
        {
            if (checkInvalidSession())
            {
                return RedirectToAction("Index", "Home");
            }
            var movie = _mediator.Send(new DeleteMovieRequestModel { MovieId = id });

            return RedirectToAction("Index","Movie");
        }
        public bool checkInvalidSession()
        {
            if (HttpContext.Session.GetString("Email") == null)
            {
                TempData["SessionError"] = "Invalid Access,Login First";
                return true;
            }
            else
                return false;
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("Email");
            return RedirectToAction("Index", "Home");
        }
    }
} 
