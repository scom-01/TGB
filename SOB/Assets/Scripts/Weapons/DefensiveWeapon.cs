using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefensiveWeapon : Weapon
{
    protected override void Awake()
    {
        base.Awake();
    }

    public override void EnterWeapon()
    {
        base.EnterWeapon();

        SetBoolName("attack", true);
    }

    public override void ExitWeapon()
    {
        base.ExitWeapon();

        SetBoolName("attack", false);

        gameObject.SetActive(false);
    }

    

    public override void AnimationActionTrigger()
    {
        base.AnimationActionTrigger();
    }
}
