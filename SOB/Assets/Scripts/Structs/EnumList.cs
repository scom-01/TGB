using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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