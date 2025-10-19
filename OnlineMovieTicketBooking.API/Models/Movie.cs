using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineMovieTicketBooking.API.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Title { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        public int AvailableSeats { get; set; }
    }
}
