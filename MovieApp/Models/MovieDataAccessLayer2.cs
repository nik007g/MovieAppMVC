using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApp.Models
{
    public class MovieDataAcessLayer2 :IDataAcessLayer
    {
        MovieProjectContext movieAppDBContext;

        public MovieDataAcessLayer2(MovieProjectContext context)
        {
            movieAppDBContext = context;
        }

        public void AddMovie(Movie movie)
        {
            movieAppDBContext.Add(movie);
            movieAppDBContext.SaveChanges();
        }

        public IEnumerable<Movie> GetAllMovies()
        {
            return movieAppDBContext.Movie.ToList(); ;
        }

        public void Delete(int? id)
        {
            var movie = movieAppDBContext.Movie.Find(id);
            movieAppDBContext.Movie.Remove(movie);
            movieAppDBContext.SaveChanges();
        }

        public void Update(Movie movie)
        {
            movieAppDBContext.Update(movie);
            movieAppDBContext.SaveChanges();
        }

        public Movie GetMovies(int? id)
        {
            Movie movie = movieAppDBContext.Movie.Find(id);
            return movie;
        }
    }
}