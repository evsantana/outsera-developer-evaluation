namespace OutseraMovies.Application.Interfaces
{
    public interface IBaseService<T> where T : class
    {
        /// <summary>
        /// Creates a new entity in the repository
        /// </summary>
        /// <param name="entity">The entity to create</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The created entity</returns>
        Task<T?> CreateAsync(T entity, CancellationToken cancellationToken);

        /// <summary>
        /// Updates an entity in the repository.
        /// </summary>
        /// <param name="entity">The entity to update</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The updated entity.</returns>
        Task UpdateAsync(T entity, CancellationToken cancellationToken);

        /// <summary>
        /// Deletes an entity from the repository
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The deleted entity</returns>
        Task DeleteAsync(T entity, CancellationToken cancellationToken);

        /// <summary>
        /// Retrieves an entity by their unique identifier
        /// </summary>
        /// <param name="id">The unique identifier of the entity</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The entity if found, null otherwise</returns>
        Task<T> GetByIdAsync(int id, CancellationToken cancellationToken);

        /// <summary>
        /// Retrieves a list with all entities.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>List of entities</returns>
        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken);
    }
}
