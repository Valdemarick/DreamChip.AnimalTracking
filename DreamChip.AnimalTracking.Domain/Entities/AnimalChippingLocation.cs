namespace DreamChip.AnimalTracking.Domain.Entities;

public sealed class AnimalChippingLocation : BaseEntity<long>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public AnimalChippingLocation()
    {
    }
    
    /// <summary>
    /// Animal identifier.
    /// </summary>
    public long AnimalId { get; set; }

    /// <summary>
    /// Location identifier, where the animal was chipped.
    /// </summary>
    public long LocationId { get; set; }

    /// <summary>
    /// Chipping date time.
    /// </summary>
    public DateTime ChippingDateTime { get; set; }
}
