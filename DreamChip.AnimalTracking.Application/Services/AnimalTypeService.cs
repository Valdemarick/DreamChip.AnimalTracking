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

    public async Task<Result<AnimalTypeDto>> CreateAsync(CreateAnimalTypeDto dto)
    {
        var animalType = await _animalTypeRepository.GetByName(dto.Type);
        if (animalType is not null)
        {
            var exception = new AnimalTypeWithSuchNameAlreadyExistsException();

            return new Result<AnimalTypeDto>(exception);
        }

        animalType = _mapper.Map<AnimalType>(dto);

        var id = await _animalTypeRepository.CreateAsync(animalType);
        animalType.Id = id;

        var animalTypeDto = _mapper.Map<AnimalTypeDto>(animalType);

        return animalTypeDto;
    }

    public async Task<Result<AnimalTypeDto>> UpdateAsync(long id, UpdateAnimalTypeDto dto)
    {
        var animalType = await _animalTypeRepository.GetByIdAsync(id);
        if (animalType is null)
        {
            var exception = new AnimalTypeNotFoundException();

            return new Result<AnimalTypeDto>(exception);
        }

        animalType = await _animalTypeRepository.GetByName(dto.Type);
        if (animalType is not null)
        {
            var exception = new AnimalTypeWithSuchNameAlreadyExistsException();

            return new Result<AnimalTypeDto>(exception);
        }

        animalType = new AnimalType()
        {
            Id = id,
            Type = dto.Type
        };

        await _animalTypeRepository.UpdateAsync(animalType);

        var animalTypeDto = _mapper.Map<AnimalTypeDto>(animalType);

        return new Result<AnimalTypeDto>(animalTypeDto);
    }

    public async Task<Result<AnimalTypeDto>> DeleteAsync(long id)
    {
        var animalType = await _animalTypeRepository.GetByIdAsync(id);
        if (animalType is null)
        {
            var exception = new AnimalTypeNotFoundException();

            return new Result<AnimalTypeDto>(exception);
        }

        if (animalType.Animals.Count > 0)
        {
            var exception = new AnimalTypeLinkedWithAnimalException();

            return new Result<AnimalTypeDto>(exception);
        }

        await _animalTypeRepository.DeleteAsync(id);

        return new Result<AnimalTypeDto>(new AnimalTypeDto());
    }
}
