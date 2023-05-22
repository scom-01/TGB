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


        #region 공격시 방향키 이동이 가능한 Movement
        //만약 방향키를 누르지 않으면 
        private void HandleStartMovement()
        {
            CoreMovement.CanMovement = true;
        }
        private void HandleStopMovement()
        {
            CoreMovement.CanMovement = false;
            CoreMovement.SetVelocityX(0f);
        }
        #endregion

        #region 고정 Movement
        private void HandleFixedStartMovement()
        {
            Debug.Log("HandleFixedStart");
            if (currentActionData != null)
            {
                CheckMovementAction(currentActionData);
            }
            core.Unit.RB.gravityScale = 0f;            
        }
        private void HandleFixedStopMovement()
        {
            Debug.Log("HandleFixedStop");
            CoreMovement.SetVelocityX(0f);
            core.Unit.RB.gravityScale = core.Unit.UnitData.UnitGravity;
        }
        #endregion

        #region Handle Flip
        private void HandleStartFlip()
        {
            CoreMovement.CanFlip = true;
        }
        private void HandleStopFlip()
        {
            CoreMovement.CanFlip = false;
        }
        #endregion

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
