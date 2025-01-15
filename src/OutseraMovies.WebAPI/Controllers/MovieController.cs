using Microsoft.AspNetCore.Mvc;
using OutseraMovies.Application.DTOs;
using OutseraMovies.Application.Interfaces;

namespace OutseraMovies.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovieController : Controller
    {
        private readonly IMovieService _movieService;

        /// <summary>
        /// Initializes a new instance of MovieController
        /// </summary>
        /// <param name="movieService">The IMovieService instance</param>
        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        /// <summary>
        /// Retrieves a movie by their ID
        /// </summary>
        /// <param name="id">The unique identifier of the movie</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The movie details if found</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovie([FromRoute] int id, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(id.ToString())) return NotFound();

            MovieDTO movie = await _movieService.GetByIdAsync(id, cancellationToken);

            if (movie is null)
                return NotFound(new
                {
                    Message = "Movie not found"
                });

            return Ok(new{
                Success = true,
                Message = "Movie retrieved successfully",
                Data = movie
            });
        }

        /// <summary>
        /// Retrieves all movies
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>A list of movies</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllMovies(CancellationToken cancellationToken)
        {
            var list = await _movieService.GetAllAsync(cancellationToken);

            if (list is null || list.Count() == 0)
                return NotFound(new
                {
                    Message = "Movies not found"
                });

            return Ok(list);
        }

        /// <summary>
        /// Creates a new movie
        /// </summary>
        /// <param name="movieDto">The movie creation DTO</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The created movie details</returns>
        [HttpPost]
        public async Task<IActionResult> Create(MovieDTO movieDto, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var response = await _movieService.CreateAsync(movieDto, cancellationToken);

            return Created(string.Empty, new
            {
                Success = true,
                Message = "Movie create successfully",
                Data = response
            });
        }

        /// <summary>
        /// Updates a movie
        /// </summary>
        /// <param name="id">The unique identifier of the movie</param>
        /// <param name="request">Movie DTO</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Success response if the movie was updated</returns>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] MovieDTO request, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (string.IsNullOrEmpty(id.ToString()))
                return NotFound(new { Message = "ID invalid" });

            if (request is null)
                return NotFound(new { Message = "Invalid data" });

            await _movieService.UpdateAsync(request, cancellationToken);

            return Ok(new
            {
                Success = true,
                Message = "Movie updated successfully",
                Data = request
            });
        }

        /// <summary>
        /// Deletes a movie by their ID
        /// </summary>
        /// <param name="id">The unique identifier of the movie to delete</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Success response if the movie was deleted</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(id.ToString()))
                return NotFound(new { Message = "ID invalid" });

            var movie = await _movieService.GetByIdAsync(id, cancellationToken);

            if (movie is null)
                return NotFound(new
                {
                    Message = "Movie not found"
                });

            await _movieService.DeleteAsync(movie, cancellationToken);

            return Ok(new{
                Success = true,
                Message = "Movie deleted successfully"
            });
        }
    }
}
