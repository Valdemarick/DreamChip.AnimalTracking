using DreamChip.AnimalTracking.Domain.Entities;
using DreamChip.AnimalTracking.Domain.ValueObjects.AnimalVisitedLocation;

namespace DreamChip.AnimalTracking.Application.Abstractions.Repositories;

public interface IAnimalVisitedLocationRepository : IBaseRepository<AnimalVisitedLocation, long>
{
    Task<List<AnimalVisitedLocation>> GetPageAsync(long animalId, AnimalVisitedLocationPageRequest request);
}
