using System.Collections.Generic;
using System.Linq;

namespace MovieApp.Models.DataAccessLayers
{
    public class MovieDataAcessLayerEF : IMovieDataAccessLayer
    {
        MovieProjectContext movieAppDBContext;

        public MovieDataAcessLayerEF(MovieProjectContext context)
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