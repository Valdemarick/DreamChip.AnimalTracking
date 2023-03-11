using DreamChip.AnimalTracking.Domain.Entities;

namespace DreamChip.AnimalTracking.Application.Abstractions.Repositories;

public interface IAnimalTypeRepository
{
    Task<AnimalType?> GetByIdAsync(long id);
    Task<List<AnimalType>> GetByIdsAsync(List<long> ids);
    Task<AnimalType?> GetByName(string type);
    Task<long> CreateAsync(AnimalType animalType);
    Task UpdateAsync(AnimalType animalType);
    Task DeleteAsync(long id);
}
