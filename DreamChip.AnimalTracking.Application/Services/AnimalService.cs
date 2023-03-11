﻿using AutoMapper;
using DreamChip.AnimalTracking.Application.Abstractions.Repositories;
using DreamChip.AnimalTracking.Application.Abstractions.Services;
using DreamChip.AnimalTracking.Application.Dto.Animal;
using DreamChip.AnimalTracking.Domain.Entities;
using DreamChip.AnimalTracking.Domain.Exceptions.Account;
using DreamChip.AnimalTracking.Domain.Exceptions.Animal;
using DreamChip.AnimalTracking.Domain.Exceptions.AnimalType;
using DreamChip.AnimalTracking.Domain.Exceptions.Location;
using DreamChip.AnimalTracking.Domain.ValueObjects.Animal;
using LanguageExt.Common;

namespace DreamChip.AnimalTracking.Application.Services;

public sealed class AnimalService : IAnimalService
{
    private readonly IAnimalRepository _animalRepository;
    private readonly IAnimalTypeRepository _animalTypeRepository;
    private readonly ILocationRepository _locationRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IMapper _mapper;

    public AnimalService(
        IAnimalRepository animalRepository, 
        IAnimalTypeRepository animalTypeRepository, 
        ILocationRepository locationRepository, 
        IAccountRepository accountRepository,
        IMapper mapper)
    {
        _animalRepository = animalRepository;
        _animalTypeRepository = animalTypeRepository;
        _locationRepository = locationRepository;
        _mapper = mapper;
        _accountRepository = accountRepository;
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

    public async Task<Result<List<AnimalDto>>> GetPageAsync(AnimalPageRequestDto dto)
    {
        var request = _mapper.Map<AnimalPageRequest>(dto);

        var animals = await _animalRepository.GetPageAsync(request);
        var animalsDto = _mapper.Map<List<AnimalDto>>(animals);

        return new Result<List<AnimalDto>>(animalsDto);
    }

    public async Task<Result<AnimalDto>> CreateAsync(CreateAnimalDto dto)
    {
        var chippingLocation = await _locationRepository.GetByIdAsync(dto.ChippingLocationId);
        if (chippingLocation is null)
        {
            var exception = new LocationNotFoundException();

            return new Result<AnimalDto>(exception);
        }

        var chipper = await _accountRepository.GetByIdAsync(dto.ChipperId);
        if (chipper is null)
        {
            var exception = new AccountNotFoundException();

            return new Result<AnimalDto>(exception);
        }

        var animalTypes = await _animalTypeRepository.GetByIdsAsync(dto.AnimalTypes);
        if (animalTypes.Count != dto.AnimalTypes.Count)
        {
            var exception = new AnimalTypeNotFoundException();

            return new Result<AnimalDto>(exception);
        }

        var animal = _mapper.Map<Animal>(dto);
        animal.AnimalTypes = animalTypes;

        var id = await _animalRepository.CreateAsync(animal);
        var createdAnimal = await _animalRepository.GetByIdAsync(id);
        if (createdAnimal is null)
        {
            var exception = new AnimalCreationFailedException();

            return new Result<AnimalDto>(exception);
        }

        var animalDto = _mapper.Map<AnimalDto>(createdAnimal);

        return new Result<AnimalDto>(animalDto);
    }
}
