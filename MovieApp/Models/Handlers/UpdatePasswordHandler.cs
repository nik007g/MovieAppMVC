using MediatR;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MovieApp.Models.DataAccessLayers;

namespace MovieApp.Models.Handlers
{
        public class UpdatePasswordRequestModel :IRequest<UpdatePasswordResponseModel>
        {
            [Required(ErrorMessage = "Enter Valid Email")]
            [DataType(DataType.EmailAddress)]
            public string Email { set; get; }


            [Required(ErrorMessage = "Enter Valid Password")]
            [DataType(DataType.Password)]
            [StringLength(10, ErrorMessage = "Password Should Contain More than 6 Characters", MinimumLength = 6)]
            public string Password { set; get; }


            [Required(ErrorMessage = "Confirm Password Require")]
            [Display(Name = "Confirm Password")]
            [DataType(DataType.Password)]
            [StringLength(10, ErrorMessage = "Password Should Contain More than 6 Characters", MinimumLength = 6)]
            [Compare("Password", ErrorMessage = "Password Does Not Match")]
            public string ConfirmPassword { set; get; }
        }
        public class UpdatePasswordResponseModel
        {
            public bool success { get; set;}
        }
    internal class UpdatePasswordHandler : IRequestHandler<UpdatePasswordRequestModel, UpdatePasswordResponseModel>
    {
        IUserDataAccessLayer userDataAccessLayer;
        private IMapper _mapper;
        public UpdatePasswordHandler(IMapper mapper, IUserDataAccessLayer _userDataAccessLayer)
        {
            userDataAccessLayer = _userDataAccessLayer;
            _mapper = mapper;
        }
        public async Task<UpdatePasswordResponseModel> Handle(UpdatePasswordRequestModel request, CancellationToken cancellationToken)
        {
            return new UpdatePasswordResponseModel
            {
               success= userDataAccessLayer.ChangePassword(request.Email,request.Password)
            };
    }
    }
}
