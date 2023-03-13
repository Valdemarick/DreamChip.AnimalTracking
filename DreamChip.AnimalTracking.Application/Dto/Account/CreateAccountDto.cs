namespace DreamChip.AnimalTracking.Application.Dto.Account;

public sealed class CreateAccountDto
{
    /// <summary>
    /// User first name
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

    /// <summary>
    /// User password.
    /// </summary>
    public string Password { get; set; } = null!;
}
