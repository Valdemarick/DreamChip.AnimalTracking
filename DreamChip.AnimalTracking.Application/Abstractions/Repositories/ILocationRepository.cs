using DreamChip.AnimalTracking.Domain.Entities;

namespace DreamChip.AnimalTracking.Application.Abstractions.Repositories;

/// <summary>
/// Interface for Location Repository
/// </summary>
public interface ILocationRepository : IBaseRepository<Location, long>
{
    /// <summary>
    /// Gets location by coordinates.
    /// </summary>
    /// <param name="latitude">Geographic latitude.</param>
    /// <param name="longitude">Geographic longitude.</param>
    /// <returns>Existing location or null.</returns>
    Task<Location?> GetByCoordinatesAsync(double latitude, double longitude);
}
