using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "newItemData", menuName = "Data/Item Data/Item Data")]
public class ItemDataSO : ScriptableObject
{
    [field: Tooltip("아이템 Data")]
    public ItemData ItemData;

    [field: SerializeField]
    [field: Tooltip("IncreaseHealth")]
    public Dictionary<EVENT_ITEM_TYPE, float> Event_item_type = new Dictionary<EVENT_ITEM_TYPE, float>();
    public EVENT_ITEM_TYPE[] coroutineName;
    public float[] coroutineValue;

    //--Collider--
    [field: Header("Collider Use")]
    [field: Tooltip("획득 시 이펙트")]
    [field: SerializeField] public GameObject AcquiredEffectPrefab { get; private set; }
    
    [field: Tooltip("충돌 시 소모여부")]
    [field: SerializeField] public bool ConflictUse { get; private set; }

    //--Detect--
    [field: Header("Detect Use")]
    [field: Tooltip("Detect 시 SubUI 표시 여부")]
    [field: SerializeField] public bool DetailSubUI { get; private set; }
}