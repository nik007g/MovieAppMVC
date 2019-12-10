using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
namespace MovieApp.Models.DataAccessLayers
{
    public class MovieDataAccesLayerADONET : IMovieDataAccessLayer
    {
        string connectionString = "Server=FSIND-LT-43; Database= MovieProject; Trusted_Connection = True";
        public bool AddMovie(Movie movie)
        {
            if (movie.Rating > 10 || movie.Rating < 1)
            {
                throw new ArgumentOutOfRangeException("Rating Should be in between 0-10");
            }
            else if (String.IsNullOrEmpty(movie.MovieName))
            {
                throw new ArgumentOutOfRangeException("Movie Name Can not be null");
            }
            if (CheckMovieDetails(movie.MovieName).MovieName != movie.MovieName)
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand("spAddMovie", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MovieName", movie.MovieName);
                    cmd.Parameters.AddWithValue("@rating", movie.Rating);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        public IEnumerable<Movie> GetAllMovies()
        {
            List<Movie> lastmovie = new List<Movie>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetAllMovies", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Movie movie = new Movie();
                    movie.MovieId = Convert.ToInt32(rdr["MovieID"]);
                    movie.MovieName = rdr["MovieName"].ToString();
                    movie.Rating = Convert.ToInt32(rdr["Rating"]);
                    lastmovie.Add(movie);
                }
                con.Close();
            }
            return lastmovie;
        }
        public void Delete(int? id)
        {

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spDeleteMovie", con);
                cmd.CommandType = CommandType.StoredProcedure;
                
                cmd.Parameters.AddWithValue("@MovieID", id);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        public bool Update(Movie movie)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spUpdateMovie", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@MovieID", movie.MovieId);
                cmd.Parameters.AddWithValue("@MovieName", movie.MovieName);
                cmd.Parameters.AddWithValue("@Rating", movie.Rating);
        
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            return true;
        }
        public Movie GetMovies(int? id)
        {
            Movie movie = new Movie();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM Movie WHERE MovieID= "+ id;
                SqlCommand cmd = new SqlCommand(sqlQuery, con);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    movie.MovieId = Convert.ToInt32(rdr["MovieID"]);
                    movie.MovieName = rdr["MovieName"].ToString();
                    movie.Rating = Convert.ToInt32(rdr["Rating"]);

                }
            }
            return movie;
        }
        public Movie CheckMovieDetails(string MovieName)
        {
            Movie movie = new Movie();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spCheckMovieDetail", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@MovieName", SqlDbType.VarChar, 30).Value = MovieName;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    movie.MovieName = rdr["MovieName"].ToString();  
                }
            }
            return movie;
        }

    }
}
