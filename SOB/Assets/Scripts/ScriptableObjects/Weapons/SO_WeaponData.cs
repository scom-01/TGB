using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="newWeaponData",menuName ="Data/Weapon Data/Weapon")]
public class SO_WeaponData : ScriptableObject
{
    [Tooltip("���� ��� ��")]
    public int amountOfAttacks;
    [Tooltip("�޺� ���� �ʱ�ȭ �ð�")]
    public float comboResetTime;
    [Tooltip("���� �� VelocityX movement ��")]
    public float[] movementSpeed { get; protected set; }
}
