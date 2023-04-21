using System.Collections;
using System.Collections.Generic;
using UnityEngine;


#region Item
public enum EVENT_BUFF_TYPE
{
    E_Buff = 0,
    E_DeBuff = 1,
}

public enum EVENT_BUFF_STATS
{
    MaxHealth,
    currentHealth,
    MovementVelocity,
    DefaulPower,
    AttackSpeedPer,
    PhysicsDefensivePer,
    MagicDefensivePer,
    PhysicsAggressivePer,
    MagicAggressivePer,
    ElementalDefensivePer,
    ElementalAggressivePer,
}


public enum ITEM_TPYE
{
    Detected = 0,
    Collision = 1,
}

#endregion

#region Stats
public enum DAMAGE_ATT
{
    Physics = 0,
    Magic = 1,
    Fixed = 2,
}

/// <summary>
/// Water > Earth > Wind > Fire > Water
/// </summary>
public enum E_Power
{
    Normal = 0,
    Fire = 1,
    Wind = 2,
    Earth = 3,
    Water = 4,
}

#endregion

#region Enemy

public enum ENEMY_Form
{
    Grounded = 0,
    Fly = 1,
}

public enum ENEMY_Size
{
    Small = 0,
    Medium = 1,
    Big = 2,
}

public enum ENEMY_Level
{
    Normal = 0,
    Elete = 1,
    Boss = 2,
}

#endregion

#region Sound
public enum Sound
{
    BGM,
    Effect,
    MaxCount,
}
#endregion

#region Cfg Setting
public enum Resolution_Level
{
    _1080x720,
    _1920x1080,
    Count,
}
#endregion

#region UI
public enum Localization
{
    en,
    kr,
}
#endregion