using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct StatsData
{
    //-------------------------Common Options
    [Header("Stats Options")]
    [Tooltip("최대 체력")]
    public float MaxHealth;
    [Tooltip("움직임 Velocity")]
    public float MovementVelocity;
    [Tooltip("점프 Velocity")]
    public float JumpVelocity;
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
    public DAMAGE_ATT DamageAttiribute;

    //-------------------------Elemental Options
    [Header("Elemental Options")]
    [Tooltip("원소 속성")]
    public E_Power Elemental;
    [Tooltip("원소 저항력 (max = 100)%")]
    public float ElementalDefensivePer;
    [Tooltip("원소 공격력 %")]
    public float ElementalAggressivePer;

    public static StatsData operator +(StatsData s1, StatsData s2)
    {
        StatsData temp = new StatsData();
        temp.MaxHealth = s1.MaxHealth + s2.MaxHealth;
        temp.MovementVelocity = s1.MovementVelocity + s2.MovementVelocity;
        temp.JumpVelocity = s1.JumpVelocity + s2.JumpVelocity;
        temp.DefaultPower = s1.DefaultPower + s2.DefaultPower;
        temp.AttackSpeedPer = s1.AttackSpeedPer + s2.AttackSpeedPer;
        temp.PhysicsDefensivePer = s1.PhysicsDefensivePer + s2.PhysicsDefensivePer;
        temp.MagicDefensivePer = s1.MagicDefensivePer + s2.MagicDefensivePer;
        temp.PhysicsAggressivePer = s1.PhysicsAggressivePer + s2.PhysicsAggressivePer;
        temp.MagicAggressivePer = s1.MagicAggressivePer + s2.MagicAggressivePer;
        temp.ElementalDefensivePer = s1.ElementalDefensivePer + s2.ElementalDefensivePer;
        temp.ElementalAggressivePer = s1.ElementalAggressivePer + s2.ElementalAggressivePer;
        return temp;
    }

    public static StatsData operator *(StatsData s1, float f1)
    {
        StatsData temp = new StatsData();
        temp.MaxHealth = s1.MaxHealth * f1;
        temp.MovementVelocity = s1.MovementVelocity * f1;
        temp.JumpVelocity = s1.JumpVelocity * f1;
        temp.DefaultPower = s1.DefaultPower * f1;
        temp.AttackSpeedPer = s1.AttackSpeedPer * f1;
        temp.PhysicsDefensivePer = s1.PhysicsDefensivePer * f1;
        temp.MagicDefensivePer = s1.MagicDefensivePer * f1;
        temp.PhysicsAggressivePer = s1.PhysicsAggressivePer * f1;
        temp.MagicAggressivePer = s1.MagicAggressivePer * f1;
        temp.ElementalDefensivePer = s1.ElementalDefensivePer * f1;
        temp.ElementalAggressivePer = s1.ElementalAggressivePer * f1;
        return temp;
    }
}
