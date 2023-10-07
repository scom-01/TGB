using TGB.Weapons.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newWeaponData", menuName = "Data/Weapon Data/Test Data")]
public class TestDataSO : ScriptableObject
{
    [field: SerializeField] public int NumberOfActions { get; private set; }

    [field: SerializeField] public bool CanJump { get; private set; }
    [field: SerializeField] public bool CanAirAttack { get; private set; }
    [field: SerializeReference] public List<ComponentData> ComponentData { get; private set; }
}
