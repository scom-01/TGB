using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct ItemData
{
    [Tooltip("아이템 이름")]
    public string ItemName;
    [Tooltip("아이템 설명")]
    [field: TextArea]
    public string ItemDescription;
    [Tooltip("아이템 Sprite아이콘")]
    public Sprite ItemSprite;
    
    [Tooltip("아이템 추가스텟")]
    public CommonData commomData;
}
