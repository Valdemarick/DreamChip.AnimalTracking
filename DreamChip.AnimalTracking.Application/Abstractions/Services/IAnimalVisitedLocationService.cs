using DreamChip.AnimalTracking.Application.Dto.AnimalVisitedLocation;
using LanguageExt.Common;

namespace DreamChip.AnimalTracking.Application.Abstractions.Services;

public interface IAnimalVisitedLocationService
{
    Task<Result<List<AnimalVisitedLocationDto>>> GetPageAsync(long animalId, AnimalVisitedLocationPageRequestDto dto);
}