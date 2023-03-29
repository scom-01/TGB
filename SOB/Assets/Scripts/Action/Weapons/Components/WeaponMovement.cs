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
        protected override void HandleEnter()
        {
            base.HandleEnter();
        }
        private void HandleStartMovement()
        {
            CoreMovement.CanMovement = true;
        }

        //중력의 영향 x 
        private void HandleFixedStartMovement()
        {
            if (currentActionData != null)
            {
                CheckMovementAction(currentActionData);
            }
            //if (currentActionData != null && currentAirActionData != null)
            //{
            //    if (weapon.InAir)
            //    {
            //        CheckMovementAction(currentAirActionData);
            //    }
            //    else
            //    {
            //        CheckMovementAction(currentActionData);
            //    }
            //}
            //else if (currentActionData == null)
            //{
            //    CheckMovementAction(currentAirActionData);
            //}
            //else if (currentAirActionData == null)
            //{
            //    CheckMovementAction(currentActionData);
            //}
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

        public void CheckMovementAction(ActionMovement actionData)
        {
            if (actionData == null)
                return;
            var currMovement = actionData.movements;

            CoreMovement.SetVelocity(currMovement.Velocity, currMovement.Direction, CoreMovement.FancingDirection);
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
