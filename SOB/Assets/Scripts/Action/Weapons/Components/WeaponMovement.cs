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
            //CoreMovement.SetVelocity(currentActionData.Velocity, currentActionData.Direction, CoreMovement.FancingDirection);            
            CoreMovement.CanMovement = true;
        }

        //중력의 영향 x 
        private void HandleFixedStartMovement()
        {
            CoreMovement.SetVelocity(currentActionData.Velocity, currentActionData.Direction, CoreMovement.FancingDirection);
            core.Unit.RB.gravityScale = 0f;            
        }
        private void HandleFixedStopMovement()
        {
            CoreMovement.SetVelocityX(0f);
            core.Unit.RB.gravityScale = 5f;            
        }

        private void HandleStopMovement()
        {
            CoreMovement.CanMovement = false;
            CoreMovement.SetVelocityX(0f);
        }
        private void HandleStartFlip()
        {
            CoreMovement.CanFlip = true;
        }
        private void HandleStopFlip()
        {
            CoreMovement.CanFlip = false;
        }


        protected override void Start()
        {
            base.Start();

            eventHandler.OnFixedStartMovement += HandleFixedStartMovement;
            eventHandler.OnFixedStopMovement += HandleFixedStopMovement;
            eventHandler.OnStartMovement += HandleStartMovement;
            eventHandler.OnStopMovement += HandleStopMovement;
            eventHandler.OnStartFlip += HandleStartFlip;
            eventHandler.OnStopFlip += HandleStopFlip;
        }

        protected override void OnDestory()
        {
            base.OnDestory();

            eventHandler.OnFixedStartMovement -= HandleFixedStartMovement;
            eventHandler.OnFixedStopMovement -= HandleFixedStopMovement;
            eventHandler.OnStartMovement -= HandleStartMovement;
            eventHandler.OnStopMovement -= HandleStopMovement;
            eventHandler.OnStartFlip -= HandleStartFlip;
            eventHandler.OnStopFlip -= HandleStopFlip;
        }
    }
}
