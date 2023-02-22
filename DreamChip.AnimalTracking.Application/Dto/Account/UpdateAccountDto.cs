namespace DreamChip.AnimalTracking.Application.Dto.Account;

public sealed class UpdateAccountDto
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
}
