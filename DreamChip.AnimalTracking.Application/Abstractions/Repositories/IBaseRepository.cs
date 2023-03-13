using DreamChip.AnimalTracking.Domain;

namespace DreamChip.AnimalTracking.Application.Abstractions.Repositories;

/// <summary>
/// Base repository interface.
/// </summary>
/// <typeparam name="TEntity">The entity with which a repository will work.</typeparam>
/// <typeparam name="TKey">The type of entity identifier.</typeparam>
public interface IBaseRepository<TEntity, TKey>
{
    /// <summary>
    /// Gets entity by identifier.
    /// </summary>
    /// <param name="id">Identifier.</param>
    /// <returns>Found entity or default value.</returns>
    Task<TEntity?> GetByIdAsync(TKey id);
    
    /// <summary>
    /// Creates a new entity.
    /// </summary>
    /// <param name="entity">Entity for creation.</param>
    /// <returns>Identifier or created entity.</returns>
    Task<TKey> CreateAsync(TEntity entity);

    /// <summary>
    /// Updates entity.
    /// </summary>
    /// <param name="entity">Entity with new data.</param>
    /// <returns></returns>
    Task UpdateAsync(TEntity entity);

    /// <summary>
    /// Deletes entity by identifier.
    /// </summary>
    /// <param name="id">Identifier.</param>
    /// <returns></returns>
    Task DeleteAsync(TKey id);
}
