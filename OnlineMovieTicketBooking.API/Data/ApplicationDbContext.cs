using Microsoft.EntityFrameworkCore;
using OnlineMovieTicketBooking.API.Models;

namespace OnlineMovieTicketBooking.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Booking> Bookings { get; set; }
    }
}
