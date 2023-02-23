using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SOB.CoreSystem;
using SOB.Utilities;

namespace SOB.Weapons
{
    public class Weapon : MonoBehaviour
    {
        //Component
        public WeaponDataSO weaponData { get; private set; }

        public Core WeaponCore { get; private set; }
        public GameObject BaseGameObject { get; private set; }
        public GameObject WeaponSpriteGameObject { get; private set; }
        public AnimationEventHandler EventHandler { get; private set; }

        protected PlayerWeaponState state;
        
        private Timer actionCounterResetTimer;

        //Action
        public event Action OnEnter;
        public event Action OnExit;

        //Value
        [Tooltip("공격 횟수")]  public int CurrentActionCounter
        {
            get => currentActionCounter;
            private set => currentActionCounter = value >= weaponData.NumberOfActions ? 0 : value;
        }
        [HideInInspector] public bool InAir;
        [HideInInspector]   protected string weaponAnimBoolStr;
        
        protected Animator baseAnimator;
        
        [SerializeField]    private float actionCounterResetCooldown;

        private int currentActionCounter;
        

        protected virtual void Awake()
        {
            BaseGameObject = transform.Find("Base").gameObject;
            WeaponSpriteGameObject = transform.Find("Weapon").gameObject;

            baseAnimator = BaseGameObject.GetComponent<Animator>();

            EventHandler = BaseGameObject.GetComponent<AnimationEventHandler>();
            
            actionCounterResetTimer = new Timer(actionCounterResetCooldown);
        }

        private void Update()
        {
            BaseGameObject.GetComponent<Animator>().speed = 1f + (WeaponCore.GetCoreComponent<UnitStats>().StatsData.AttackSpeedPer * 1 / 100);
            actionCounterResetTimer.Tick();
        }

        private void OnEnable()
        {
            //TODO: event Action 사용 시 Finish처리가 animation 끝부분에 있기에 Cancle처리가 안됨
            EventHandler.OnFinish += ExitWeapon;            
            actionCounterResetTimer.OnTimerDone += ResetActionCounter;
        }

        private void OnDisable()
        {
            EventHandler.OnFinish -= ExitWeapon;
            actionCounterResetTimer.OnTimerDone -= ResetActionCounter;
        }

        /// <summary>
        /// PlayerWeaponState Enter 에서 호출되는 함수
        /// </summary>
        public virtual void EnterWeapon()
        {
            print($"{transform.name} enter");

            actionCounterResetTimer.StopTimer();
                        
            SetBoolName("inAir", InAir);            
            SetBoolName("action", true);
            Debug.Log("SetBoolName = true");
            SetIntName("actionCounter", CurrentActionCounter);            

            OnEnter?.Invoke();
        }

        /// <summary>
        /// PlayerWeaponState Exit 에서 호출되는 함수
        /// </summary>
        public virtual void ExitWeapon()
        {
            SetBoolName("action", false);
            Debug.Log("SetBoolName = false");
            CurrentActionCounter++;
            actionCounterResetTimer.StartTimer();

            OnExit?.Invoke();
        }

        public void SetCore(Core core)
        {
            if(core == null)
            {
                Debug.Log($"{transform.name} is core Null");
                return;
            }
            WeaponCore = core;
        }
        public void SetData(WeaponDataSO data)
        {
            this.weaponData = data;
        }

        public void ChangeActionCounter(int value)
        {
            CurrentActionCounter = value;
        }

        public void ResetActionCounter()
        {
            CurrentActionCounter = 0;
        }

        #region Set Func

        /// <summary>
        /// Animator Parameter Set Func
        /// </summary>
        /// <param name="name"> Param Name</param>
        /// <param name="setbool">T, F</param>
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
        #endregion


        /// <summary>
        /// 초기 State 설정
        /// </summary>
        /// <param name="state"></param>
        public void InitializeWeapon(PlayerWeaponState state, Core core)
        {
            this.state = state;
            this.WeaponCore = core;
        }
    }

}