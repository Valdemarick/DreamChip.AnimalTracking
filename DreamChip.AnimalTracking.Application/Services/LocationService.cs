using AutoMapper;
using DreamChip.AnimalTracking.Application.Abstractions.Repositories;
using DreamChip.AnimalTracking.Application.Dto.Location;
using DreamChip.AnimalTracking.Domain.Entities;
using DreamChip.AnimalTracking.Domain.Exceptions.Location;
using LanguageExt.Common;

namespace DreamChip.AnimalTracking.Application.Services;

public sealed class LocationService
{
    private readonly ILocationRepository _locationRepository;
    private readonly IMapper _mapper;

    public LocationService(ILocationRepository locationRepository, IMapper mapper)
    {
        _locationRepository = locationRepository;
        _mapper = mapper;
    }

    public async Task<Result<LocationDto>> GetByIdAsync(long id)
    {
        var location = await _locationRepository.GetByIdAsync(id);
        if (location is null)
        {
            var exception = new LocationNotFoundException();

            return new Result<LocationDto>(exception);
        }
        
        var locationDto = _mapper.Map<LocationDto>(location);

        return new Result<LocationDto>(locationDto);
    }

    public async Task<Result<LocationDto>> CreateAsync(CreateLocationDto dto)
    {
        var location = await _locationRepository.GetByCoordinatesAsync(dto.Latitude, dto.Longitude);
        if (location is not null)
        {
            var exception = new LocationWithSuchCoordinatesAlreadyExistsException();

            return new Result<LocationDto>(exception);
        }

        location = _mapper.Map<Location>(dto);

        var id = await _locationRepository.CreateAsync(location);
        location.Id = id;

        var locationDto = _mapper.Map<LocationDto>(location);

        return new Result<LocationDto>(locationDto);
    }

    public async Task<Result<LocationDto>> UpdateAsync(long id, UpdateLocationDto dto)
    {
        var location = await _locationRepository.GetByIdAsync(id);
        if (location is null)
        {
            var exception = new LocationNotFoundException();

            return new Result<LocationDto>(exception);
        }

        location = await _locationRepository.GetByCoordinatesAsync(dto.Latitude, dto.Longitude);
        if (location is not null)
        {
            var exception = new LocationWithSuchCoordinatesAlreadyExistsException();

            return new Result<LocationDto>(exception);
        }

        location = _mapper.Map<Location>(dto);
        location.Id = id;

        await _locationRepository.UpdateAsync(location);

        var locationDto = _mapper.Map<LocationDto>(location);

        return new Result<LocationDto>(locationDto);
    }

    public async Task<Result<LocationDto>> DeleteAsync(long id)
    {
        var location = await _locationRepository.GetByIdAsync(id);
        if (location is null)
        {
            var exception = new LocationNotFoundException();

            return new Result<LocationDto>(exception);
        }

        if (location.AnimalVisitedLocations.Any())
        {
            var exception = new LocationLinkedWithAnimalException();

            return new Result<LocationDto>(exception);
        }

        await _locationRepository.DeleteAsync(id);

        return new Result<LocationDto>(new LocationDto());
    }
}
