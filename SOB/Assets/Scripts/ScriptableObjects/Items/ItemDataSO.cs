using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "newItemData", menuName = "Data/Item Data/Item Data")]
public class ItemDataSO : ScriptableObject
{
    [Tooltip("획득 시 이펙트")]
    [field: SerializeField] public GameObject AcquiredEffectPrefab { get; private set; }
    [field: SerializeField] public bool DetailSubUI { get; private set; }

}
