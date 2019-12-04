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
    public class UpdateMovieRequestModel : IRequest<UpdateMovieResponseModel>
    {
        public int MovieId { set; get; }
        [Required(ErrorMessage = "Enter Movie Name")]
        public string MovieName { set; get; }
        [Required(ErrorMessage = "Give A rating To Movie")]
        [Range(0, 10)]
        public int Rating { set; get; }

    }
    public class UpdateMovieResponseModel
    {
        public bool success { set; get; }
    }

    public class UpdateMovieHandler : IRequestHandler<UpdateMovieRequestModel, UpdateMovieResponseModel>
    {
        IMovieDataAccessLayer movieDataAccessLayer;
        IMapper _mapper;

        public UpdateMovieHandler(IMovieDataAccessLayer _movieDataAccessLayer, IMapper mapper)
        {
            _mapper = mapper;
            movieDataAccessLayer = _movieDataAccessLayer;
        }


        public async Task<UpdateMovieResponseModel> Handle(UpdateMovieRequestModel request, CancellationToken cancellationToken)
        { 
            movieDataAccessLayer.Update(_mapper.Map<Movie>(request));
            return new UpdateMovieResponseModel() { success = true };
        }
    }
}
