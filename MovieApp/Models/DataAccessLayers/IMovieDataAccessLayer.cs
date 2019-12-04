using System.Collections.Generic;

namespace MovieApp.Models.DataAccessLayers
{
    public interface IMovieDataAccessLayer
    {
        public void AddMovie(Movie movie);
        public IEnumerable<Movie> GetAllMovies();
        public void Delete(int? id);
        public bool Update(Movie movie);
        public Movie GetMovies(int? id);

    }
}
