using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected SO_WeaponData weaponData;

    protected Animator baseAnimator;
    protected Animator WeaponAnimator;

    protected int attackCounter;

    public bool InAir;

    protected PlayerAttackState state;
    protected virtual void Awake()
    {
        baseAnimator = transform.Find("Base").GetComponent<Animator>();
        WeaponAnimator = transform.Find("Weapon").GetComponent<Animator>();
    }

    public virtual void EnterWeapon()
    {
        gameObject.SetActive(true);

        if (InAir)
        {
            baseAnimator.SetBool("inAir", true);
            WeaponAnimator.SetBool("inAir", true);
        }
        else
        {
            baseAnimator.SetBool("inAir", false);
            WeaponAnimator.SetBool("inAir", false);
        }

        if (attackCounter >= weaponData.amountOfAttacks)
        {
            attackCounter = 0;
        }

        if (baseAnimator != null)
        {
            baseAnimator.SetBool("attack", true);
            baseAnimator.SetInteger("attackCounter", attackCounter);
        }
        else
        {
            baseAnimator = transform.Find("Base").GetComponent<Animator>();
            baseAnimator.SetBool("attack", true);
            baseAnimator.SetInteger("attackCounter", attackCounter);
        }

        if (WeaponAnimator != null)
        {
            WeaponAnimator.SetBool("attack", true);
            WeaponAnimator.SetInteger("attackCounter", attackCounter);
        }
        else
        {
            WeaponAnimator = transform.Find("Weapon").GetComponent<Animator>();
            WeaponAnimator.SetBool("attack", true);
            WeaponAnimator.SetInteger("attackCounter", attackCounter);
        }
    }

    public virtual void ExitWeapon()
    {
        if (baseAnimator != null)
            baseAnimator.SetBool("attack", false);
        else
        {
            baseAnimator = transform.Find("Base").GetComponent<Animator>();
            baseAnimator.SetBool("attack", false);
        }

        if (WeaponAnimator != null)
            WeaponAnimator.SetBool("attack", false);
        else
        {
            WeaponAnimator = transform.Find("Weapon").GetComponent<Animator>();
            WeaponAnimator.SetBool("attack", false);
        }

        attackCounter++;

        gameObject.SetActive(false);
    }

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

    public void InitializeWeapon(PlayerAttackState state)
    {
        this.state = state;
    }
}
