using AutoMapper;
using MediatR;
using MovieApp.Models.DataAccessLayers;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace MovieApp.Models.Handlers
{

    public class AddMovieRequestModel : IRequest<AddMovieResponseModel>
    {

        [Required(ErrorMessage = "Enter Movie Name")]
        public string MovieName { set; get; }
        [Required(ErrorMessage = "Give A rating To Movie")]
        [Range(0, 10)]
        public int Rating { set; get; }
    }
    public class AddMovieResponseModel
    {
        public bool success { set; get; }
    }
    internal class AddMovieHandler : IRequestHandler<AddMovieRequestModel, AddMovieResponseModel>
    {
        IMovieDataAccessLayer movieDataAccessLayer;
        private IMapper _mapper;
        public AddMovieHandler( IMapper mapper, IMovieDataAccessLayer _movieDataAccessLayer)
        {
            movieDataAccessLayer = _movieDataAccessLayer;
            _mapper = mapper;
        }

      public async Task<AddMovieResponseModel> Handle(AddMovieRequestModel request, CancellationToken cancellationToken)
        {
            movieDataAccessLayer.AddMovie(_mapper.Map<Movie>(request));
            return new AddMovieResponseModel() { success = true};
        }
    }
}
