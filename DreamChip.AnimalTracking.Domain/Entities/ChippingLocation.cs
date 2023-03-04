namespace DreamChip.AnimalTracking.Domain.Entities;

/// <summary>
/// Entity that described the point, where the animal was chipped.
/// </summary>
public sealed class ChippingLocation : BaseEntity<long>
{
    /// <summary>
    /// Animal identifier.
    /// </summary>
    public required long AnimalId { get; set; }

    /// <summary>
    /// Location identifier.
    /// </summary>
    public required long LocationId { get; set; }
}
