using SOB.Item;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "newItemData", menuName = "Data/Item Data/Stats Data")]
public class StatsItemSO : ItemDataSO
{
    [Tooltip("아이템이 갖는 스탯")]
    public StatsData StatsDatas;
    //--Collider--
    [field: Header("Collider Use")]
    [field: Tooltip("획득 시 이펙트")]
    [field: SerializeField] public GameObject AcquiredEffectPrefab { get; private set; }
    [field: Tooltip("획득 시 사운드이펙트")]
    [field: SerializeField] public AudioClip AcquiredSoundEffect { get; private set; }

    [field: Tooltip("아이템 소비 여부")]
    [field: SerializeField] public bool isEquipment { get; private set; }

    //--Detect--
    [field: Header("Detect Use")]
    [field: Tooltip("Detect 시 SubUI 표시 여부")]
    [field: SerializeField] public bool DetailSubUI { get; private set; }

    [field: SerializeField] public List<ItemEffectSO> ItemEffects = new List<ItemEffectSO>();

    public virtual void Use(Unit unit)
    {
        foreach(ItemEffectSO effect in ItemEffects)
        {
            effect.ExcuteEffect(this, unit);
        }
    }
}