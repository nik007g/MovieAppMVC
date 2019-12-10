using System;
using Xunit;
using MovieApp.Models.DataAccessLayers;
using MovieApp.Models;
using Moq;
namespace MovieAppTests
{
    public class AddMovieTest
    {
        [Fact]
        public void Test_Invalid_Rating()
        {
            //Arrange
            var movieDataAccessLayer = new MovieDataAcessLayerEF(new MovieProjectContext());
            //Act
            Movie movie = new Movie { MovieName="Sholey",Rating=100};
            //Assert
            Assert.Throws<ArgumentOutOfRangeException>(() =>movieDataAccessLayer.AddMovie(movie));
        }

        [Fact]
        public void Test_Invalid_MovieName()
        {
            var movieDataAccessLayer = new MovieDataAcessLayerEF(new MovieProjectContext());
            Movie movie = new Movie {Rating=9, MovieName = "" };
            Assert.Throws<ArgumentOutOfRangeException>(() => movieDataAccessLayer.AddMovie(movie));
        }
        [Fact]
        public void Test_valid_Movie()
        {
            /* var movieDataAccessLayer = new MovieDataAcessLayerEF(new MovieProjectContext());
              Movie movie = new Movie {  MovieName = "Raaz" ,Rating=10};
              Assert.True(movieDataAccessLayer.AddMovie(movie));*/
              //MOQ 
            var movieDataAcess = new Mock<IMovieDataAccessLayer>();
            movieDataAcess.Setup(dal => dal.AddMovie(It.Is<Movie>(mv=> !string.IsNullOrEmpty(mv.MovieName)
            ==(mv.Rating <0)
            ==(mv.Rating >10)
            )))
                .Returns(true);
            Movie movie = new Movie { MovieName = "Aparichit",Rating = 10 };
            Assert.True(movieDataAcess.Object.AddMovie(movie));
        }
    }
}
