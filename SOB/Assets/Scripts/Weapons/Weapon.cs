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
        [field: SerializeField] public WeaponDataSO weaponData { get; private set; }
        [SerializeField] private float actionCounterResetCooldown;

        [HideInInspector]
        protected string weaponAnimBoolStr;

        [Tooltip("공격 횟수")]
        public int CurrentActionCounter
        {
            get => currentActionCounter;
            private set => currentActionCounter = value >= weaponData.NumberOfActions ? 0 : value;
        }

        public Core WeaponCore { get; private set; }

        private int currentActionCounter;
        private Timer actionCounterResetTimer;

        public event Action OnEnter;
        public event Action OnExit;

        protected Animator baseAnimator;
        public GameObject BaseGameObject { get; private set; }
        protected Animator WeaponAnimator;
        public GameObject WeaponSpriteGameObject { get; private set; }

        public AnimationEventHandler EventHandler { get; private set; }

        [HideInInspector]
        public bool InAir;

        protected PlayerWeaponState state;
        protected virtual void Awake()
        {
            BaseGameObject = transform.Find("Base").gameObject;
            WeaponSpriteGameObject = transform.Find("Weapon").gameObject;

            baseAnimator = transform.Find("Base").GetComponent<Animator>();

            EventHandler = BaseGameObject.GetComponent<AnimationEventHandler>();
            
            actionCounterResetTimer = new Timer(actionCounterResetCooldown);
        }

        private void Update()
        {
            actionCounterResetTimer.Tick();
        }

        private void OnEnable()
        {
            EventHandler.OnFinish += ExitWeapon;
            actionCounterResetTimer.OnTimerDone += ResetactionCounter;
        }

        private void OnDisable()
        {
            EventHandler.OnFinish -= ExitWeapon;
            actionCounterResetTimer.OnTimerDone -= ResetactionCounter;
        }

        /// <summary>
        /// PlayerWeaponState Enter 에서 호출되는 함수
        /// </summary>
        public virtual void EnterWeapon()
        {
            print($"{transform.name} enter");

            //gameObject.SetActive(true);

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

            //BaseGameObject.GetComponent<SpriteRenderer>().sprite = null;
            //WeaponSpriteGameObject.GetComponent<SpriteRenderer>().sprite = null;


            OnExit?.Invoke();

            //gameObject.SetActive(false);
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

        public void ChangeactionCounter(int value)
        {
            CurrentActionCounter = value;
        }

        public void ResetactionCounter()
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
        public void InitializeWeapon(PlayerWeaponState state)
        {
            this.state = state;
        }
    }

}