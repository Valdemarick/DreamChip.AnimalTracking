using System.ComponentModel.DataAnnotations.Schema;

namespace DreamChip.AnimalTracking.Domain.Entities;

public sealed class AnimalChippingLocation : BaseEntity<long>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public AnimalChippingLocation()
    {
        Animals = new List<Animal>();
    }
    
    /// <summary>
    /// Animal identifier.
    /// </summary>
    [Column("animal_id")]
    public long AnimalId { get; set; }

    /// <summary>
    /// Location identifier, where the animal was chipped.
    /// </summary>
    [Column("location_id")]
    public long LocationId { get; set; }

    /// <summary>
    /// Chipping date time.
    /// </summary>
    [Column("chipping_date_time")]
    public DateTime ChippingDateTime { get; set; }

    /// <summary>
    /// The animals, that were chipped at this location
    /// </summary>
    public List<Animal> Animals { get; set; }
}