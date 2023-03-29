using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newWeaponCommandData", menuName = "Data/Weapon Data/WeaponCommand Data")]
public class WeaponCommandDataSO : ScriptableObject
{
    [field: SerializeField] public int NumberOfActions { get; private set; }
    [field: SerializeField] public AnimatorOverrideController DefaultAnimator;

    [field: SerializeField] public List<CommandList> GroundedCommandList;
    [field: SerializeField] public List<CommandList> AirCommandList;

    [field: SerializeField] public WeaponDataSO data;
}

[Serializable]
public struct CommandWeapon
{
    public CommandList CommandList;
}
