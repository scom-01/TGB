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
        private int currentMovementCommandIndex = 0;
        protected override void HandleEnter()
        {
            base.HandleEnter();
            currentMovementCommandIndex = 0;
        }
        private void HandleStartMovement()
        {
            CoreMovement.CanMovement = true;
        }

        //중력의 영향 x 
        private void HandleFixedStartMovement()
        {
            if (currentGroundedActionData != null && currentAirActionData != null)
            {
                if (weapon.InAir)
                {
                    CheckMovementAction(currentAirActionData);
                }
                else
                {
                    CheckMovementAction(currentGroundedActionData);
                }
            }
            else if (currentGroundedActionData == null)
            {
                CheckMovementAction(currentAirActionData);
            }
            else if (currentAirActionData == null)
            {
                CheckMovementAction(currentGroundedActionData);
            }
            currentMovementCommandIndex++;
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
            var currMovement = actionData.movementCommands;
            if (currMovement.Length <= 0)
                return;

            for (int i = 0; i < currMovement.Length; i++)
            {
                if (currMovement[i].Command == weapon.Command)
                {
                    currentMovementCommandIndex = i;
                    break;
                }
                currentMovementCommandIndex = -1;

                if (currentMovementCommandIndex == -1)
                {
                    weapon.EventHandler.AnimationFinishedTrigger();
                    return;
                }
            }

            CoreMovement.SetVelocity(currMovement[currentMovementCommandIndex].Velocity, currMovement[currentMovementCommandIndex].Direction, CoreMovement.FancingDirection);
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
