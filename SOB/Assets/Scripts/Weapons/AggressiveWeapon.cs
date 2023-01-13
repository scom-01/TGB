using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AggressiveWeapon : Weapon
{
    protected SO_AggressiveWeaponData aggressiveWeaponData;
    [SerializeField]
    [Tooltip("Animator Param Bool Name")]
    protected string boolname;
    private List<IDamageable> detectedDamagable = new List<IDamageable>();

    protected override void Awake()
    {
        base.Awake();

        if(weaponData.GetType() == typeof(SO_AggressiveWeaponData))
        {
            aggressiveWeaponData = (SO_AggressiveWeaponData)weaponData;
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

        baseAnimator.SetInteger("attackCounter", CurrentAttackCounter);
        //WeaponAnimator.SetInteger("attackCounter", CurrentAttackCounter);
    }

    public override void ExitWeapon()
    {
        base.ExitWeapon();

        SetBoolName(boolname, false);

        ChangeAttackCounter(CurrentAttackCounter + 1);

        gameObject.SetActive(false);
    }

    public override void AnimationActionTrigger()
    {
        base.AnimationActionTrigger();
        CheckMeleeAttack();
    }

    private void CheckMeleeAttack()
    {
        WeaponAttackDetails details = aggressiveWeaponData.AttackDetails[CurrentAttackCounter];

        foreach(IDamageable item in detectedDamagable.ToList())
        {
            item.Damage(details.damageAmount);
        }
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

    public override void AnimationStartMovementTrigger()
    {
        base.AnimationStartMovementTrigger();
        state.SetPlayerVelocity(weaponData.movementSpeed[CurrentAttackCounter]);
    }

    public override void AnimationStopMovementTrigger()
    {
        base.AnimationStopMovementTrigger();
    }
}
