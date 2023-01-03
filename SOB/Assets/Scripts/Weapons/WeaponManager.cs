using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [HideInInspector]
    public float lastAttackTime;

    [HideInInspector] public Weapon weapon;

    // Start is called before the first frame update
    protected void Start()
    {
        
    }

    // Update is called once per frame
    protected void Update()
    {
        if(weapon != null)
        {
            //Debug.Log("lastAttackTime = " + lastAttackTime);
            if (Time.time >= lastAttackTime + weapon.weaponData.comboResetTime)
            {
                weapon.attackCounter = 0;
            }
        }
    }
}
