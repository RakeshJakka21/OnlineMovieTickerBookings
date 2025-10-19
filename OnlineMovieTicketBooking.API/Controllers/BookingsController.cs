using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineMovieTicketBooking.API.Data;
using OnlineMovieTicketBooking.API.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace OnlineMovieTicketBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BookingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // API methods go here
        // GET: api/Bookings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Booking>>> GetBookings()
        {
            // Include User and Movie details
            return await _context.Bookings
                .Include(b => b.User)
                .Include(b => b.Movie)
                .ToListAsync();
        }

        // GET: api/Bookings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Booking>> GetBooking(int id)
        {
            var booking = await _context.Bookings
                .Include(b => b.User)
                .Include(b => b.Movie)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (booking == null)
                return NotFound();

            return booking;
        }

        // POST: api/Bookings
        [HttpPost]
        public async Task<ActionResult<Booking>> PostBooking(Booking booking)
        {
            // Check if Movie exists
            var movie = await _context.Movies.FindAsync(booking.MovieId);
            if (movie == null)
                return BadRequest("Movie not found.");

            // Check seat availability
            if (booking.Quantity > movie.AvailableSeats)
                return BadRequest($"Only {movie.AvailableSeats} seats available.");

            // Deduct seats
            movie.AvailableSeats -= booking.Quantity;

            // Set booking date
            booking.BookingDate = DateTime.Now;

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBooking), new { id = booking.Id }, booking);
        }

        // PUT: api/Bookings/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBooking(int id, Booking booking)
        {
            if (id != booking.Id) return BadRequest();

            _context.Entry(booking).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Bookings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null) return NotFound();

            // Restore seats when booking is deleted
            var movie = await _context.Movies.FindAsync(booking.MovieId);
            if (movie != null)
                movie.AvailableSeats += booking.Quantity;

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // GET: api/Bookings/User/1  => Booking history for user
        [HttpGet("User/{userId}")]
        public async Task<ActionResult<IEnumerable<Booking>>> GetBookingHistory(int userId)
        {
            var bookings = await _context.Bookings
                .Include(b => b.Movie)
                .Include(b => b.User)
                .Where(b => b.UserId == userId)
                .ToListAsync();

            if (bookings.Count == 0)
                return NotFound("No bookings found for this user.");

            return bookings;
        }

    }
}
