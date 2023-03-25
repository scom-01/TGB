using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SOB.CoreSystem;

namespace SOB.Weapons.Components
{
    public abstract class WeaponComponent : MonoBehaviour
    {
        protected Weapon weapon;
        protected AnimationEventHandler eventHandler;
        protected Core core => weapon.WeaponCore ?? weapon.GetComponentInParent<Unit>().Core;
        protected bool isAttackActive;

        private CoreSystem.Movement coreMovement;
        protected CoreSystem.Movement CoreMovement
        {
            get => coreMovement ? coreMovement : core.GetCoreComponent(ref coreMovement);
        }
        private CoreSystem.CollisionSenses coreCollisionSenses;
        protected CoreSystem.CollisionSenses CoreCollisionSenses
        {
            get => coreCollisionSenses ? coreCollisionSenses : core.GetCoreComponent(ref coreCollisionSenses);
        }
        private CoreSystem.ParticleManager coreParticleManager;
        protected CoreSystem.ParticleManager CoreParticleManager
        {
            get => coreParticleManager ? coreParticleManager : core.GetCoreComponent(ref coreParticleManager);
        }

        private CoreSystem.UnitStats coreUnitStats;
        protected CoreSystem.UnitStats CoreUnitStats
        {
            get => coreUnitStats ? coreUnitStats : core.GetCoreComponent(ref coreUnitStats);
        }

        public virtual void Init()
        {

        }
        protected virtual void Awake()
        {
            weapon = GetComponent<Weapon>();

            eventHandler = GetComponentInChildren<AnimationEventHandler>();
        }

        protected virtual void Start()
        {
            weapon.OnEnter += HandleEnter;
            weapon.OnExit += HandleExit;
        }

        protected virtual void HandleEnter()
        {
            isAttackActive = true;
        }

        protected virtual void HandleExit()
        {
            isAttackActive = false;
        }

        protected virtual void OnDestory()
        {
            weapon.OnEnter -= HandleEnter;
            weapon.OnExit -= HandleExit;
        }


    }
    public abstract class WeaponComponent<T1, T2> : WeaponComponent where T1 : ComponentData<T2> where T2 : ActionData
    {
        protected T1 data;
        protected T2 currentGroundedActionData;
        protected T2 currentAirActionData;

        protected override void HandleEnter()
        {
            base.HandleEnter();
            currentGroundedActionData = data.ActionData[weapon.CurrentActionCounter] ?? null;
            Debug.LogWarning($"{data.ActionData}[{weapon.CurrentActionCounter}] = {currentGroundedActionData}");
            currentAirActionData = data.InAirActionData[weapon.CurrentActionCounter] ?? null;
            Debug.LogWarning($"{data.InAirActionData}[{weapon.CurrentActionCounter}] = {currentAirActionData}");

        }

        public override void Init()
        {
            base.Init();

            data = weapon.weaponData.GetData<T1>();
        }
    }
}