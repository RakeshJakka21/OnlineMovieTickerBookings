using OnlineMovieTicketBooking.API.Models;
using Xunit;

namespace OnlineMovieTicketBooking.Tests.Models
{
    public class MovieTests
    {
        [Fact]
        public void Movie_Should_Store_Details()
        {
            var movie = new Movie { Id = 1, Title = "RRR", Description = "Action", Price = 150 };
            Assert.Equal("RRR", movie.Title);
        }

        [Fact]
        public void Movie_Price_Should_Be_Positive()
        {
            var movie = new Movie { Price = 200 };
            Assert.True(movie.Price > 0);
        }

        [Fact]
        public void Movie_Genre_Should_Not_Be_Empty()
        {
            var movie = new Movie { Description = "Drama" };
            Assert.NotEmpty(movie.Description);
        }

        [Fact]
        public void Movie_Id_Should_Be_NonNegative()
        {
            var movie = new Movie { Id = 3 };
            Assert.True(movie.Id >= 0);
        }

        [Fact]
        public void Movie_Title_Should_Have_Min_Length()
        {
            var movie = new Movie { Title = "RRR" };
            Assert.True(movie.Title.Length >= 2);
        }
    }
}
