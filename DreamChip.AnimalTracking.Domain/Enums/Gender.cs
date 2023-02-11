using System.ComponentModel;

namespace DreamChip.AnimalTracking.Domain.Enums;

/// <summary>
/// The enum with gender options.
/// </summary>
public enum Gender
{
    [Description("Male")]
    Male = 0,

    [Description("Female")]
    Female = 1,

    [Description("Other")]
    Other = 2,
}
