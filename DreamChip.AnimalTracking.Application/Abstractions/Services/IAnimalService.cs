using DreamChip.AnimalTracking.Application.Dto.Animal;
using LanguageExt.Common;

namespace DreamChip.AnimalTracking.Application.Abstractions.Services;

public interface IAnimalService
{
    Task<Result<AnimalDto>> GetByIdAsync(long id);

    Task<Result<List<AnimalDto>>> GetPageAsync(AnimalPageRequestDto dto);

    Task<Result<AnimalDto>> CreateAsync(CreateAnimalDto dto);

    Task<Result<AnimalDto>> AddTypeAsync(long animalId, long typeId);

    Task<Result<AnimalDto>> DeleteAsync(long id);
}
