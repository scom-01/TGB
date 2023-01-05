using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [HideInInspector]
    public float lastAttackTime;

    [HideInInspector] public AggressiveWeapon aggressiveweapon;
    [HideInInspector] public DefensiveWeapon defensiveweapon;

    // Start is called before the first frame update
    protected void Start()
    {
        
    }

    // Update is called once per frame
    protected void Update()
    {
        if(aggressiveweapon != null)
        {
            //Debug.Log("lastAttackTime = " + lastAttackTime);
            if (Time.time >= lastAttackTime + aggressiveweapon.weaponData.comboResetTime)
            {
                aggressiveweapon.ResetAttackCounter();
            }
        }
    }
}
