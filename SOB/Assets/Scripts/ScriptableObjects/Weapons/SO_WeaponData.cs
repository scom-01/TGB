using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using SOB.Weapons.Components;
[CreateAssetMenu(fileName ="newWeaponData",menuName ="Data/Weapon Data/Basic Weapon Data",order = 0)]
public class SO_WeaponData : ScriptableObject
{
    [field: SerializeField]
    [Tooltip("���� ��� ��")]
    public int amountOfAttacks;

    [Tooltip("�޺� ���� �ʱ�ȭ �ð�")]
    public float actionCounterResetCooldown;
    [Tooltip("���� �� VelocityX movement ��")]
    public float[] movementSpeed { get; protected set; }

    [Header("Skill polymorphism")]
    [Tooltip("���� �� ����")]
    public bool CanJump;
    [Tooltip("���� ����")]
    public bool CanAirAttack;

    /*[field: SerializeReference] public List<ComponentData> ComponentData { get; private set; }

    public T GetData<T>()
    {
        return ComponentData.OfType<T>().FirstOrDefault();
    }

    public void AddData(ComponentData data)
    {
        if (ComponentData.FirstOrDefault(t => t.GetType() == data.GetType()) != null)
        {
            return;
        }

        ComponentData.Add(data);
    }*/
}
