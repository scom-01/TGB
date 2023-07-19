using SOB.Weapons.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOB.Weapons.Components
{
    public class WeaponProjectileData : ComponentData<WeaponProjectileActionData>
    {

        public WeaponProjectileData()
        {
            ComponentDependency = typeof(WeaponProjectile);
        }
    }
}
