using DreamChip.AnimalTracking.Application.Dto.AnimalType;
using LanguageExt.Common;

namespace DreamChip.AnimalTracking.Application.Abstractions.Services;

public interface IAnimalTypeService
{
    Task<Result<AnimalTypeDto?>> GetByIdAsync(long id);
}
