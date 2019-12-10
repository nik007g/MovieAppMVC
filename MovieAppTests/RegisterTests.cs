using System;
using Xunit;
using MovieApp.Models.DataAccessLayers;
using MovieApp.Models;
using Moq;

namespace MovieAppTests
{
    public class RegisterTests
    {
        [Fact]
        public void Test_Invalid_FName()
        {
            var userDataAccessLayer = new UserDataAccessLayer();
            User user = new User {FName="",LName = "Shinde", Email = "Omkar@Omkar.com", Password = "Omkar1", ConfirmPassword = "Omkar1", Contact = "9876543210" };
            Assert.Throws<ArgumentNullException>(() => userDataAccessLayer.AddUser(user));
        }
      
        [Fact]
        public void Test_Invalid_LName()
        {
            var userDataAccessLayer = new UserDataAccessLayer();
            User user = new User { FName = "Omkar", Email = "Omkar@Omkar.com", Password = "Omkar1", ConfirmPassword = "Omkar1", Contact = "9876543210" };
            Assert.Throws<ArgumentNullException>(() => userDataAccessLayer.AddUser(user));
        }
        [Fact]
        public void Test_Invalid_Email()
        {
            var userDataAccessLayer = new UserDataAccessLayer();
            User user = new User { FName = "Omkar", LName = "Shinde", Password = "Omkar1", ConfirmPassword = "Omkar1", Contact = "9876543210" };
            Assert.Throws<ArgumentNullException>(() => userDataAccessLayer.AddUser(user));
        }
        [Fact]
        public void Test_Invalid_Password()
        {
            var userDataAccessLayer = new UserDataAccessLayer();
            User user = new User { FName = "Omkar", LName = "Shinde", Email = "Omkar@Omkar.com", Password = "Omka", ConfirmPassword = "Omkar1", Contact = "9876543210" };
            Assert.Throws<ArgumentException>(() => userDataAccessLayer.AddUser(user));
        }
        [Fact]
        public void Test_PasswordsDoesNotMatch()
        {
            var userDataAccessLayer = new UserDataAccessLayer();
            User user = new User { FName = "Omkar", LName = "Shinde", Email = "Omkar@Omkar.com", Password = "Omkar1", ConfirmPassword = "Omka", Contact = "9876543210" };
            Assert.Throws<ArgumentException>(() => userDataAccessLayer.AddUser(user));
        }
        [Fact]
        public void Test_Invalid_contact()
        {
            var userDataAccessLayer = new UserDataAccessLayer();
            User user = new User { FName = "Omkar", LName = "Shinde", Email = "Omkar@Omkar.com", Password = "Omkar1", ConfirmPassword = "Omkar1", Contact = "976362019" };
            Assert.Throws<ArgumentException>(() => userDataAccessLayer.AddUser(user));
        }
        [Fact]
        public void Test_Valid_User()
        {
            var userDataAccessLayer = new UserDataAccessLayer();
            User user = new User { FName = "Omkar", LName = "Shinde", Email = "Omkar@omka.com", Password = "Omkar1", ConfirmPassword = "Omkar1", Contact = "9876543210" };
            Assert.True(userDataAccessLayer.AddUser(user));

         /*   var userDataAccessLayer = new Mock<IUserDataAccessLayer>();
            userDataAccessLayer.Setup(dal => dal.AddUser(It.Is<User>(usr => !string.IsNullOrEmpty(usr.FName)
            == !string.IsNullOrEmpty(usr.LName)
            ==  !string.IsNullOrEmpty(usr.Email)
            ==  !string.IsNullOrEmpty(usr.Password)
            ==   !string.IsNullOrEmpty(usr.ConfirmPassword)
            == !string.IsNullOrEmpty(usr.Contact)
                ))).Returns(true);

            User user = new User { FName = "Omkar", LName = "Shinde", Email = "Omkar@omka.com", Password = "Omkar1", ConfirmPassword = "Omkar1", Contact = "9876543210"};
            
            Assert.True(userDataAccessLayer.Object.AddUser(user)); */
        }
    }
}
