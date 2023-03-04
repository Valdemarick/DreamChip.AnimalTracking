namespace DreamChip.AnimalTracking.Domain.Entities;

/// <summary>
/// Location Point entity.
/// </summary>
public sealed class Location : BaseEntity<long>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public Location()
    {
        AnimalVisitedLocations = new List<AnimalVisitedLocation>();
    }

    /// <summary>
    /// Geographic latitude in degrees.
    /// </summary>
    public required double Latitude { get; set; }

    /// <summary>
    /// Geographic longitude in degrees.
    /// </summary>
    public required double Longitude { get; set; }

    /// <summary>
    /// Animal that were at this point.
    /// </summary>
    public List<AnimalVisitedLocation> AnimalVisitedLocations { get; set; }

    public ChippingLocation ChippingLocations { get; set; }
}
