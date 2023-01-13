using System.Collections;
using System.Collections.Generic;
using SOB.Weapons.Components;
using UnityEngine;

[CreateAssetMenu(fileName = "newWeaponData", menuName = "Data/Weapon Data/Test Data", order = 0)]
public class TestSPO : ScriptableObject
{
    [field: SerializeReference] public List<float> ComponentData { get; private set; }
}
