using System.ComponentModel.DataAnnotations;

namespace OnlineMovieTicketBooking.API.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
