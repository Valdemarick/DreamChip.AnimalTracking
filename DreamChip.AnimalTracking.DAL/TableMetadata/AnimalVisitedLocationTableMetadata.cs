using DreamChip.AnimalTracking.Application.Abstractions;
using DreamChip.AnimalTracking.Domain.Entities;

namespace DreamChip.AnimalTracking.DAL.RepositoryMetadata;

public sealed class AnimalVisitedLocationTableMetadata : ITableMetadata
{
    public static string TableName => nameof(AnimalVisitedLocation);
    
    public static string[] Columns { get; } = { "Id", "AnimalId", "LocationId", "DateTimeOfVisitLocation" };
}
