using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Text.RegularExpressions;

namespace MovieApp.Models.DataAccessLayers
{
    public class UserDataAccessLayer :IUserDataAccessLayer
    {
        string connectionString = "Server=FSIND-LT-43; Database= MovieProject; Trusted_Connection = True";
        public bool AddUser(User user)
        {
             if (string.IsNullOrEmpty(user.FName))
            {
                throw new ArgumentNullException("First Name Cannot be Null or Empty");
            }
            else if (string.IsNullOrEmpty(user.LName))
            {
                throw new ArgumentNullException("Last Name Cannot be Null or Empty");
            }
            else if (string.IsNullOrEmpty(user.Email) ||
                !Regex.IsMatch(user.Email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase))
            {
                throw new ArgumentNullException("Email Id Cannot be Null or Empty");
            }
            else if (user.Password.Length < 6)
            {
                throw new ArgumentException("Password Length Should be More than 6");
            }
            else if (user.Password != user.ConfirmPassword)
            {
                throw new ArgumentException("Both the Passwords Must be same");
            }
            else if (user.Contact.Length !=10)
            {
                throw new ArgumentException("Contact Number must Have 10 digits");
            }
           


            if (CheckUserDetails(user.Email).Email != user.Email)
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("spAddUser", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    
                    cmd.Parameters.AddWithValue("@FName", user.FName);
                    cmd.Parameters.AddWithValue("@LName", user.LName);
                    cmd.Parameters.AddWithValue("@Email", user.Email);
                    cmd.Parameters.AddWithValue("@Password", user.Password);
                    cmd.Parameters.AddWithValue("@ConfirmPassword", user.ConfirmPassword);
                    cmd.Parameters.AddWithValue("@ContactNumber", user.Contact);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        public bool ChangePassword(String Email, string pass)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spUpdateUser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Email", Email);
                cmd.Parameters.AddWithValue("@Password", pass);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return true;
            }
        }
        public bool CheckLogin(String Email,String Password)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spCheckUser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Email", Email);
                cmd.Parameters.AddWithValue("@Password",Password);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    string email = rdr["Email"].ToString();
                    string pass = rdr["Password"].ToString();
                    if (email == Email && pass == Password)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                return false;
            }

        }
        public User CheckUserDetails(string Email)
        {
            User user = new User();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spCheckUserDetail", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Email", SqlDbType.VarChar, 30).Value = Email;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    user.Email = rdr["Email"].ToString();
                    user.FName = rdr["FName"].ToString();
                    user.LName = rdr["LName"].ToString();
                    user.Password = rdr["Password"].ToString();
                }
            }
            return user;
        }
    }
}
