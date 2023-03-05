using AutoMapper;
using DreamChip.AnimalTracking.Application.Dto.AnimalType;
using DreamChip.AnimalTracking.Domain.Entities;

namespace DreamChip.AnimalTracking.Application.Infrastructure.Mapping;

public sealed class AnimalTypeProfile : Profile
{
    public AnimalTypeProfile()
    {
        CreateMap<AnimalType, AnimalTypeDto>();

        CreateMap<CreateAnimalTypeDto, AnimalType>()
            .ForMember(dest => dest.Id,
                opt => opt.Ignore());
    }
}
