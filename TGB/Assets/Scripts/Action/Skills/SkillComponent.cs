using SCOM.CoreSystem;
using SCOM.Weapons;
using SCOM.Weapons.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SCOM.Skills.Component
{
    public class SkillComponent : MonoBehaviour
    {
        protected Skill skill;
        protected AnimationEventHandler eventHandler;
        protected Core core => skill.SkillCore;
        protected bool isAttackActive;

        public virtual void Init()
        {

        }
        protected virtual void Awake()
        {
            skill = GetComponent<Skill>();

            eventHandler = GetComponentInChildren<AnimationEventHandler>();
        }

        protected virtual void Start()
        {
            skill.OnEnter += HandleEnter;
            skill.OnExit += HandleExit;
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
            skill.OnEnter -= HandleEnter;
            skill.OnExit -= HandleExit;
        }
    }
    public abstract class SkillComponent<T1, T2> : SkillComponent where T1 : ComponentData<T2> where T2 : ActionData
    {
        protected T1 data;
        protected T2 currentActionData;

        protected override void HandleEnter()
        {
            base.HandleEnter();

            //currentActionData = data.ActionData[skill.CurrentActionCounter];
        }

        public override void Init()
        {
            base.Init();

            data = skill.skillData.GetData<T1>();
        }
    }
}
