using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApp.Models
{
    public class User
    {

        [Required(ErrorMessage = "Enter Valid First Name")]
        public string FName { set; get; }
        [Required(ErrorMessage = "Enter Valid Last Name")]

        public string LName { set; get; }


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
        [StringLength(10, ErrorMessage = "Password Should Contain More than 6 Characters",MinimumLength = 6)]
        [Compare("Password", ErrorMessage = "Password Does Not Match")]
        public string ConfirmPassword { set; get; }


        [Required(ErrorMessage = "Enter Valid Contact number")]
        [StringLength(10, ErrorMessage = "Mobile Number Must Contain 10 Digits", MinimumLength = 10)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",
                   ErrorMessage = "Entered phone format is not valid.")]
        [DataType(DataType.PhoneNumber)]

        public string Contact { set; get; }


    }
}
