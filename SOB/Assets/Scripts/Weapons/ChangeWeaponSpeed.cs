using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWeaponSpeed : MonoBehaviour
{
    private GameObject baseObject;
    private GameObject weaponObject;

    public float WeaponSpeed;
    private void Awake()
    {
        baseObject = transform.Find("Base").gameObject;
        weaponObject = transform.Find("Weapon").gameObject;
    }

    private void Update()
    {
        baseObject.GetComponent<Animator>().speed = WeaponSpeed;
        //weaponObject.GetComponent<Animator>().speed = WeaponSpeed;
    }
}
