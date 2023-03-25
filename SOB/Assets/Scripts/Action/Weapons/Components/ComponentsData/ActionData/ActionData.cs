using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SOB.Weapons.Components
{
    public class ActionData
    {
        [SerializeField, HideInInspector] private string name;

        public void SetAttackName(string actionName, int i) => name = $"{actionName} Action {i}";
    }

    public enum CommandEnum
    {
        Primary,
        Secondary,
    }
}
