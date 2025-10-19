using OnlineMovieTicketBooking.MVC.Models;
using Xunit;

namespace MovieTicketApp.Tests.Models
{
    public class UserTests
    {
        [Fact]
        public void User_Should_Create_Object()
        {
            var user = new User { Id = 1, Name = "Rakesh", Email = "test@mail.com", Password = "123" };
            Assert.Equal(1, user.Id);
        }

        [Fact]
        public void User_Should_Contain_Required_Fields()
        {
            var user = new User { Name = "Rakesh", Email = "mail@test.com", Password = "123" };
            Assert.NotNull(user.Email);
            Assert.NotNull(user.Password);
        }

        [Fact]
        public void User_Email_Should_Be_Valid_Format()
        {
            var user = new User { Email = "test@mail.com" };
            Assert.Contains("@", user.Email);
        }

        [Fact]
        public void User_Name_Should_Not_Be_Empty()
        {
            var user = new User { Name = "Rakesh" };
            Assert.False(string.IsNullOrEmpty(user.Name));
        }

        [Fact]
        public void User_Password_Should_Not_Be_Empty()
        {
            var user = new User { Password = "123" };
            Assert.False(string.IsNullOrEmpty(user.Password));
        }
    }
}
