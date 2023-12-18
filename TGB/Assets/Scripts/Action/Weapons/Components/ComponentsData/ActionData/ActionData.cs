using System;
using System.Collections.Generic;
using UnityEngine;

namespace TGB.Weapons.Components
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

    public static bool operator ==(AnimCommand s1, AnimCommand s2)
    {
        if(s1.animOC != s2.animOC)
        {
            return false;
        }
        if(s1.data != s2.data)
        {
            return false;
        }
        return true;
    }

    public static bool operator !=(AnimCommand s1, AnimCommand s2)
    {
        if(s1.animOC == s2.animOC)
        {
            return false;
        }
        if(s1.data == s2.data)
        {
            return false;
        }
        return true;
    }
}