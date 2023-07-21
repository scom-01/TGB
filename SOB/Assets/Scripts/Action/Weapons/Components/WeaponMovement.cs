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
        private int currentMovementIndex = -1;
        protected override void HandleEnter()
        {
            base.HandleEnter();
            currentMovementIndex = 0;
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
            CoreMovement.CanMovement = currentActionData.movements[currentMovementIndex].CanMoveCtrl;
            if (currentActionData != null)
            {
                CheckMovementAction(currentActionData);
            }
            core.Unit.RB.gravityScale = 0f;
            currentMovementIndex++;
        }
        private void HandleFixedStopMovement()
        {
            Debug.Log("HandleFixedStop");
            CoreMovement.CanMovement = false;
            CoreMovement.SetVelocityX(0f);
            core.Unit.RB.gravityScale = core.Unit.UnitData.UnitGravity;
        }
        #endregion

        #region Teleport
        private void HandleTeleport()
        {
            if (core.Unit.TargetUnit == null)
            {
                Debug.LogWarning("Target not found");
                return;
            }
            CoreMovement.SetVelocityZero();
            core.Unit.transform.position = core.Unit.TargetUnit.transform.position;
        }
        #endregion

        #region

        private void HandleStartInvincible()
        {
            CoreDamageReceiver.isHit = true;
            core.Unit.isCCimmunity = true;
        }

        private void HandleStopInvincible()
        {
            CoreDamageReceiver.isHit = false;
            core.Unit.isCCimmunity = false;
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
            if(currMovement.Length < currentMovementIndex)
            {
                return;
            }
            CoreMovement.SetVelocity(currMovement[currentMovementIndex].Velocity, currMovement[currentMovementIndex].Direction, CoreMovement.FancingDirection);
        }

        protected override void Start()
        {
            base.Start();

            eventHandler.OnFixedStartMovement -= HandleFixedStartMovement;
            eventHandler.OnFixedStartMovement += HandleFixedStartMovement;
            
            eventHandler.OnFixedStopMovement -= HandleFixedStopMovement;
            eventHandler.OnFixedStopMovement += HandleFixedStopMovement;
            
            eventHandler.OnTeleportToTarget -= HandleTeleport;
            eventHandler.OnTeleportToTarget += HandleTeleport;
            
            eventHandler.OnStartMovement -= HandleStartMovement;
            eventHandler.OnStartMovement += HandleStartMovement;

            eventHandler.OnStopMovement -= HandleStopMovement;
            eventHandler.OnStopMovement += HandleStopMovement;

            eventHandler.OnStartInvincible -= HandleStartInvincible;
            eventHandler.OnStartInvincible += HandleStartInvincible;

            eventHandler.OnStartFlip -= HandleStartFlip;
            eventHandler.OnStartFlip += HandleStartFlip;

            eventHandler.OnStopFlip -= HandleStopFlip;
            eventHandler.OnStopFlip += HandleStopFlip;

            eventHandler.OnStopInvincible -= HandleStopInvincible;
            eventHandler.OnStopInvincible += HandleStopInvincible;

            //애니메이션 종료 시 원래 상태로 돌리기 위함
            eventHandler.OnFinish += HandleStopInvincible;
            eventHandler.OnFinish += HandleFixedStopMovement;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            eventHandler.OnFixedStartMovement -= HandleFixedStartMovement;
            eventHandler.OnFixedStopMovement -= HandleFixedStopMovement;
            eventHandler.OnTeleportToTarget -= HandleTeleport;
            eventHandler.OnStartMovement -= HandleStartMovement;
            eventHandler.OnStopMovement -= HandleStopMovement;
            eventHandler.OnStartInvincible -= HandleStartInvincible;
            eventHandler.OnStartFlip -= HandleStartFlip;
            eventHandler.OnStopFlip -= HandleStopFlip;
            eventHandler.OnStopInvincible -= HandleStopInvincible;

            //애니메이션 종료 시 원래 상태로 돌리기 위함
            eventHandler.OnFinish -= HandleStopInvincible;
            eventHandler.OnFinish -= HandleFixedStopMovement;
        }
    }
}
