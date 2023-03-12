using DapperExtensions.Mapper;
using DreamChip.AnimalTracking.Domain.Entities;

namespace DreamChip.AnimalTracking.DAL.Mappers;

public sealed class AnimalChippingLocationMapper : ClassMapper<AnimalChippingLocation>
{
    public AnimalChippingLocationMapper()
    {
        Table(nameof(AnimalChippingLocation));
        Map(x => x.Id).Column("Id");
        Map(x => x.AnimalId).Column("AnimalId");
        Map(x => x.LocationId).Column("LocationId");
        Map(x => x.ChippingDateTime).Column("ChippingDateTime");
    }    
}
