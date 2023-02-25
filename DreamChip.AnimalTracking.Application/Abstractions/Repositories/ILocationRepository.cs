using DreamChip.AnimalTracking.Domain.Entities;
using Serilog;

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
}