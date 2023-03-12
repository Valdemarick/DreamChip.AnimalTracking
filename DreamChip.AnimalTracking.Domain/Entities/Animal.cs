using DreamChip.AnimalTracking.Domain.Enums;

namespace DreamChip.AnimalTracking.Domain.Entities;

/// <summary>
/// Animal entity.
/// </summary>
public sealed class Animal : BaseEntity<long>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public Animal()
    {
        AnimalTypes = new List<AnimalType>();
        AnimalVisitedLocations = new List<AnimalVisitedLocation>();
    }

    /// <summary>
    /// Weight of animal in kilograms.
    /// </summary>
    public required float Weight { get; set; }

    /// <summary>
    /// Length of animal in meters.
    /// </summary>
    public required float Length { get; set; }

    /// <summary>
    /// Height of animal in meters.
    /// </summary>
    public required float Height { get; set; }

    /// <summary>
    /// Gender type.
    /// </summary>
    public required Gender Gender { get; set; }

    /// <summary>
    /// Identifier of the chipper account.
    /// </summary>
    public required int ChipperId { get; set; }

    /// <summary>
    /// Animal life status. By default Alive.
    /// </summary>
    public required LifeStatus LifeStatus { get; set; }

    /// <summary>
    /// Date time when the animal died.
    /// </summary>
    public DateTime? DeathDatetime { get; set; }

    /// <summary>
    /// The location where the animal was chipped.
    /// </summary>
    public AnimalChippingLocation ChippingLocation { get; set; }

    /// <summary>
    /// Animal types.
    /// </summary>
    public List<AnimalType> AnimalTypes { get; set; }

    /// <summary>
    /// The locations, that were visited by the animal.
    /// </summary>
    public List<AnimalVisitedLocation> AnimalVisitedLocations { get; set; }
}
