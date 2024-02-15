using Microsoft.Cci;
using TGB.CoreSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TGB.Skills
{
    public class Skill : MonoBehaviour
    {
        public SkillDataSO skillData { get; private set; }

        [HideInInspector]   protected string weaponAnimBoolStr;

        public event Action OnEnter;
        public event Action OnExit;
        public Core SkillCore { get; private set; }

        protected Animator baseAnimator;
        public GameObject BaseGameObject { get; private set; }
        protected Animator WeaponAnimator;
        public GameObject SkillSpriteGameObject { get; private set; }

        public AnimationEventHandler EventHandler { get; private set; }


        protected PlayerSkillState state;

        private void Awake()
        {
            BaseGameObject = transform.Find("Base").gameObject;
            SkillSpriteGameObject = transform.Find("Skill").gameObject;

            baseAnimator = transform.Find("Skill").GetComponent<Animator>();

            EventHandler = BaseGameObject.GetComponent<AnimationEventHandler>();
        }
        private void Update()
        {
            //BaseGameObject.GetComponent<Animator>().speed = 1f + (SkillCore.GetCoreComponent<UnitStats>().AttackSpeedPer * 1 / 100);            
        }

        private void OnEnable()
        {
            //TODO: event Action 사용 시 Finish처리가 animation 끝부분에 있기에 Cancle처리가 안됨
            EventHandler.OnFinish += ExitWeapon;
            
        }

        private void OnDisable()
        {
            EventHandler.OnFinish -= ExitWeapon;            
        }

        public virtual void EnterWeapon()
        {
            print($"{transform.name} enter");

            SetBoolName("action", true);
            Debug.Log("SetBoolName = true");

            OnEnter?.Invoke();
        }

        /// <summary>
        /// PlayerWeaponState Exit 에서 호출되는 함수
        /// </summary>
        public virtual void ExitWeapon()
        {
            SetBoolName("action", false);
            Debug.Log("SetBoolName = false");

            OnExit?.Invoke();
        }

        public void SetCore(Core core)
        {
            if (core == null)
            {
                Debug.Log($"{transform.name} is core Null");
                return;
            }
            SkillCore = core;
        }
        public void SetData(SkillDataSO data)
        {
            this.skillData = data;
        }

        public virtual void SetBoolName(string name, bool setbool)
        {
            weaponAnimBoolStr = name;
            baseAnimator.SetBool(name, setbool);
        }
        public virtual void SetIntName(string name, int setint)
        {
            weaponAnimBoolStr = name;
            baseAnimator.SetInteger(name, setint);
        }

        public void InitializeSkill(PlayerSkillState state, Core core)
        {
            this.state = state;
            this.SkillCore = core;
        }
    }
}
