using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageAttiribute
{
    Physics = 0,
    Magic = 1,
}
public enum ElementalPower
{
    Normal = 0,
    Ice = 1,
    Fire = 2,
    Earth = 3,
    Steel = 4,
}
[CreateAssetMenu(fileName = "newWeaponData", menuName = "Data/Weapon Data/Base Data")]
public class WeaponData : ScriptableObject
{
    [Header("Damage")]
    [Tooltip("Attack Damage")]
    public int AttackDamage = 10;

    [Header("Attack Speed")]
    [Tooltip("檬寸 傍拜 加档")]
    public float AttackSpeed = 0.5f;

    [Header("Attack Attributes")]
    [Tooltip("加己 (付过, 拱府)")]
    public DamageAttiribute AttackAttributes;

    [Header("Elemental Power")]
    [Tooltip("盔家 加己")]
    public ElementalPower Elemental;
}
