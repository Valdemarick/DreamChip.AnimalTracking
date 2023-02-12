using System.ComponentModel.DataAnnotations.Schema;
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
    [Column("weight")]
    public required float Weight { get; set; }

    /// <summary>
    /// Length of animal in meters.
    /// </summary>
    [Column("length")]
    public required float Length { get; set; }

    /// <summary>
    /// Height of animal in meters.
    /// </summary>
    [Column("height")]
    public required float Height { get; set; }

    /// <summary>
    /// Gender type.
    /// </summary>
    [Column("gender")]
    public required Gender Gender { get; set; }

    /// <summary>
    /// Identifier of the chipper account.
    /// </summary>
    [Column("chipper_id")]
    public required int ChipperId { get; set; }

    /// <summary>
    /// Identifier of the animal's location point.
    /// </summary>
    [Column("chipping_location_id")]
    public required long ChippingLocationId { get; set; }

    /// <summary>
    /// Animal life status. By default Alive.
    /// </summary>
    [Column("life_status")]
    public required LifeStatus LifeStatus { get; set; }

    /// <summary>
    /// Date time when the animal was chipped.
    /// </summary>
    [Column("chipping_date_time")]
    public required DateTime ChippingDateTime { get; set; }

    /// <summary>
    /// Date time when the animal died.
    /// </summary>
    [Column("death_date_time")]
    public DateTime? DeathDatetime { get; set; }

    /// <summary>
    /// Animal types.
    /// </summary>
    public List<AnimalType> AnimalTypes { get; set; }

    /// <summary>
    /// The locations, that were visited by the animal.
    /// </summary>
    public List<AnimalVisitedLocation> AnimalVisitedLocations { get; set; }
}