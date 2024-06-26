using System;

#region Input
[Serializable]
public enum InputEnum
{
    GamePlay,
    UI,
    Cfg,
    CutScene,
}

[Serializable]
public enum UI_State
{
    GamePlay = 0,
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
    Active = 0,
    Passive = 1,
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

/// <summary>
/// 아이템의 보유 효과가 적용될 상태 
/// </summary>
public enum ITEM_TPYE
{
    None = 0,
    OnAction = 1,
    OnHit = 2,
    OnUpdate = 3,
    OnInit = 4,
    OnDash = 5,
    OnMoveMap = 6,
    OnDamaged = 7,
    OnCritical = 8,
    OnJump = 9,
    OnLand = 10,
    OnDodge = 11,
    OnHealing = 12,
    OnKilled = 13,
    OnSkill = 14,
}

public enum ITEM_Level
{
    Common = 1,
    Elete = 2,
    Special = 3,
    Legendary = 4,
}

public enum GOODS_TPYE
{
    Gold = 0,
    FireGoods,
    WaterGoods,
    EarthGoods,
    WindGoods,
    HammerShards,
    None,
}

public enum Goods_UI_Type
{
    Curr = 0,
    Cumulative = 1,
    Usage = 2,
}

#endregion

#region Stats
public enum DAMAGE_ATT
{
    Physics = 0,
    Magic = 1,
    Fixed = 2,
    Heal = 3,
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
    CriticalPer,
    CriticalDmg,
    MaxHealth,
    MoveSpeed,
    JumpVelocity,
    Health,
}

public enum Blessing_Stats_TYPE
{
    //Physics, Magic
    Bless_Agg,
    //Physics, Magic
    Bless_Def,
    //AttackSpeed, Move
    Bless_Speed,
    //CriticalPer, Dmg
    Bless_Critical,
    //Agg, Def
    Bless_Elemental,
}

#endregion

#region Enemy

public enum ENEMY_Form
{
    Grounded = 0,
    Fly = 1,
}

public enum Unit_Size
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

public enum ENEMY_DetectedType
{
    Box = 0,
    Circle = 1,
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

public enum ObjectPooling_TYPE
{
    Effect,
    Projectile,
    DmgText,
}