using DreamChip.AnimalTracking.Domain.Entities;
using DreamChip.AnimalTracking.Domain.ValueObjects.Animal;

namespace DreamChip.AnimalTracking.Application.Abstractions.Repositories;

public interface IAnimalRepository
{
    Task<Animal?> GetByIdAsync(long id);
    Task<List<Animal>> GetPageAsync(AnimalPageRequest request);
}
