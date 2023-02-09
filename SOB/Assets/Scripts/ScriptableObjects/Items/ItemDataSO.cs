using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SOB.CoreSystem;

[CreateAssetMenu(fileName = "newItemData", menuName = "Data/Item Data/Item Data")]
public class ItemDataSO : ScriptableObject
{
    [field: Header("Item")]
    [field: Tooltip("아이템 Data")]
    public ItemData ItemData;

    [field: Header("Buff")]
    public EVENT_BUFF_TYPE[] BuffType;
    public EVENT_BUFF_STATS[] BuffName;
    public float[] BuffValue;
    public float[] BuffDurationTime;

    public virtual void Actions()
    {

    }

    //--Collider--
    [field: Header("Collider Use")]
    [field: Tooltip("획득 시 이펙트")]
    [field: SerializeField] public GameObject AcquiredEffectPrefab { get; private set; }
    
    [field: Tooltip("아이템 소비 여부")]
    [field: SerializeField] public bool isEquipment { get; private set; }

    //--Detect--
    [field: Header("Detect Use")]
    [field: Tooltip("Detect 시 SubUI 표시 여부")]
    [field: SerializeField] public bool DetailSubUI { get; private set; }
}
