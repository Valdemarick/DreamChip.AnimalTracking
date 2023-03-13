using AutoMapper;
using DreamChip.AnimalTracking.Application.Dto.AnimalVisitedLocation;
using DreamChip.AnimalTracking.Domain.Entities;
using DreamChip.AnimalTracking.Domain.ValueObjects.AnimalVisitedLocation;

namespace DreamChip.AnimalTracking.Application.Infrastructure.Mapping;

public sealed class AnimalVisitedLocationProfile : Profile
{
    public AnimalVisitedLocationProfile()
    {
        CreateMap<AnimalVisitedLocation, AnimalVisitedLocationDto>();
        
        CreateMap<AnimalVisitedLocationPageRequestDto, AnimalVisitedLocationPageRequest>();
    }    
}
