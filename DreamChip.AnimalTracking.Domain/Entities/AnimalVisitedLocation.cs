namespace DreamChip.AnimalTracking.Domain.Entities;

/// <summary>
/// Relationship between Animal and Location Point.
/// </summary>
public sealed class AnimalVisitedLocation : BaseEntity<long>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public AnimalVisitedLocation()
    {
    }

    /// <summary>
    /// Date time, when the animal visited the location.
    /// </summary>
    public required DateTime DateTimeOfVisitLocationPoint { get; set; }

    /// <summary>
    /// The animal identifier.
    /// </summary>
    public required long AnimalId { get; set; }

    /// <summary>
    /// The location point identifier.
    /// </summary>
    public required long LocationId { get; set; }
}
