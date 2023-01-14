using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SOB.CoreSystem;
public class Combat : CoreComponent, IDamageable
{
    public void Damage(float amount = 0)
    {
        Debug.Log(core.transform.parent.name + " " + amount + " Damaged!");
    }
}
