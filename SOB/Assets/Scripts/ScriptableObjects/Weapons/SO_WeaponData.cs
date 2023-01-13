using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="newWeaponData",menuName ="Data/Weapon Data/Weapon")]
public class SO_WeaponData : ScriptableObject
{
    [Tooltip("���� ��� ��")]
    public int amountOfAttacks;
    [Tooltip("�޺� ���� �ʱ�ȭ �ð�")]
    public float attackCounterResetCooldown;
    [Tooltip("���� �� VelocityX movement ��")]
    public float[] movementSpeed { get; protected set; }


    [Header("Skill polymorphism")]
    [Tooltip("���� �� ����")]
    public bool CanJump;
    [Tooltip("���� ����")]
    public bool CanAirAttack;
}
