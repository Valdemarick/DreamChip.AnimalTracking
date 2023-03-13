using DreamChip.AnimalTracking.Application.Abstractions;
using DreamChip.AnimalTracking.Domain.Entities;

namespace DreamChip.AnimalTracking.DAL.RepositoryMetadata;

public sealed class AnimalTableMetadata : ITableMetadata
{
    public static string TableName => nameof(Animal);

    public static string[] Columns { get; } = { "Id", "Weight", "Length", "Height", "Gender", "ChipperId", "LifeStatus", "DeathDateTime" };
}
