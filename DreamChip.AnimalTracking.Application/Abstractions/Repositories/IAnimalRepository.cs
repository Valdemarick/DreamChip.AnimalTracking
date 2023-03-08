using DreamChip.AnimalTracking.Domain.Entities;

namespace DreamChip.AnimalTracking.Application.Abstractions.Repositories;

public interface IAnimalRepository
{
    Task<Animal?> GetByIdAsync(long id);
}
