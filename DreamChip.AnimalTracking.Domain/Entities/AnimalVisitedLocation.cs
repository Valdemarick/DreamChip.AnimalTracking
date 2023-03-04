using System.ComponentModel.DataAnnotations.Schema;

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
        DateTimeOfVisitLocationPoint = DateTime.Now;
    }

    /// <summary>
    /// Date time, when the animal visited the location.
    /// </summary>
    [Column("date_time_of_visit_location_point")]
    public required DateTime DateTimeOfVisitLocationPoint { get; set; }

    /// <summary>
    /// The animal identifier.
    /// </summary>
    [Column("animal_id")]
    public required long AnimalId { get; set; }

    /// <summary>
    /// The location point identifier.
    /// </summary>
    [Column("location_id")]
    public required long LocationPointId { get; set; }
}
