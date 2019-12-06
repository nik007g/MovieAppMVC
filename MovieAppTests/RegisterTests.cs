using System;
using Xunit;
using MovieApp;
using MovieApp.Models.DataAccessLayers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MovieApp.Models;

namespace MovieAppTests
{
    public class RegisterTests
    {
        [Fact]
        public void Test_Invalid_Email()
        {
            var userDataAccessLayer = new UserDataAccessLayer();
            User user = new User {FName="Omkar",LName="Shinde",Password="Omkar123",Email="omkar1@Gmail.com"};
            Assert.Throws<ArgumentNullException>(() => userDataAccessLayer.AddUser(user));
        }
        [Fact]
        public void Test_Invalid_FName()
        {
            var userDataAccessLayer = new UserDataAccessLayer();
            User user = new User {  LName = "Shinde", Password = "Omkar123" };
            Assert.Throws<ArgumentNullException>(() => userDataAccessLayer.AddUser(user));
        }
        [Fact]
        public void Test_Invalid_contact()
        {
            var userDataAccessLayer = new UserDataAccessLayer();
            User user = new User {Email="Om@kar.com", FName="Omkar",LName = "Shinde", Password = "Omkar123", Contact="77894745"};
            Assert.Throws<ArgumentException>(() => userDataAccessLayer.AddUser(user));
        }
        [Fact]
        public void Test_Invalid_LName()
        {
            var userDataAccessLayer = new UserDataAccessLayer();
            User user = new User { FName = "Omkar", Password = "Omkar123" };
            Assert.Throws<ArgumentNullException>(() => userDataAccessLayer.AddUser(user));
        }
        [Fact]
        public void Test_Invalid_Password()
        {
            var userDataAccessLayer = new UserDataAccessLayer();
            User user = new User { Email="Omkar@gmail.com",FName = "Omkar", LName = "Shinde", Password = "Omk" };
            Assert.Throws<ArgumentException>(() => userDataAccessLayer.AddUser(user));
        }
        [Fact]
        public void Test_PasswordsDoesNotMatch()
        {
            var userDataAccessLayer = new UserDataAccessLayer();
            User user = new User { Email = "Omkar@gmail.com", FName = "Omkar", LName = "Shinde", Password = "Omkar12",Contact="7458963210",ConfirmPassword="Omkar12" };
            Assert.Throws<ArgumentException>(() => userDataAccessLayer.AddUser(user));
        }
        [Fact]
        public void Test_Valid_User()
        {
            var userDataAccessLayer = new UserDataAccessLayer();
            User user = new User { Email = "Omkar@Omkar.com", FName = "Omkar", LName = "Shinde", Password = "Omkar1",ConfirmPassword="Omkar1",Contact="9876543210" };
            Assert.True(userDataAccessLayer.AddUser(user));
        }

    }
}
