using SOB.Weapons.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SOB.Weapons.Components
{
    public class ActionData
    {
        [SerializeField, HideInInspector] private string name;

        public void SetAttackName(int i) => name = $"Action {i}";
    }
}
[Serializable]
public enum CommandEnum
{
    Primary,
    Secondary,
}
[Serializable]
public struct CommandList
{
    public List<AnimCommand> commands;    
}

[Serializable]
public struct AnimCommand
{
    public CommandEnum command;
    public AnimatorOverrideController animOC;
    public WeaponDataSO data;
}