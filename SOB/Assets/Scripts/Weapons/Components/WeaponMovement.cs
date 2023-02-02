using SOB.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SOB.Weapons.Components;
using System;

namespace SOB.Weapons.Components
{
    public class WeaponMovement : WeaponComponent<MovementData, ActionMovement>
    {
        private CoreSystem.Movement coreMovement;
        private CoreSystem.Movement CoreMovement
        {
            get => coreMovement ? coreMovement : core.GetCoreComponent(ref coreMovement);
        }


        private void HandleStartMovement()
        {            
            CoreMovement.SetVelocity(currentActionData.Velocity, currentActionData.Direction, CoreMovement.FancingDirection);            
        }

        private void HandleStopMovement()
        {
            CoreMovement.SetVelocityX(0f);
        }

        protected override void Start()
        {
            base.Start();

            eventHandler.OnStartMovement += HandleStartMovement;
            eventHandler.OnStopMovement += HandleStopMovement;
        }

        protected override void OnDestory()
        {
            base.OnDestory();

            eventHandler.OnStartMovement -= HandleStartMovement;
            eventHandler.OnStopMovement -= HandleStopMovement;
        }
    }
}
