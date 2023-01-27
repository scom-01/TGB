using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SOB.Weapons;
using SOB.Item;
using Unity.VisualScripting;
using static UnityEditor.Progress;

public class Inventory : MonoBehaviour
{
    public Weapon[] weapons;
    public SOB_Item[] items;

    private void Start()
    {
        if(weapons == null || weapons?.Length == 0)
        {
            weapons = GetComponentsInChildren<Weapon>();
            if (weapons == null)
            {
                Debug.LogWarning($"{transform.name}'s Weapons is empty in The Inventory");
            }
        }

        if(items == null || items?.Length == 0)
        {
            items = GetComponentsInChildren<SOB_Item>();
            if (weapons == null)
            {
                Debug.LogWarning($"{transform.name}'s Items is empty in The Inventory");
            }
        }
    }

    private void Update()
    {
        var oldWeapon = weapons;
        if(weapons != oldWeapon)
        {
            ChangeWeaponAttribute();
        }

        var oldItem = items;
        if(items != oldItem)
        {
            ChangeItemAttribute();
        }
    }

    private void ChangeWeaponAttribute()
    {

    }

    private void ChangeItemAttribute()
    {

    }
}
