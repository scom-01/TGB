using System.Collections;
using System.Collections.Generic;
using SCOM.Item;
using UnityEngine;


public class ItemManager : MonoBehaviour
{
    public GameObject InventoryItem;


    public GameObject SpawnItem(GameObject SpawnPrefab, Vector3 pos, Transform transform, StatsItemSO itemData)
    {
        if (SpawnPrefab == null)
            return null;

        var item = Instantiate(SpawnPrefab, pos, Quaternion.identity, transform);
        item.GetComponent<SOB_Item>().Item = itemData;
        item.GetComponent<SOB_Item>().Init();

        return item;
    }
}