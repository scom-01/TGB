using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TGB.Weapons.Components
{
    public class WeaponBuffData : ComponentData<ActionWeaponBuff>
    {
        public WeaponBuffData()
        {
            ComponentDependency = typeof(WeaponBuff);
        }
    }
}
