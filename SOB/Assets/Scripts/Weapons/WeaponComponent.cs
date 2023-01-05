using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponComponent : MonoBehaviour
{
    protected Weapon weapon;

    protected virtual void Awake()
    {
        weapon =GetComponent<Weapon>();
    }
}
