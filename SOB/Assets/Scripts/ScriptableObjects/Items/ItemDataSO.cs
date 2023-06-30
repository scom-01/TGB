using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SOB.CoreSystem;
using UnityEditor;
using UnityEngine.Localization;

[CreateAssetMenu(fileName = "newItemData", menuName = "Data/Item Data/Item Data")]
public class ItemDataSO : ScriptableObject
{
    [field: Tooltip("아이템 index")]
    public int ItemIdx;
    public ItemData itemData;
}

[Serializable]
public struct ItemData
{
    [field: Header("Item")]
    [field: Tooltip("아이템 이름")]
    public string ItemName;
    [field: SerializeField] public LocalizedString ItemNameLocal { get; private set; }
    [Tooltip("아이템 설명")]
    [field: TextArea]
    public string ItemDescription;
    [field: SerializeField] public LocalizedString ItemDescriptionLocal { get; private set; }

    [field: Tooltip("아이템 Sprite아이콘")]
    public Sprite ItemSprite;
}