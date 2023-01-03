using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    public SO_WeaponData weaponData;

    public string weaponAnimBoolStr;

    [Tooltip("°ø°Ý È½¼ö")]
    public int attackCounter;

    protected Animator baseAnimator;
    protected Animator WeaponAnimator;

    [HideInInspector]
    protected WeaponManager weaponManager;

    [HideInInspector]
    public bool InAir;

    protected PlayerWeaponState state;
    protected virtual void Awake()
    {
        baseAnimator = transform.Find("Base").GetComponent<Animator>();
        WeaponAnimator = transform.Find("Weapon").GetComponent<Animator>();

        weaponManager = GetComponentInParent<WeaponManager>();
    }

    public virtual void EnterWeapon()
    {
        gameObject.SetActive(true);

        if (InAir)
        {
            SetBoolName("inAir", true);
        }
        else
        {
            SetBoolName("inAir", false);
        }

        weaponManager.lastAttackTime = Time.time;
    }

    public virtual void ExitWeapon()
    {
        gameObject.SetActive(false);
    }

    #region Set Func
    
    public virtual void SetBoolName(string name, bool setbool)
    {
        weaponAnimBoolStr = name;
        
        baseAnimator.SetBool(name, setbool);
        WeaponAnimator.SetBool(name, setbool);
    }
    #endregion

    #region Animation Triggers
    public virtual void AnimationFinishTrigger()
    {
        state.AnimationFinishTrigger();
    }

    public virtual void AnimationStartMovementTrigger()
    {
        state.SetPlayerVelocity(weaponData.movementSpeed[attackCounter]);
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
