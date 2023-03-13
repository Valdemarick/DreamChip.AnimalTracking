using DreamChip.AnimalTracking.Domain.Entities;

namespace DreamChip.AnimalTracking.Application.Abstractions.Repositories;

public interface IAnimalTypeRepository : IBaseRepository<AnimalType, long>
{
    Task<List<AnimalType>> GetByIdsAsync(List<long> ids);
    Task<AnimalType?> GetByName(string type);
}
