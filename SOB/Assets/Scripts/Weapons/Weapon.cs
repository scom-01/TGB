using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    public SO_WeaponData weaponData;

    [HideInInspector]
    protected string weaponAnimBoolStr;

    [Tooltip("°ø°Ý È½¼ö")]
    public int CurrentAttackCounter
    {
        get => currentAttackCounter;
        private set => currentAttackCounter = value >= weaponData.amountOfAttacks ? 0 : value;
    }

    private int currentAttackCounter;
    private Timer attackCounterResetTimer;

    public event Action OnEnter;
    public event Action OnExit;

    protected Animator baseAnimator;
    public GameObject BaseGameObject { get; private set; }    
    protected Animator WeaponAnimator;
    public GameObject WeaponSpriteGameObject { get; private set; }

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
    }

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

    public void InitializeWeapon(PlayerWeaponState state)
    {
        this.state = state;
    }
}
