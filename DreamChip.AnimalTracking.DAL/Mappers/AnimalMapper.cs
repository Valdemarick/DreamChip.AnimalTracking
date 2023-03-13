using DapperExtensions.Mapper;
using DreamChip.AnimalTracking.Domain.Entities;

namespace DreamChip.AnimalTracking.DAL.Mappers;

public sealed class AnimalMapper : ClassMapper<Animal>
{
    public AnimalMapper()
    {
        Table(nameof(Animal));
        Map(x => x.Id).Column("Id");
        Map(x => x.Length).Column("Length");
        Map(x => x.Weight).Column("Weight");
        Map(x => x.Height).Column("Height");
        Map(x => x.Gender).Column("Gender");
        Map(x => x.LifeStatus).Column("LifeStatus");
        Map(x => x.ChipperId).Column("ChipperId");
        Map(x => x.DeathDatetime).Column("DeathDateTime");
        Map(x => x.AnimalTypes).Ignore();
        Map(x => x.AnimalVisitedLocations).Ignore();
        Map(x => x.ChippingLocation).Ignore();
    }
}
