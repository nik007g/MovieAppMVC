using AutoMapper;
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
    public class GetMoviesRequestModel : IRequest<GetMoviesResponseModel>
    {
        public int? MovieId { set; get; }

    }
    public class GetMoviesResponseModel
    {
        public int MovieId { set; get; }
        [Required(ErrorMessage = "Enter Movie Name")]
        public string MovieName { set; get; }
        [Required(ErrorMessage = "Give A rating To Movie")]
        [Range(0, 10)]
        public int Rating { set; get; }
    }

    public class GetMoviesHandler : IRequestHandler<GetMoviesRequestModel, GetMoviesResponseModel>
    {
        IMovieDataAccessLayer movieDataAccessLayer;
        IMapper _mapper;

        public GetMoviesHandler(IMovieDataAccessLayer _movieDataAccessLayer,IMapper mapper)
        {
            _mapper = mapper;
            movieDataAccessLayer = _movieDataAccessLayer;
        }
     

        public async Task<GetMoviesResponseModel> Handle(GetMoviesRequestModel request, CancellationToken cancellationToken)
        {
            return _mapper.Map<GetMoviesResponseModel>(movieDataAccessLayer.GetMovies(request.MovieId));
            
        }
    }
}

