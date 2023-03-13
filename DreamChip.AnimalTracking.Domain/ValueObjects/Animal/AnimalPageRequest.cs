using DreamChip.AnimalTracking.Domain.Enums;

namespace DreamChip.AnimalTracking.Domain.ValueObjects.Animal;

public sealed class AnimalPageRequest
{
    public DateTime? StartDateTime { get; set; }

    public DateTime? EndDateTime { get; set; }
    
    public Gender? Gender { get; set; } = null!;
    
    public LifeStatus? LifeStatus { get; set; } = null!;

    public int? ChipperId { get; set; }
    
    public long? ChippingLocationId { get; set; }

    public int From { get; set; }

    public int Size { get; set; } 
}
