using DreamChip.AnimalTracking.Application.Abstractions;

namespace DreamChip.AnimalTracking.DAL.RepositoryMetadata;

public sealed class AnimalTypeAnimalTableMetadata : ITableMetadata
{
    public static string TableName => "AnimalTypeAnimal";
    
    public static string[] Columns { get; } = { "AnimalId", "AnimalTypeId" };
}
