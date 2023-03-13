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
    /// Constructor for AutoMapper.
    /// </summary>
    /// <param name="id">Animal type identifier.</param>
    public AnimalType(long id) : this()
    {
        Id = id;
    }

    /// <summary>
    /// Type name.
    /// </summary>
    public string Type { get; set; }

    /// <summary>
    /// Animals of this type.
    /// </summary>
    public List<Animal> Animals { get; set; }
}
