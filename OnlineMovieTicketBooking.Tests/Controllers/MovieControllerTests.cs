using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using OnlineMovieTicketBooking.API.Controllers;
using OnlineMovieTicketBooking.API.Models;
using OnlineMovieTicketBooking.API.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace OnlineMovieTicketBooking.Tests.Controllers
{ 

    public class MovieControllerTests
    {
        private readonly ApplicationDbContext _context;

        public MovieControllerTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "MovieTestDB")
                .Options;
            _context = new ApplicationDbContext(options);
        }

        [Fact]
        public async Task GetMovies_ReturnsList()
        {
            _context.Movies.Add(new Movie { Title = "Test", Description = "Drama", Price = 100 });
            await _context.SaveChangesAsync();

            var controller = new MoviesController(_context);
            var result = await controller.GetMovies();

            Assert.NotNull(result.Value);
        }

        [Fact]
        public async Task AddMovie_Works()
        {
            var controller = new MoviesController(_context);
            var movie = new Movie { Title = "NewMovie", Description = "Comedy", Price = 200 };

            var result = await controller.PostMovie(movie);
            Assert.IsType<ActionResult<Movie>>(result);
        }

        [Fact]
        public async Task GetMovieById_Works()
        {
            var movie = new Movie { Title = "RRR", Description = "Action", Price = 150 };
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            var controller = new MoviesController(_context);
            var result = await controller.GetMovie(movie.Id);

            Assert.NotNull(result.Value);
        }

        [Fact]
        public async Task UpdateMovie_Works()
        {
            var movie = new Movie { Title = "Pushpa", Description = "Drama", Price = 180 };
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            var controller = new MoviesController(_context);
            movie.Price = 200;

            var result = await controller.PutMovie(movie.Id, movie);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteMovie_Works()
        {
            var movie = new Movie { Title = "DeleteMe", Description = "Thriller", Price = 120 };
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            var controller = new MoviesController(_context);
            var result = await controller.DeleteMovie(movie.Id);

            Assert.NotNull(result);
        }
    }
}
