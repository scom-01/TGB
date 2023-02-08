using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newItemData", menuName = "Data/Item Data/Buff Data")]
public class BuffItemSO : ItemDataSO
{
    public override void Actions()
    {
        base.Actions();
        Debug.Log("Item Actions");
    }
}
