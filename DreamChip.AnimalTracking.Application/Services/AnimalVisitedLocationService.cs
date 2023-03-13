using AutoMapper;
using DreamChip.AnimalTracking.Application.Abstractions.Repositories;
using DreamChip.AnimalTracking.Application.Abstractions.Services;
using DreamChip.AnimalTracking.Application.Dto.AnimalVisitedLocation;
using DreamChip.AnimalTracking.Domain.Exceptions.Animal;
using DreamChip.AnimalTracking.Domain.ValueObjects.AnimalVisitedLocation;
using LanguageExt.Common;

namespace DreamChip.AnimalTracking.Application.Services;

public sealed class AnimalVisitedLocationService : IAnimalVisitedLocationService
{
    private readonly IAnimalVisitedLocationRepository _animalVisitedLocationRepository;
    private readonly IAnimalRepository _animalRepository;
    private readonly IMapper _mapper;

    public AnimalVisitedLocationService(
        IAnimalVisitedLocationRepository animalVisitedLocationRepository,
        IAnimalRepository animalRepository,
        IMapper mapper)
    {
        _animalVisitedLocationRepository = animalVisitedLocationRepository;
        _animalRepository = animalRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<AnimalVisitedLocationDto>>> GetPageAsync(long animalId, AnimalVisitedLocationPageRequestDto dto)
    {
        var animal = await _animalRepository.GetByIdAsync(animalId);
        if (animal is null)
        {
            var exception = new AnimalNotFoundException();

            return new Result<List<AnimalVisitedLocationDto>>(exception);
        }

        var request = _mapper.Map<AnimalVisitedLocationPageRequest>(dto);

        var animalVisitedLocations = await _animalVisitedLocationRepository.GetPageAsync(animalId, request);

        var animalVisitedLocationDto = _mapper.Map<List<AnimalVisitedLocationDto>>(animalVisitedLocations);

        return new Result<List<AnimalVisitedLocationDto>>(animalVisitedLocationDto);
    }
}