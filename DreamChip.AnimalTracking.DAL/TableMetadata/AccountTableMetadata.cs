using DreamChip.AnimalTracking.Application.Abstractions;
using DreamChip.AnimalTracking.Domain.Entities;

namespace DreamChip.AnimalTracking.DAL.RepositoryMetadata;

public sealed class AccountTableMetadata : ITableMetadata
{
    public static string TableName => nameof(Account);


    public static string[] Columns { get; } = { "Id", "FirstName", "LastName", "Email", "Password" };
}
