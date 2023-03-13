namespace DreamChip.AnimalTracking.Application.Dto.AnimalVisitedLocation;

public sealed class AnimalVisitedLocationPageRequestDto
{
    public DateTime? StartDateTime { get; set; }

    public DateTime? EndDateTime { get; set; }

    public int From { get; set; } = 0;

    public int Size { get; set; } = 10;
}
