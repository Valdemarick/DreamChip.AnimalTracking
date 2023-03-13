namespace DreamChip.AnimalTracking.Application.Infrastructure.DtoValidators.AnimalType;

public static class AnimalTypeValidatorRules
{
    public static bool IsTypeNameValid(string? typeName)
    {
        return !string.IsNullOrWhiteSpace(typeName);
    }
}
