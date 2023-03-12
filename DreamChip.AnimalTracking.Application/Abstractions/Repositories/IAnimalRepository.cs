using DreamChip.AnimalTracking.Domain.Entities;
using DreamChip.AnimalTracking.Domain.ValueObjects.Animal;

namespace DreamChip.AnimalTracking.Application.Abstractions.Repositories;

public interface IAnimalRepository : IBaseRepository<Animal, long>
{
    Task<List<Animal>> GetPageAsync(AnimalPageRequest request);
    Task AddTypeAsync(long animalId, long typeId);
}
