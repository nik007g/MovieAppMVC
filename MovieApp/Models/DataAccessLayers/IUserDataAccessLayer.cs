using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApp.Models.DataAccessLayers
{
   public interface IUserDataAccessLayer
    {
        public bool AddUser(User user);
        public bool ChangePassword(String Email, string pass);
        public bool CheckLogin(String Email, String Password);
        public User CheckUserDetails(string Email);

    }
}
