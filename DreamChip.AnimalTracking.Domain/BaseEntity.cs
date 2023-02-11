using System.ComponentModel.DataAnnotations.Schema;

namespace DreamChip.AnimalTracking.Domain;

/// <summary>
/// Base entity.
/// </summary>
/// <typeparam name="TKey">Key type.</typeparam>
public abstract class BaseEntity<TKey>
{
    /// <summary>
    /// Identifier.
    /// </summary>
    [Column("id")]
    public required TKey Id { get; set; }
}
