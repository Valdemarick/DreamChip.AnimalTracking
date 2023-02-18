using DreamChip.AnimalTracking.Domain.Entities;

namespace DreamChip.AnimalTracking.Application.Abstractions.Repositories;

/// <summary>
/// An interface for Account Repository.
/// </summary>
public interface IAccountRepository
{
    /// <summary>
    /// Gets an account by its identifier.
    /// </summary>
    /// <param name="id">Identifier.</param>
    /// <returns>Existing account or null.</returns>
    Task<Account?> GetByIdAsync(long id);

    /// <summary>
    /// Returns an account by email.
    /// </summary>
    /// <param name="email">Input email.</param>
    /// <returns>Existing account or null.</returns>
    Task<Account?> GetByEmailAsync(string email);

    /// <summary>
    /// Creates a new account.
    /// </summary>
    /// <param name="account">Account entity.</param>
    /// <returns>Identifier of created account.</returns>
    Task<int> CreateAsync(Account account);
}
