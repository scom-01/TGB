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
        protected Core core => weapon.WeaponCore;
        protected bool isAttackActive;

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
        protected T2 currentActionData;

        protected override void HandleEnter()
        {
            base.HandleEnter();

            currentActionData = data.ActionData[weapon.CurrentActionCounter];
        }

        public override void Init()
        {
            base.Init();

            data = weapon.weaponData.GetData<T1>();
        }
    }
}