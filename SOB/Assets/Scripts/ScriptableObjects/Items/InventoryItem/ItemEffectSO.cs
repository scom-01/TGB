using UnityEngine;

[CreateAssetMenu(fileName = "newItemEffectData", menuName = "Data/Item Data/ItemEffect Data")]
public abstract class ItemEffectSO : ScriptableObject
{
    public abstract void ExcuteEffect(StatsItemSO parentItem, Unit unit);
}