using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    [field: SerializeField] public ItemDataSO itemData { get; private set; }
}

