using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public enum TriggerType
{
    [Description("Unassigned")]
    Unassigned = 0,

    [Description("Dialogue")]
    Dialogue = 1,

    [Description("Raider")]
    Raider = 2,

    [Description("Zombie")]
    Zombie = 3,

    [Description("Wraith")]
    Wraith = 4,

    [Description("Trap")]
    Trap = 5
}
