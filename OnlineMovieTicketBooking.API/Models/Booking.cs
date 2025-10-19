using OnlineMovieTicketBooking.API.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineMovieTicketBooking.API.Models
{
    public class Booking
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int MovieId { get; set; }

        [Required, Range(1, 10)]
        public int Quantity { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime BookingDate { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        [ForeignKey("MovieId")]
        public Movie Movie { get; set; }
    }
}
