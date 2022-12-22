using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    protected Animator baseAnimator;
    protected Animator WeaponAnimator;

    protected PlayerAttackState state;
    protected virtual void Start()
    {
        baseAnimator = transform.Find("Base").GetComponent<Animator>();
        WeaponAnimator = transform.Find("Weapon").GetComponent<Animator>();
    }

    public virtual void EnterWeapon()
    {
        gameObject.SetActive(true);

        if (baseAnimator != null)
            baseAnimator.SetBool("attack", true);
        else
        {
            baseAnimator = transform.Find("Base").GetComponent<Animator>();
            baseAnimator.SetBool("attack", true);
        }

        if (WeaponAnimator != null)
            WeaponAnimator.SetBool("attack", true);
        else
        {
            WeaponAnimator = transform.Find("Weapon").GetComponent<Animator>();
            WeaponAnimator.SetBool("attack", true);
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

        gameObject.SetActive(false);
    }

    #region Animation Triggers
    public virtual void AnimationFinishTrigger()
    {
        state.AnimationFinishTrigger();
    }
    #endregion

    public void InitializeWeapon(PlayerAttackState state)
    {
        this.state = state;
    }
}
