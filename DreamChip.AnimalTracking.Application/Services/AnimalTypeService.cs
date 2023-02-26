using AutoMapper;
using DreamChip.AnimalTracking.Application.Abstractions.Repositories;
using DreamChip.AnimalTracking.Application.Abstractions.Services;
using DreamChip.AnimalTracking.Application.Dto.AnimalType;
using DreamChip.AnimalTracking.Domain.Entities;
using DreamChip.AnimalTracking.Domain.Exceptions.AnimalType;
using LanguageExt.Common;

namespace DreamChip.AnimalTracking.Application.Services;

public sealed class AnimalTypeService : IAnimalTypeService
{
    private readonly IAnimalTypeRepository _animalTypeRepository;
    private readonly IMapper _mapper;

    public AnimalTypeService(IAnimalTypeRepository animalTypeRepository, IMapper mapper)
    {
        _animalTypeRepository = animalTypeRepository;
        _mapper = mapper;
    }
    
    public async Task<Result<AnimalTypeDto?>> GetByIdAsync(long id)
    {
        var animalType = await _animalTypeRepository.GetByIdAsync(id);
        if (animalType is null)
        {
            var exception = new AnimalTypeNotFoundException();

            return new Result<AnimalTypeDto?>(exception);
        }

        var animalTypeDto = _mapper.Map<AnimalTypeDto>(animalType);

        return animalTypeDto;
    }
}