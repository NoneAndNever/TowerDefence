using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ECharacterStatus
{
    Dizzy=1,
    MoveSpeedUp=2,
    MoveSpeedDown=4,
    AtkUp=8,
    AtkDown=16,
    AtkSpeedUp=32,
    AtkSpeedDown=64,
    Cured=128,
    Burned=256
    
    //VulnerableMagical=32,
    //VulnerablePhysical=64,

}

public enum ETowerStatus
{
    Dizzy=1,
    RadiusUp=2,
    RadiusDown=4,
    AtkUp=8,
    AtkDown=16,
    AtkSpeedUp=32,
    AtkSpeedDown=64,
}

public enum EDamageType
{
    Physical,
    Magic,
    True
}

