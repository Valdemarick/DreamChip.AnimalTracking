using DreamChip.AnimalTracking.Application.Abstractions;
using DreamChip.AnimalTracking.Domain.Entities;

namespace DreamChip.AnimalTracking.DAL.RepositoryMetadata;

public sealed class AnimalTypeTableMetadata : ITableMetadata
{
    public static string TableName => nameof(AnimalType);
    
    public static string[] Columns { get; } = { "Id", "Type" };
}
