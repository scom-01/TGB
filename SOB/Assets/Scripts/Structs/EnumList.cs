using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region Input
[Serializable]
public enum InputEnum
{
    GamePlay,
    UI,
    Cfg,
}

[Serializable]
public enum UI_State
{
    GamePlay,
    Inventory,
    Reforging,
    Cfg,
    CutScene,
    Result,
    Loading,
}

#endregion

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

public enum GOODS_TPYE
{
    Gold = 0,
    ElementalSculpture = 1,
    FireGoods,
    WaterGoods,
    EarthGoods,
    WindGoods,
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

public enum Stats_TYPE
{
    PhysicsAgg,
    MagicAgg,
    PhysicsDef,
    MagicDef,
    ElementalAgg,
    ElementalDef,
    AttackSpeed,
    MaxHealth,
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
    NormalEnemy = 0,
    EleteEnemy = 1,
    BossEnemy = 2,
}

#endregion

#region CC
public enum CrowdControl
{
    Stun,
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