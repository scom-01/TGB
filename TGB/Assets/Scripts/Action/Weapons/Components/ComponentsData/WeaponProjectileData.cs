using TGB.Weapons.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TGB.Weapons.Components
{
    public class WeaponProjectileData : ComponentData<WeaponProjectileActionData>
    {

        public WeaponProjectileData()
        {
            ComponentDependency = typeof(WeaponProjectile);
        }
    }
}
