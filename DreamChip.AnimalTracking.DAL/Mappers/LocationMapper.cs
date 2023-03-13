using DapperExtensions.Mapper;
using DreamChip.AnimalTracking.Domain.Entities;

namespace DreamChip.AnimalTracking.DAL.Mappers;

public sealed class LocationMapper : ClassMapper<Location>
{
    public LocationMapper()
    {
        Table(nameof(Location));
        Map(x => x.Id).Column("Id");
        Map(x => x.Latitude).Column("Latitude");
        Map(x => x.Longitude).Column("Longitude");
        Map(x => x.ChippingLocations).Ignore();
        Map(x => x.AnimalVisitedLocations).Ignore();
    }
}
