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

        SetBoolName("block", true);
    }

    public override void ExitWeapon()
    {
        base.ExitWeapon();

        SetBoolName("block", false);

        gameObject.SetActive(false);
    }

    

    public override void AnimationActionTrigger()
    {
        base.AnimationActionTrigger();
    }
}
