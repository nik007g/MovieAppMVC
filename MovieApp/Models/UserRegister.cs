using System;
using System.Collections.Generic;

namespace MovieApp.Models
{
    public partial class UserRegister
    {
        public int UserId { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string ContactNumber { get; set; }
    }
}
