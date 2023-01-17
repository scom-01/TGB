using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWeaponSpeed : MonoBehaviour
{
    private GameObject baseObject;

    public float WeaponSpeed;
    private void Awake()
    {
        baseObject = transform.Find("Base").gameObject;
    }

    private void Update()
    {
        baseObject.GetComponent<Animator>().speed = WeaponSpeed;
    }
}
