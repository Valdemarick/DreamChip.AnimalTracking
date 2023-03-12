using DapperExtensions.Mapper;
using DreamChip.AnimalTracking.Domain.Entities;

namespace DreamChip.AnimalTracking.DAL.Mappers;

public sealed class AnimalVisitedLocationMapper : ClassMapper<AnimalVisitedLocation>
{
    public AnimalVisitedLocationMapper()
    {
        Table(nameof(AnimalVisitedLocation));
        Map(x => x.Id).Column("Id");
        Map(x => x.AnimalId).Column("AnimalId");
        Map(x => x.LocationId).Column("LocationId");
    }    
}
