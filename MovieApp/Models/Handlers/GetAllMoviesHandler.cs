using MediatR;
using MovieApp.Models.DataAccessLayers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MovieApp.Models.Handlers
{
    public class GetAllMoviesRequestModel : IRequest<GetAllMoviesResponseModel>
    {
    }
    public class GetAllMoviesResponseModel
    {
        public IEnumerable<Movie> Movies;
    }

    public class GetAllMoviesHandler : IRequestHandler<GetAllMoviesRequestModel, GetAllMoviesResponseModel>
    {
        IMovieDataAccessLayer movieDataAccessLayer;
       
        public GetAllMoviesHandler(IMovieDataAccessLayer _movieDataAccessLayer)
        {
            movieDataAccessLayer = _movieDataAccessLayer;
        }
        public async Task<GetAllMoviesResponseModel> Handle(GetAllMoviesRequestModel request, CancellationToken cancellationToken)
        {
            
            return new GetAllMoviesResponseModel { Movies = movieDataAccessLayer.GetAllMovies() };
        }
    }
}
