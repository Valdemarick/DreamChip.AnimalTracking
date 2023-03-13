namespace DreamChip.AnimalTracking.Application.Dto.AnimalVisitedLocation;

public sealed class AnimalVisitedLocationDto
{

    public long Id { get; set; }
    
    public DateTime DateTimeOfVisitLocationPoint { get; set; }

    public long LocationId { get; set; }
}
