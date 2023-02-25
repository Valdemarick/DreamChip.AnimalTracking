using AutoMapper;
using DreamChip.AnimalTracking.Application.Abstractions.Repositories;
using DreamChip.AnimalTracking.Application.Dto.Location;
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
}
