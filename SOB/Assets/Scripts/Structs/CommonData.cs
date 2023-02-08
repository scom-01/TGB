using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct CommonData
{
    //-------------------------Common Options
    [Header("Common Options")]
    [Tooltip("최대 체력")]
    public float MaxHealth;
    [Tooltip("움직임 Velocity")]
    public float MovementVelocity;
    [Tooltip("기본 공격력")]
    public float DefaultPower;
    [Tooltip("추가 공격 속도 %")]
    public float AttackSpeedPer;
    [Tooltip("물리 방어력 % (max = 100)")]    
    public float PhysicsDefensivePer;
    [Tooltip("마법 방어력 % (max = 100)")]    
    public float MagicDefensivePer;
    [Tooltip("추가 물리공격력 %")]
    public float PhysicsAggressivePer;
    [Tooltip("추가 마법공격력 %")]
    public float MagicAggressivePer;
    [Tooltip("공격 속성")]
    public DamageAttiribute DamageAttiribute;

    //-------------------------Elemental Options
    [Header("Elemental Options")]
    [Tooltip("원소 속성")]
    public ElementalPower MyElemental;
    [Tooltip("원소 저항력 (max = 100)%")]
    public float ElementalDefensivePer;
    [Tooltip("원소 공격력 %")]
    public float ElementalAggressivePer;

    public static CommonData operator +(CommonData c1, CommonData c2)
    {
        CommonData temp =new CommonData();
        temp.MaxHealth = c1.MaxHealth + c2.MaxHealth;
        temp.MovementVelocity = c1.MovementVelocity + c2.MovementVelocity;
        temp.DefaultPower = c1.DefaultPower + c2.DefaultPower;
        temp.AttackSpeedPer = c1.AttackSpeedPer + c2.AttackSpeedPer;
        temp.PhysicsDefensivePer = c1.PhysicsDefensivePer + c2.PhysicsDefensivePer;
        temp.MagicDefensivePer = c1.MagicDefensivePer + c2.MagicDefensivePer;
        temp.PhysicsAggressivePer = c1.PhysicsAggressivePer + c2.PhysicsAggressivePer;
        temp.MagicAggressivePer = c1.MagicAggressivePer + c2.MagicAggressivePer;
        temp.ElementalDefensivePer = c1.ElementalDefensivePer + c2.ElementalDefensivePer;
        temp.ElementalAggressivePer = c1.ElementalAggressivePer + c2.ElementalAggressivePer;
        return temp;
    }
    public static CommonData operator -(CommonData c1, CommonData c2)
    {
        CommonData temp =new CommonData();
        temp.MaxHealth = c1.MaxHealth - c2.MaxHealth;
        temp.MovementVelocity = c1.MovementVelocity - c2.MovementVelocity;
        temp.DefaultPower = c1.DefaultPower - c2.DefaultPower;
        temp.AttackSpeedPer = c1.AttackSpeedPer - c2.AttackSpeedPer;
        temp.PhysicsDefensivePer = c1.PhysicsDefensivePer - c2.PhysicsDefensivePer;
        temp.MagicDefensivePer = c1.MagicDefensivePer - c2.MagicDefensivePer;
        temp.PhysicsAggressivePer = c1.PhysicsAggressivePer - c2.PhysicsAggressivePer;
        temp.MagicAggressivePer = c1.MagicAggressivePer - c2.MagicAggressivePer;
        temp.ElementalDefensivePer = c1.ElementalDefensivePer - c2.ElementalDefensivePer;
        temp.ElementalAggressivePer = c1.ElementalAggressivePer - c2.ElementalAggressivePer;
        return temp;
    }
}
