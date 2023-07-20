using SOB.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Localization.Plugins.XLIFF.V20;
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
            eventHandler.OnShootProjectile += HandleShootProjectile;
        }

        protected override void OnDestory()
        {
            base.OnDestory();
            eventHandler.OnShootProjectile -= HandleShootProjectile;
        }
    }
}
