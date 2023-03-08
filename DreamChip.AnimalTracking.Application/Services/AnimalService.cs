using AutoMapper;
using DreamChip.AnimalTracking.Application.Abstractions.Repositories;
using DreamChip.AnimalTracking.Application.Abstractions.Services;
using DreamChip.AnimalTracking.Application.Dto.Animal;
using DreamChip.AnimalTracking.Domain.Exceptions.Animal;
using LanguageExt.Common;

namespace DreamChip.AnimalTracking.Application.Services;

public sealed class AnimalService : IAnimalService
{
    private readonly IAnimalRepository _animalRepository;
    private readonly IAnimalTypeRepository _animalTypeRepository;
    private readonly ILocationRepository _locationRepository;
    private readonly IMapper _mapper;

    public AnimalService(
        IAnimalRepository animalRepository, 
        IAnimalTypeRepository animalTypeRepository, 
        ILocationRepository locationRepository, 
        IMapper mapper)
    {
        _animalRepository = animalRepository;
        _animalTypeRepository = animalTypeRepository;
        _locationRepository = locationRepository;
        _mapper = mapper;
    }

    public async Task<Result<AnimalDto>> GetByIdAsync(long id)
    {
        var animal = await _animalRepository.GetByIdAsync(id);
        if (animal is null)
        {
            var exception = new AnimalNotFoundException();

            return new Result<AnimalDto>(exception);
        }

        var animalDto = _mapper.Map<AnimalDto>(animal);

        return new Result<AnimalDto>(animalDto);
    }
}