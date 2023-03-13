namespace DreamChip.AnimalTracking.Domain.ValueObjects.AnimalVisitedLocation;

public sealed class AnimalVisitedLocationPageRequest
{
    public DateTime? StartDateTime { get; set; }

    public DateTime? EndDateTime { get; set; }

    public int From { get; set; } 

    public int Size { get; set; } 
}
