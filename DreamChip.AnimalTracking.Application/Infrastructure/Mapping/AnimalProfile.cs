using System.Globalization;
using AutoMapper;
using DreamChip.AnimalTracking.Application.Dto.Animal;
using DreamChip.AnimalTracking.Domain.Entities;
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
                    x => x.ChippingLocation.ChippingDateTime.ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffffffzzz",
                        CultureInfo.InvariantCulture)))
            .ForMember(dest => dest.DeathDateTime,
                src => src.MapFrom(
                    x => x.DeathDatetime.HasValue
                        ? x.DeathDatetime.Value.ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffffffzzz", CultureInfo.InvariantCulture)
                        : null));

        CreateMap<AnimalPageRequestDto, AnimalPageRequest>();
    }
}
