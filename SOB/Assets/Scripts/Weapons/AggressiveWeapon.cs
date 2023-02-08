using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using SOB.Weapons;
using SOB.Weapons.Components;

public class AggressiveWeapon : Weapon
{
    protected WeaponDataSO aggressiveWeaponData;
    [SerializeField]
    [Tooltip("Animator Param Bool Name")]
    protected string boolname;
    private List<IDamageable> detectedDamagable = new List<IDamageable>();

    protected override void Awake()
    {
        base.Awake();

        if(weaponData.GetType() == typeof(WeaponDataSO))
        {
            aggressiveWeaponData = weaponData;
        }
        else
        {
            Debug.LogError($"Wrong data for the weapon, this weaponType is {aggressiveWeaponData.GetType()}");
        }
    }

    public override void EnterWeapon()
    {
        base.EnterWeapon();

        SetBoolName(boolname, true);

        baseAnimator.SetInteger("actionCounter", CurrentActionCounter);
        //WeaponAnimator.SetInteger("actionCounter", currentActionCounter);
    }

    public override void ExitWeapon()
    {
        base.ExitWeapon();

        SetBoolName(boolname, false);

        ChangeActionCounter(CurrentActionCounter + 1);

        gameObject.SetActive(false);
    }

    private void CheckMeleeAttack()
    {
        /*WeaponAttackDetails details = aggressiveWeaponData.AttackDetails[currentActionCounter];

        foreach(IDamageable item in detectedDamagable.ToList())
        {
            item.Damage(details.damageAmount);
        }*/
    }
    
    

    public void AddToDetected(Collider2D coll)
    {
        Debug.Log("AddToDetected = " + coll.name);
        IDamageable damagable = coll.GetComponent<IDamageable>();

        if (damagable != null)
        {
            detectedDamagable.Add(damagable);
        }
    }

    public void RemoveFromDetected(Collider2D coll)
    {
        Debug.Log("RemoveFromDetected = " + coll.name);
        IDamageable damagable = coll.GetComponent<IDamageable>();

        if (damagable != null)
        {
            detectedDamagable.Remove(damagable);
        }
    }
}
