using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageAttiribute
{
    Physics = 0,
    Magic = 1,
    Fixed = 2,
}

/// <summary>
/// Water > Earth > Wind > Fire > Water
/// </summary>
public enum ElementalPower
{
    Normal = 0,
    Fire = 1,
    Wind = 2,
    Earth = 3,
    Water = 4,
}
[CreateAssetMenu(fileName = "newWeaponData", menuName = "Data/Weapon Data/Base Data")]
public class WeaponData : ScriptableObject
{
    [Header("Damage")]
    [Tooltip("Attack Damage")]
    public int AttackDamage = 10;

    [Header("Attack Speed")]
    [Tooltip("초당 공격 속도")]
    public float AttackSpeed = 0.5f;

    [Header("Attack Attributes")]
    [Tooltip("속성 (마법, 물리)")]
    public DamageAttiribute AttackAttributes;

    [Header("Elemental Power")]
    [Tooltip("원소 속성")]
    public ElementalPower Elemental;
}
