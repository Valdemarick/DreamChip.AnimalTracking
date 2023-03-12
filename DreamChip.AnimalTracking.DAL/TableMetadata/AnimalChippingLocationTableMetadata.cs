using DreamChip.AnimalTracking.Application.Abstractions;
using DreamChip.AnimalTracking.Domain.Entities;

namespace DreamChip.AnimalTracking.DAL.RepositoryMetadata;

public sealed class AnimalChippingLocationTableMetadata : ITableMetadata
{
    public static string TableName => nameof(AnimalChippingLocation);

    public static string[] Columns { get; } = { "Id", "AnimalId", "LocationId", "ChippingDateTime" };
}
