using SOB.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOB.Weapons.Components
{
    public class WeaponProjectile : WeaponComponent<WeaponProjectileData, WeaponProjectileActionData>
    {
        public int currentProjectileIndex = 0;
        protected override void HandleEnter()
        {
            base.HandleEnter();
            currentProjectileIndex = 0;
        }

        private void HandleShootProjectile()
        {
            Debug.LogWarning($"{core.Unit.name}");

            if (currentActionData != null)
            {
                CheckAttackAction(currentActionData);
            }
            currentProjectileIndex++;
        }
        private void CheckAttackAction(WeaponProjectileActionData actionData)
        {
            CoreEffectManager.StartProjectileCheck(core.Unit, actionData.ProjectileActionData[currentProjectileIndex]);
            //obj.Shoot();
        }

        protected override void Start()
        {
            base.Start();            
            eventHandler.OnShootProjectile -= HandleShootProjectile;
            eventHandler.OnShootProjectile += HandleShootProjectile;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            eventHandler.OnShootProjectile -= HandleShootProjectile;
        }
    }
}
