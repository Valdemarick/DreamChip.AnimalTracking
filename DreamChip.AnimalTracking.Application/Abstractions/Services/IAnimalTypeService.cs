using DreamChip.AnimalTracking.Application.Dto.AnimalType;
using LanguageExt.Common;

namespace DreamChip.AnimalTracking.Application.Abstractions.Services;

public interface IAnimalTypeService
{
    Task<Result<AnimalTypeDto?>> GetByIdAsync(long id);
    Task<Result<AnimalTypeDto>> CreateAsync(CreateAnimalTypeDto dto);
    Task<Result<AnimalTypeDto>> UpdateAsync(long id, UpdateAnimalTypeDto dto);
    Task<Result<AnimalTypeDto>> DeleteAsync(long id);
}
