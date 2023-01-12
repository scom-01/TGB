using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefensiveWeapon : Weapon
{
    protected SO_DefensiveWeaponData defensiveWeaponData;
    [SerializeField]
    [Tooltip("Animator Param Bool Name")]
    protected string boolname;

    protected override void Awake()
    {
        base.Awake();

        if (weaponData.GetType() == typeof(SO_DefensiveWeaponData))
        {
            defensiveWeaponData = (SO_DefensiveWeaponData)weaponData;
        }
        else
        {
            Debug.LogError($"Wrong data for the weapon, this weaponType is {defensiveWeaponData.GetType()}");
        }
    }

    public override void EnterWeapon()
    {
        base.EnterWeapon();

        SetBoolName(boolname, true);
    }

    public override void ExitWeapon()
    {
        base.ExitWeapon();

        SetBoolName(boolname, false);

        gameObject.SetActive(false);
    }

    

    public override void AnimationActionTrigger()
    {
        base.AnimationActionTrigger();
    }
}
