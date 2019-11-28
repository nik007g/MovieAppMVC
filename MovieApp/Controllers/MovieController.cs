
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieApp.Models;

namespace MovieApp.Controllers
{
    public class MovieController : Controller
    {
       // MovieDataAccessLayer objmovie = new MovieDataAccessLayer();
       // MovieDataAcessLayer2 objmovie;
        IDataAccessLayer objmovie;
        public MovieController(MovieProjectContext context)
        {
            objmovie = new MovieDataAcessLayerEF(context);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] Movie movie)
        {
            if (ModelState.IsValid)
            {
                 objmovie.AddMovie(movie);
                return RedirectToAction("Index","Movie");
            }
            return View(movie);
        }
       public IActionResult Index()
        {
            List<Movie> lastmovie = new List<Movie>();
            lastmovie = objmovie.GetAllMovies().ToList();
            return View(lastmovie);
  
        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
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
            objmovie.Delete(id);
            return RedirectToAction("Index","Movie");
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
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

    }
} 
