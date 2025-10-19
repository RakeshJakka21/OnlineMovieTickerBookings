using OnlineMovieTicketBooking.API.Models;
using Xunit;

namespace MovieTicketApp.Tests.Models
{
    public class BookingTests
    {
        [Fact]
        public void Booking_Should_Link_Movie_And_User()
        {
            var booking = new Booking { MovieId = 1, UserId = 2 };
            Assert.Equal(1, booking.MovieId);
            Assert.Equal(2, booking.UserId);
        }

        [Fact]
        public void Booking_Quantity_Should_Be_AtLeast_One()
        {
            var booking = new Booking { Quantity = 2 };
            Assert.True(booking.Quantity >= 1);
        }

        [Fact]
        public void Booking_Should_Have_Date()
        {
            var booking = new Booking { BookingDate = DateTime.Now };
            Assert.NotEqual(default, booking.BookingDate);
        }

        [Fact]
        public void Booking_Should_Store_Valid_Ids()
        {
            var booking = new Booking { UserId = 5, MovieId = 3 };
            Assert.True(booking.UserId > 0 && booking.MovieId > 0);
        }

        [Fact]
        public void Booking_Should_Create_Object()
        {
            var booking = new Booking { Id = 1 };
            Assert.Equal(1, booking.Id);
        }
    }
}
