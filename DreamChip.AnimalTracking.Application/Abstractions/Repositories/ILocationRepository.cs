using DreamChip.AnimalTracking.Domain.Entities;

namespace DreamChip.AnimalTracking.Application.Abstractions.Repositories;

/// <summary>
/// Interface for Location Repository
/// </summary>
public interface ILocationRepository
{
    /// <summary>
    /// Gets location by identifier.
    /// </summary>
    /// <param name="id">Identifier.</param>
    /// <returns>Particular location or null.</returns>
    Task<Location?> GetByIdAsync(long id);

    /// <summary>
    /// Gets location by coordinates.
    /// </summary>
    /// <param name="latitude">Geographic latitude.</param>
    /// <param name="longitude">Geographic longitude.</param>
    /// <returns>Existing location or null.</returns>
    Task<Location?> GetByCoordinatesAsync(double latitude, double longitude);

    /// <summary>
    /// Creates a new location.
    /// </summary>
    /// <param name="location">Location entity.</param>
    /// <returns>Identifier.</returns>
    Task<long> CreateAsync(Location location);

    /// <summary>
    /// Updates the location.
    /// </summary>
    /// <param name="location">New data.</param>
    /// <returns>Updated location.</returns>
    Task<Location> UpdateAsync(Location location);

    /// <summary>
    /// Deletes location by identifier.
    /// </summary>
    /// <param name="id">Identifier.</param>
    /// <returns></returns>
    Task DeleteAsync(long id);
}
