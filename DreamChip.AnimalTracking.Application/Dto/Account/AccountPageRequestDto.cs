namespace DreamChip.AnimalTracking.Application.Dto.Account;

public sealed class AccountPageRequestDto
{
    /// <summary>
    /// First name filter.
    /// </summary>
    public string? FirstName { get; set; }

    /// <summary>
    /// Last name filter.
    /// </summary>
    public string? LastName { get; set; }

    /// <summary>
    /// Email filter.
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Element number for skipping.
    /// </summary>
    public int From { get; set; }

    /// <summary>
    /// Page size.
    /// </summary>
    public int Size { get; set; }
}
