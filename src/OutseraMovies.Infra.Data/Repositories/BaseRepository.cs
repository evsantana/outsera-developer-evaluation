using Microsoft.EntityFrameworkCore;
using OutseraMovies.Domain.Entities;
using OutseraMovies.Domain.Interfaces;
using OutseraMovies.Infra.Data.Context;

namespace OutseraMovies.Infra.Data.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        #region Properties and Constructors
        protected readonly ApplicationContext _context;
        public BaseRepository(ApplicationContext context)
        {
            _context = context;
        }
        #endregion

        /// <summary>
        /// Creates a new entity in the repository
        /// </summary>
        /// <param name="entity">The entity to create</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The created entity</returns>
        public async Task<T> CreateAsync(T entity, CancellationToken cancellationToken)
        {
            _context.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity;
        }

        /// <summary>
        /// Deletes an entity from the repository
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The deleted entity</returns>
        public async Task<T> DeleteAsync(T entity, CancellationToken cancellationToken)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity;
        }


        /// <summary>
        /// Retrieves a list with all entities.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>List of entities</returns>
        public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Set<T>().ToListAsync(cancellationToken);
        }

        /// <summary>
        /// Retrieves an entity by their unique identifier
        /// </summary>
        /// <param name="Id">The unique identifier of the entity</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The entity if found, null otherwise</returns>
        public async Task<T> GetByIdAsync(int Id, CancellationToken cancellationToken)
        {
            return await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == Id, cancellationToken);
        }

        /// <summary>
        /// Updates an entity in the repository
        /// </summary>
        /// <param name="entity">The entity to update</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The updated entity.</returns>
        public async Task<T> UpdateAsync(T entity, CancellationToken cancellationToken)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity;
        }
    }
}
