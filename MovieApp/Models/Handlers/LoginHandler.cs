using AutoMapper;
using MediatR;
using MovieApp.Models.DataAccessLayers;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MovieApp.Models.Handlers
{
    public class RequestModel : IRequest<ResponseModel>
    {
        public String Email { get; set; }
        public string Password { get; set; }
        public string FName { get; set; }

    }
    public class ResponseModel
    {
        public bool Success { get; set; }
   
    }
    internal class LoginHandler : IRequestHandler<RequestModel, ResponseModel>
    {
        IUserDataAccessLayer userDataAccessLayer;
        private IMapper _mapper;

        public LoginHandler(IMapper mapper, IUserDataAccessLayer _userDataAccessLayer)
        {
            userDataAccessLayer = _userDataAccessLayer;
            _mapper = mapper;
        }
        public async Task<ResponseModel> Handle(RequestModel request, CancellationToken cancellationToken)
        {
            
            //   bool success = userDataAccessLayer.CheckLogin(request.Email, request.Password);
            return new ResponseModel
            {
                Success = userDataAccessLayer.CheckLogin(request.Email,request.Password)
        };
        }
    }
}

