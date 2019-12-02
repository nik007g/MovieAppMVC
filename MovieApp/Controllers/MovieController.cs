using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieApp.Models;
using MovieApp.Models.DataAccessLayers;
using Microsoft.AspNetCore.Http;

namespace MovieApp.Controllers
{
    public class MovieController : Controller
    {
        IDataAccessLayer objmovie;
        public MovieController(MovieProjectContext context)
        {
            objmovie = new MovieDataAcessLayerEF(context);
        }
        [HttpGet]
        public IActionResult Create()
        {
            if (checkInvalidSession())
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] Movie movie)
        {
            if (checkInvalidSession())
            {
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
                 objmovie.AddMovie(movie);
                return RedirectToAction("Index","Movie");
            }
            return View(movie);
        }
       public IActionResult Index()
        {
            if (checkInvalidSession())
            {
                return RedirectToAction("Index", "Home");
            }
            List<Movie> lastmovie = new List<Movie>();
            lastmovie = objmovie.GetAllMovies().ToList();
            return View(lastmovie);
  
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
            Movie movie = objmovie.GetMovies(id);

            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }
        [HttpPost,ActionName("Delete")]
        public IActionResult DeleteConfirmed(int? id)
        {
            if (checkInvalidSession())
            {
                return RedirectToAction("Index", "Home");
            }
            objmovie.Delete(id);
            return RedirectToAction("Index","Movie");
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (checkInvalidSession())
            {
                return RedirectToAction("Index", "Home");
            }
            if (id == null)
            {
                return NotFound();
            }
            Movie movie = objmovie.GetMovies(id);

            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        [HttpPost]
        public IActionResult Edit(int id, [Bind]Movie movie)
        {
            if (checkInvalidSession())
            {
                return RedirectToAction("Index", "Home");
            }
            if (id != movie.MovieId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                objmovie.Update(movie);
                return RedirectToAction("Index","Movie");
            }
            return View(movie);
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
            HttpContext.Session.Remove("email");
            return RedirectToAction("Index", "Home");
        }
    }
} 
