using Microsoft.AspNetCore.Mvc;
using OutseraMovies.Application.Interfaces;

namespace OutseraMovies.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProducersWinnersController : Controller
    {
        private readonly IMovieService _movieService;

        /// <summary>
        /// Initializes a new instance of ProducersWinnersController
        /// </summary>
        /// <param name="movieService">The IMovieService instance</param>
        public ProducersWinnersController(IMovieService movieService)
        {
            _movieService = movieService;
        }


        [HttpGet]
        public async Task<IActionResult> GetWinningsMovies(CancellationToken cancellationToken)
        {
            var list = await _movieService.GetProducerIntervalMinMax(cancellationToken);

            if (list is null)
                return NotFound("Movies not found");

            return Ok(list);
        }
    }
}
