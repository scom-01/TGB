using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SOB.Weapons;

public class DefensiveWeapon : Weapon
{
    protected WeaponDataSO defensiveWeaponData;
    [SerializeField]
    [Tooltip("Animator Param Bool Name")]
    protected string boolname;

    protected override void Awake()
    {
        base.Awake();

        if (weaponData.GetType() == typeof(WeaponDataSO))
        {
            defensiveWeaponData = weaponData;
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
}
