using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApp.Models
{
    public class UserDataAccessLayer
    {
        string connectionString ="Server=FSIND-LT-43; Database= MovieProject; Trusted_Connection = True";
        public void AddUser(User user)
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
        public bool CheckLogin(User user)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
             
                SqlCommand cmd = new SqlCommand("spCheckUser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@Password", user.Password);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    string email = rdr["Email"].ToString();
                    string pass = rdr["Password"].ToString();
                    if (email == user.Email && pass == user.Password)
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

    }
}
