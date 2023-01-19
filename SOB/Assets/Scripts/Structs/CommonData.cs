using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct CommonData
{
    [Tooltip("최대 체력")]
    public float maxHealth;
    [Tooltip("움직임 Velocity")]
    public float movementVelocity;
    [Tooltip("기본 공격력")]
    public float DefaulPower;
    [Tooltip("기본 물리방어력")]
    public float PhysicsDefensivePer;
    [Tooltip("기본 물리방어력")]    
    public float MagicDefensivePer;

    [Tooltip("추가 물리공격력 %")]
    public float PhysicsAggressivePer;
    [Tooltip("추가 마법공격력 %")]
    public float MagicAggressivePer;
    [Tooltip("원소 속성")]
    public ElementalPower MyElemental;
    [Tooltip("원소 저항력 %")]
    public float ElementalDefensivePer;
    [Tooltip("원소 공격력 %")]
    public float ElementalAggressivePer;
    [Tooltip("공격 속성")]
    public DamageAttiribute DamageAttiribute;
}
