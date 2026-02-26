using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public interface IGenericRepository<T>
    {
        /// <summary>
        /// Asynchronously retrieves all entities of type T from the data source.
        /// </summary>
        /// <returns>Returns asynchronously a read-only list of entities of
        /// type T. The list will be empty if no entities are found.</returns>
        Task<IReadOnlyList<T>> GetAllAsync();
        /// <summary>
        /// Asynchronously retrieves an entity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the entity to retrieve. Must be a positive integer.</param>
        /// <returns>Returns asynchronously the entity of type T if found;
        /// otherwise, null.</returns>
        Task<T> GetByIDAsync(int id);
        /// <summary>
        /// Asynchronously adds the specified entity to the data store.
        /// </summary>
        /// <param name="entity">The entity to add. Cannot be null.</param>
        /// <returns>A task that represents the asynchronous add operation.</returns>
        Task AddAsync(T entity);
        /// <summary>
        /// Asynchronously updates the specified entity in the data store.
        /// </summary>
        /// <param name="entity">The entity to update. Cannot be null. The entity must already exist in the data store.</param>
        /// <returns>Nothin simply updates the state asynchronously.</returns>
        Task UpdateAsync(T entity);
        /// <summary>
        /// Asynchronously delets the specified entity data.
        /// </summary>
        /// <param name="id">The id of the entity to delete Cannot be null. The entity must already exist in the data store.</param>
        /// <returns>Nothin simply updates the state asynchronously.</returns>
        Task DeleteAsync(int id);
        Task SaveAsync();

        // COPY PASTE, MY SAVIOUR
    }
}
