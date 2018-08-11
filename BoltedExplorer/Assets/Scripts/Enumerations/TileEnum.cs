using System.ComponentModel;

public enum TileType
{
    [Description("Unassigned")]
    Unassigned = 0,
    
    [Description("Spawn Point")]
    Spawn = 1,

    [Description("End Goal")]
    Goal = 2,

    [Description("Solid Collider")]
    Solid = 3,

    [Description("Water")]
    Water = 4,

    [Description("Spikes")]
    Spikes = 5
}
