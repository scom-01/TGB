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

        //강제로 움직임-> 나중에 스킬로사용 가능
        //private void HandleStartMovement()
        //{            
        //    CoreMovement.SetVelocity(currentActionData.Velocity, currentActionData.Direction, CoreMovement.FancingDirection);            
        //}

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

            eventHandler.OnStartMovement += HandleStartMovement;
            eventHandler.OnStopMovement += HandleStopMovement;
            eventHandler.OnStartFlip += HandleStartFlip;
            eventHandler.OnStopFlip += HandleStopFlip;
        }

        protected override void OnDestory()
        {
            base.OnDestory();

            eventHandler.OnStartMovement -= HandleStartMovement;
            eventHandler.OnStopMovement -= HandleStopMovement;
            eventHandler.OnStartFlip -= HandleStartFlip;
            eventHandler.OnStopFlip -= HandleStopFlip;
        }
    }
}
