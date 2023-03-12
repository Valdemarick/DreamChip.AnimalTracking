using DreamChip.AnimalTracking.Domain.Entities;
using DreamChip.AnimalTracking.Domain.ValueObjects.Account;

namespace DreamChip.AnimalTracking.Application.Abstractions.Repositories;

/// <summary>
/// An interface for Account Repository.
/// </summary>
public interface IAccountRepository : IBaseRepository<Account, int>
{
    /// <summary>
    /// Returns an account by email.
    /// </summary>
    /// <param name="email">Input email.</param>
    /// <returns>Existing account or null.</returns>
    Task<Account?> GetByEmailAsync(string? email);

    /// <summary>
    /// Gets page of accounts.
    /// </summary>
    /// /// <param name="request">Filter parameters.</param>
    /// <returns>Account list.</returns>
    Task<List<Account>> GetPageAsync(AccountPageRequest request);
}
