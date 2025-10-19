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
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public UsersController(ApplicationDbContext context) => _context = context;

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.AsNoTracking().ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
            if (user == null) return NotFound();
            return user;
        }

        // POST: api/Users  (Register)
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            // Basic duplicate email check
            if (await _context.Users.AnyAsync(u => u.Email == user.Email))
                return BadRequest(new { message = "Email already registered." });

            // NOTE: For capstone demo we store plaintext. In production, hash passwords.
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        // PUT: api/Users/5  (Update user)
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id) return BadRequest();

            // Prevent email duplication with other users
            if (await _context.Users.AnyAsync(u => u.Email == user.Email && u.Id != id))
                return BadRequest(new { message = "Email already used by another account." });

            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();

            // Optional: handle cascade or existing bookings — here we allow delete only if no bookings
            var hasBookings = await _context.Bookings.AnyAsync(b => b.UserId == id);
            if (hasBookings)
                return BadRequest(new { message = "Cannot delete user with existing bookings." });

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // POST: api/Users/Login
        // Body: { "email": "x", "password": "y" }
        [HttpPost("Login")]
        public async Task<ActionResult<User>> Login([FromBody] LoginDto dto)
        {
            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == dto.Email && u.Password == dto.Password);

            if (user == null)
                return Unauthorized(new { message = "Invalid email or password." });

            // For API, you might return a token here; for demo return user basic info
            return Ok(new { user.Id, user.Name, user.Email });
        }

        // DTO for login
        public class LoginDto
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }
    }
}
