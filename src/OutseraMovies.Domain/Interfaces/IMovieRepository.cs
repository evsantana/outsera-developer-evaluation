using OutseraMovies.Domain.Entities;

namespace OutseraMovies.Domain.Interfaces
{
    public interface IMovieRepository : IBaseRepository<Movie>
    {
        /// <summary>
        /// Deletes all entities from the repository
        /// </summary>
        /// <returns>The deleted entity</returns>
        Task DeleteAllAsync();

        /// <summary>
        /// Retrieves a list of all winning movies.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>List of entities</returns>
        Task<List<Movie>> GetWinnersAsync(CancellationToken cancellationToken);
    }
}
