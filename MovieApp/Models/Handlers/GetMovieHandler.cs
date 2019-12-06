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
    public class GetMovieRequestModel : IRequest<GetMovieResponseModel>
    {
        public int? MovieId { set; get; }

    }
    public class GetMovieResponseModel
    {
        public int MovieId { set; get; }
        [Required(ErrorMessage = "Enter Movie Name")]
        public string MovieName { set; get; }
        [Required(ErrorMessage = "Give A rating To Movie")]
        [Range(0, 10)]
        public int Rating { set; get; }
    }

    public class GetMovieHandler : IRequestHandler<GetMovieRequestModel, GetMovieResponseModel>
    {
        IMovieDataAccessLayer movieDataAccessLayer;
        IMapper _mapper;

        public GetMovieHandler(IMovieDataAccessLayer _movieDataAccessLayer,IMapper mapper)
        {
            _mapper = mapper;
            movieDataAccessLayer = _movieDataAccessLayer;
        }
     

        public async Task<GetMovieResponseModel> Handle(GetMovieRequestModel request, CancellationToken cancellationToken)
        {
            return _mapper.Map<GetMovieResponseModel>(movieDataAccessLayer.GetMovies(request.MovieId));
            
        }
    }
}

