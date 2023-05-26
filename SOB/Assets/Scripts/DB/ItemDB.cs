using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newItemDB", menuName = "Data/DB/ItemDB")]
public class ItemDB : ScriptableObject
{
    public List<ItemDataSO> ItemDBList;
}
