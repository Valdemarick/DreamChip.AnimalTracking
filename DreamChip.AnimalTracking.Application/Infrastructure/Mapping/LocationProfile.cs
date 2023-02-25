using AutoMapper;
using DreamChip.AnimalTracking.Application.Dto.Location;
using DreamChip.AnimalTracking.Domain.Entities;

namespace DreamChip.AnimalTracking.Application.Infrastructure.Mapping;

public sealed class LocationProfile : Profile
{
    public LocationProfile()
    {
        CreateMap<Location, LocationDto>();

        CreateMap<CreateLocationDto, Location>()
            .ForMember(dest => dest.Id, 
                opt => opt.Ignore());
    }
}
