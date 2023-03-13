using DapperExtensions.Mapper;
using DreamChip.AnimalTracking.Domain.Entities;

namespace DreamChip.AnimalTracking.DAL.Mappers;

public sealed class AnimalTypeMapper : ClassMapper<AnimalType>
{
    public AnimalTypeMapper()
    {
        Table(nameof(AnimalType));
        Map(x => x.Id).Column("Id");
        Map(x => x.Type).Column("Type");
        Map(x => x.Animals).Ignore();
    }
}
