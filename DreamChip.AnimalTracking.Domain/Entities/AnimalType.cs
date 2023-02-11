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
    /// <param name="type">Type name.</param>
    public AnimalType(string type)
    {
        Type = type;

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