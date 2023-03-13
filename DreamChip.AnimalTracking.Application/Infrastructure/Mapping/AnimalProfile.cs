using System.Globalization;
using AutoMapper;
using DreamChip.AnimalTracking.Application.Dto.Animal;
using DreamChip.AnimalTracking.Domain.Entities;
using DreamChip.AnimalTracking.Domain.Enums;
using DreamChip.AnimalTracking.Domain.ValueObjects.Animal;

namespace DreamChip.AnimalTracking.Application.Infrastructure.Mapping;

public class AnimalProfile : Profile
{
    public AnimalProfile()
    {
        CreateMap<Animal, AnimalDto>()
            .ForMember(dest => dest.AnimalTypes,
                src => src.MapFrom(
                    x => x.AnimalTypes.Select(at => at.Id)))
            .ForMember(dest => dest.VisitedLocations,
                src => src.MapFrom(
                    x => x.AnimalVisitedLocations.Select(avl => avl.Id)))
            .ForMember(dest => dest.ChippingDateTime,
                src => src.MapFrom(
                    x => x.ChippingLocation.ChippingDateTime.ToString("u")))
            .ForMember(dest => dest.DeathDateTime,
                src => src.MapFrom(
                    x => x.DeathDatetime.HasValue
                        ? x.DeathDatetime.Value.ToString()
                        : null))
            .ForMember(dest => dest.ChippingLocationId,
                src => src.MapFrom(
                    x => x.ChippingLocation.LocationId));

        CreateMap<AnimalPageRequestDto, AnimalPageRequest>();

        CreateMap<CreateAnimalDto, Animal>()
            .ForMember(dest => dest.LifeStatus,
                opt =>
                    opt.MapFrom(x => LifeStatus.Alive))
            .ForMember(dest => dest.ChippingLocation,
                opt => opt.MapFrom(
                    x => new AnimalChippingLocation { LocationId = x.ChippingLocationId }))
            .ForMember(dest => dest.Id,
                opt => opt.Ignore())
            .ForMember(dest => dest.AnimalTypes,
                opt => opt.MapFrom(src => src.AnimalTypes.Select(
                    x => new AnimalType(x))));

    }
}
