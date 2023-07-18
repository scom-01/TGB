using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOB.Weapons.Components
{
    public class WeaponProjectile : WeaponComponent<WeaponProjectileData, WeaponProjectileActionData>
    {
        protected override void Start()
        {
            base.Start();
        }

        protected override void OnDestory()
        {
            base.OnDestory();
        }
    }
}
