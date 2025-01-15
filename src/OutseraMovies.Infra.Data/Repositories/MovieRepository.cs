using Microsoft.EntityFrameworkCore;
using OutseraMovies.Domain.Entities;
using OutseraMovies.Domain.Interfaces;
using OutseraMovies.Infra.Data.Context;

namespace OutseraMovies.Infra.Data.Repositories
{
    public class MovieRepository : BaseRepository<Movie>, IMovieRepository
    {

        #region Properties and Constructors
        private readonly ApplicationContext _context;
        public MovieRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }
        #endregion

        /// <summary>
        /// Deletes all entities from the repository
        /// </summary>
        /// <returns>The deleted entity</returns>
        public async Task DeleteAllAsync()
        {
            var records = await _context.Movies.ToListAsync();
            _context.Movies.RemoveRange(records);
            await _context.SaveChangesAsync();

            await _context.Database.ExecuteSqlRawAsync("DELETE FROM sqlite_sequence WHERE name = 'Movies';");

        }

        /// <summary>
        /// Retrieves a list of all winning movies.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>List of entities</returns>
        public async Task<List<Movie>> GetWinnersAsync(CancellationToken cancellationToken)
        {
            var records = await _context.Movies.Where(p => p.Winner == true).ToListAsync();

            return records;
        }
    }
}
