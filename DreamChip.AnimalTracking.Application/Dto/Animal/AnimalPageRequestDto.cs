namespace DreamChip.AnimalTracking.Application.Dto.Animal;

public sealed class AnimalPageRequestDto
{
    public DateTime? StartDateTime { get; set; }

    public DateTime? EndDateTime { get; set; }
    
    public string? Gender { get; set; } = null!;
    
    public string? LifeStatus { get; set; } = null!;

    public int? ChipperId { get; set; }
    
    public long? ChippingLocationId { get; set; }

    public int From { get; set; } = 0;

    public int Size { get; set; } = 10;
}
