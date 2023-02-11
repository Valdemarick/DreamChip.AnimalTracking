using System.ComponentModel;

namespace DreamChip.AnimalTracking.Domain.Enums;

/// <summary>
/// Animal life status.
/// </summary>
public enum LifeStatus
{
    [Description("Alive")]
    Alive = 0,

    [Description("Dead")]
    Dead = 1,
}
