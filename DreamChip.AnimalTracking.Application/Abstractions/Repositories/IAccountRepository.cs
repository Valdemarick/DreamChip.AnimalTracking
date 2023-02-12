using DreamChip.AnimalTracking.Domain.Entities;

namespace DreamChip.AnimalTracking.Application.Abstractions.Repositories;

/// <summary>
/// An interface for Account Repository.
/// </summary>
public interface IAccountRepository
{
    /// <summary>
    /// Returns an account by email.
    /// </summary>
    /// <param name="email">Input email.</param>
    /// <returns>Existing account or null.</returns>
    Task<Account?> GetByEmail(string email);

    /// <summary>
    /// Creates a new account.
    /// </summary>
    /// <param name="account">Account entity.</param>
    /// <returns>Created account.</returns>
    Task<int> CreateAsync(Account account);
}
