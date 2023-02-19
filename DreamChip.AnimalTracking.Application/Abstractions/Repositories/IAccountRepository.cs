using DreamChip.AnimalTracking.Domain.Entities;
using DreamChip.AnimalTracking.Domain.ValueObjects.Account;

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
    Task<Account?> GetByIdAsync(int id);

    /// <summary>
    /// Returns an account by email.
    /// </summary>
    /// <param name="email">Input email.</param>
    /// <returns>Existing account or null.</returns>
    Task<Account?> GetByEmailAsync(string email);

    /// <summary>
    /// Gets page of accounts.
    /// </summary>
    /// /// <param name="request">Filter parameters.</param>
    /// <returns>Account list.</returns>
    Task<List<Account>> GetPageAsync(AccountPageRequest request);

    /// <summary>
    /// Creates a new account.
    /// </summary>
    /// <param name="account">Account entity.</param>
    /// <returns>Identifier of created account.</returns>
    Task<int> CreateAsync(Account account);

    /// <summary>
    /// Deletes account by idenitifier.
    /// </summary>
    /// <param name="id">Account identifier.</param>
    /// <returns></returns>
    Task DeleteAsync(int id);
}
