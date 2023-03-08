namespace DreamChip.AnimalTracking.Application.Dto.Animal;

public sealed class AnimalDto
{
    public long Id { get; set; }
    
    public List<long> AnimalTypes { get; set; } = null!;
    
    public double Weight { get; set; }
    
    public double Length { get; set; }
    
    public string Gender { get; set; } = null!;
    
    public string LifeStatus { get; set; } = null!;
    
    public DateTime ChippingDateTime { get; set; }
    
    public int ChipperId { get; set; }
    
    public long ChippingLocationId { get; set; }
    
    public List<long> VisitedLocations { get; set; } = null!;
    
    public DateTime? DeathDateTime { get; set; }
}
