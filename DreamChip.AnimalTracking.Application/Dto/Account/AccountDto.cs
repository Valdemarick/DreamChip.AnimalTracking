namespace DreamChip.AnimalTracking.Application.Dto.Account;

/// <summary>
/// Account dto is used t0 view data.
/// </summary>
public sealed class AccountDto
{
    /// <summary>
    /// User identifier.
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// User first name.
    /// </summary>
    public string FirstName { get; set; } = null!;

    /// <summary>
    /// User last name.
    /// </summary>
    public string LastName { get; set; } = null!;

    /// <summary>
    /// User email.
    /// </summary>
    public string Email { get; set; } = null!;
}
