namespace DreamChip.AnimalTracking.Application.Infrastructure.DtoValidators.Account;

public static class AccountValidatorRules
{
    public static bool IsPasswordValid(string? password)
    {
        return !string.IsNullOrEmpty(password?.Trim());
    }

    public static bool IsFirstNameValid(string? firstName)
    {
        return !string.IsNullOrEmpty(firstName?.Trim());
    }

    public static bool IsLastNameValid(string? lastName)
    {
        return !string.IsNullOrEmpty(lastName?.Trim());
    }

    public static bool IsEmailValid(string? email)
    {
        return !string.IsNullOrEmpty(email?.Trim());
    }

}
