using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [field: SerializeField] public SO_WeaponData weaponData { get; private set; }
    [SerializeField] private float attackCounterResetCooldown;

    [HideInInspector]
    protected string weaponAnimBoolStr;

    [Tooltip("공격 횟수")]
    public int CurrentAttackCounter
    {
        get => currentAttackCounter;
        private set => currentAttackCounter = value >= weaponData.amountOfAttacks ? 0 : value;
    }

    public Core core { get; private set; }

    private int currentAttackCounter;
    private Timer attackCounterResetTimer;

    public event Action OnEnter;
    public event Action OnExit;

    protected Animator baseAnimator;
    public GameObject BaseGameObject { get; private set; }    
    protected Animator WeaponAnimator;
    public GameObject WeaponSpriteGameObject { get; private set; }

    public AnimationEventHandler EventHandler { get; private set; }

    [HideInInspector]
    protected WeaponManager weaponManager;

    [HideInInspector]
    public bool InAir;

    protected PlayerWeaponState state;
    protected virtual void Awake()
    {
        BaseGameObject = transform.Find("Base").gameObject;
        WeaponSpriteGameObject = transform.Find("Weapon").gameObject;
        baseAnimator = transform.Find("Base").GetComponent<Animator>();
        WeaponAnimator = transform.Find("Weapon").GetComponent<Animator>();

        weaponManager = GetComponentInParent<WeaponManager>();
        //attackCounterResetTimer = new Timer(attackCounterResetCooldown);
    }

    private void Update()
    {
        //attackCounterResetTimer.Tick();
    }

    private void OnEnable()
    {
        EventHandler.OnFinish += ExitWeapon;
        //attackCounterResetTimer.OnTimerDone += ResetAttackCounter;
    }

    private void OnDisable()
    {
        EventHandler.OnFinish -= ExitWeapon;
        //attackCounterResetTimer.OnTimerDone -= ResetAttackCounter;
    }

    /// <summary>
    /// PlayerWeaponState Enter 에서 호출되는 함수
    /// </summary>
    public virtual void EnterWeapon()
    {
        gameObject.SetActive(true);

        //attackCounterResetTimer.StopTimer();

        if (InAir)
        {
            SetBoolName("inAir", true);            
        }
        else
        {
            SetBoolName("inAir", false);
        }

        OnEnter?.Invoke();

        weaponManager.lastAttackTime = Time.time;
    }

    /// <summary>
    /// PlayerWeaponState Exit 에서 호출되는 함수
    /// </summary>
    public virtual void ExitWeapon()
    {
        BaseGameObject.GetComponent<SpriteRenderer>().sprite = null;
        WeaponSpriteGameObject.GetComponent<SpriteRenderer>().sprite = null;
        gameObject.SetActive(false);

        OnExit?.Invoke();
    }

    public void ChangeAttackCounter(int value)
    {
        CurrentAttackCounter = value;
    }

    public void ResetAttackCounter()
    {
        CurrentAttackCounter = 0;
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
        //WeaponAnimator.SetBool(name, setbool);
    }
    #endregion

    #region Animation Triggers
    public virtual void AnimationFinishTrigger()
    {
        state.AnimationFinishTrigger();
    }

    public virtual void AnimationStartMovementTrigger()
    {
        //state.SetPlayerVelocity(weaponData.movementSpeed[CurrentAttackCounter]);
    }

    public virtual void AnimationStopMovementTrigger()
    {
        state.SetPlayerVelocity(0f);
    }

    public virtual void AnimationTurnOnFlipTrigger()
    {
        state.SetFlipCheck(true);
    }

    public virtual void AnimationTurnOffFlipTrigger()
    {
        state.SetFlipCheck(false);
    }

    public virtual void AnimationActionTrigger()
    {

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
