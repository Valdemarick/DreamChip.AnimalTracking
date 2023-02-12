using System.ComponentModel.DataAnnotations.Schema;

namespace DreamChip.AnimalTracking.Domain.Entities;

/// <summary>
/// Animal type entity.
/// </summary>
public sealed class AnimalType : BaseEntity<long>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public AnimalType()
    {
        Animals = new List<Animal>();
    }

    /// <summary>
    /// Type name.
    /// </summary>
    [Column("type")]
    public required string Type { get; set; }

    /// <summary>
    /// Animals of this type.
    /// </summary>
    public List<Animal> Animals { get; set; }
}