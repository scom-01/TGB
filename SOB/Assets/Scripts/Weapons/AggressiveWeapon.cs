using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AggressiveWeapon : Weapon
{
    protected SO_AggressiveWeaponData aggressiveWeaponData;

    private List<IDamagable> detectedDamagable = new List<IDamagable>();

    protected override void Awake()
    {
        base.Awake();

        if(weaponData.GetType() == typeof(SO_AggressiveWeaponData))
        {
            aggressiveWeaponData = (SO_AggressiveWeaponData)weaponData;
        }
        else
        {
            Debug.LogError("Wrong data for the weapon");
        }
    }

    public override void AnimationActionTrigger()
    {
        base.AnimationActionTrigger();
        CheckMeleeAttack();
    }

    private void CheckMeleeAttack()
    {
        WeaponAttackDetails details = aggressiveWeaponData.AttackDetails[attackCounter];

        foreach(IDamagable item in detectedDamagable.ToList())
        {
            item.Damage(details.damageAmount);
        }
    }

    public void AddToDetected(Collider2D coll)
    {
        Debug.Log("AddToDetected = " + coll);
        IDamagable damagable = coll.GetComponent<IDamagable>();

        if (damagable != null)
        {
            detectedDamagable.Add(damagable);
        }
    }

    public void RemoveFromDetected(Collider2D coll)
    {
        Debug.Log("RemoveFromDetected = " + coll.name);
        IDamagable damagable = coll.GetComponent<IDamagable>();

        if (damagable != null)
        {
            detectedDamagable.Remove(damagable);
        }
    }
}
