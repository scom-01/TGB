using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHitboxToWeapon : MonoBehaviour
{
    private AggressiveWeapon weapon;


    private void Awake()
    {
        weapon = GetComponentInParent<AggressiveWeapon>();        
    }
    private void OnTriggerEnter2D(Collider2D coll)
    {
        Debug.Log("AddToDetected");
        weapon.AddToDetected(coll);
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        Debug.Log("RemoveFromDetected = " + coll);
        weapon.RemoveFromDetected(coll);
    }
}
