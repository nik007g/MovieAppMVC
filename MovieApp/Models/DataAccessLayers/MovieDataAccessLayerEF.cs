using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Text.RegularExpressions;

namespace MovieApp.Models.DataAccessLayers
{
    public class MovieDataAcessLayerEF : IMovieDataAccessLayer
    {
        MovieProjectContext movieAppDBContext;

        public MovieDataAcessLayerEF(MovieProjectContext context)
        {
            movieAppDBContext = context;
        }

        public bool AddMovie(Movie movie)
        {
            if (movie.Rating > 10 || movie.Rating < 1)
            {
                throw new ArgumentOutOfRangeException("Rating Should be in between 0-10");
            }
            else if (String.IsNullOrEmpty(movie.MovieName))
            {
                throw new ArgumentOutOfRangeException("Movie Name Can not be null");
            }
            movieAppDBContext.Add(movie);
            movieAppDBContext.SaveChanges();
           return true;
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

        public bool Update(Movie movie)
        {
            movieAppDBContext.Update(movie);
            movieAppDBContext.SaveChanges();
            return true;
        }

        public Movie GetMovies(int? id)
        {
            Movie movie = movieAppDBContext.Movie.Find(id);
            return movie;
        }

    }
}