namespace DreamChip.AnimalTracking.Domain.Entities;

/// <summary>
/// Location Point entity.
/// </summary>
public sealed class LocationPoint : BaseEntity<long>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="latitude">Latitude.</param>
    /// <param name="longitude">Longitude.</param>
    public LocationPoint(double latitude, double longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }

    /// <summary>
    /// Geographic latitude in degrees.
    /// </summary>
    public required double Latitude { get; set; }

    /// <summary>
    /// Geographic longitude in degrees.
    /// </summary>
    public required double Longitude { get; set; }
}
