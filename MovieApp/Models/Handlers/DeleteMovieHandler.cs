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
    public class DeleteMovieRequestModel : IRequest<DeleteMovieResponseModel>
    {
        public int? MovieId { set; get; }
        

    }
    public class DeleteMovieResponseModel
    {
        
    }

    public class DeleteMovieHandler : IRequestHandler<DeleteMovieRequestModel, DeleteMovieResponseModel>
    {
        IMovieDataAccessLayer movieDataAccessLayer;
        IMapper _mapper;

        public DeleteMovieHandler(IMovieDataAccessLayer _movieDataAccessLayer, IMapper mapper)
        {
            _mapper = mapper;
            movieDataAccessLayer = _movieDataAccessLayer;
        }


        public async Task<DeleteMovieResponseModel> Handle(DeleteMovieRequestModel request, CancellationToken cancellationToken)
        {
            movieDataAccessLayer.Delete(request.MovieId);
            return new DeleteMovieResponseModel();
        }
    }
}
