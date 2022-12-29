using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="newWeaponData",menuName ="Data/Weapon Data/Weapon")]
public class SO_WeaponData : ScriptableObject
{
    [Tooltip("공격 모션 수")]
    public int amountOfAttacks;
    [Tooltip("콤보 공격 초기화 시간")]
    public float comboResetTime;
    [Tooltip("공격 시 VelocityX movement 값")]
    public float[] movementSpeed { get; protected set; }
}
