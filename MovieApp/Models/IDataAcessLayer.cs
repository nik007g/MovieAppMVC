using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApp.Models
{
    interface IDataAcessLayer
    {
        public void AddMovie(Movie movie);
        public IEnumerable<Movie> GetAllMovies();
        public void Delete(int? id);
        public void Update(Movie movie);
        public Movie GetMovies(int? id);
       

    }
}
