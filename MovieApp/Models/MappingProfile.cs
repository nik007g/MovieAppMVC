using AutoMapper;
using MovieApp.Models.Handlers;


namespace MovieApp.Models
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            CreateMap<User, RequestModel>();
            CreateMap<RequestModel, User>();

            CreateMap<User, RegisterRequestModel>();
            CreateMap<RegisterRequestModel, User>();

            CreateMap<User, UpdatePasswordRequestModel>();
            CreateMap<UpdatePasswordRequestModel, User>();
            CreateMap<Movie, AddMovieRequestModel>().ReverseMap();

            CreateMap<Movie, GetMovieRequestModel>().ReverseMap();
            

            CreateMap<Movie, GetMovieResponseModel>().ReverseMap();
            CreateMap<Movie, UpdateMovieRequestModel>().ReverseMap();

            CreateMap<GetMovieResponseModel,UpdateMovieRequestModel>().ReverseMap();
        }
    }
}
