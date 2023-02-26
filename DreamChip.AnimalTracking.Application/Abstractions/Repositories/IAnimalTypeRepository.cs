using DreamChip.AnimalTracking.Domain.Entities;

namespace DreamChip.AnimalTracking.Application.Abstractions.Repositories;

public interface IAnimalTypeRepository
{
    Task<AnimalType?> GetByIdAsync(long id);
}
