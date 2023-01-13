using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using SOB.Weapons.Components;

[CreateAssetMenu(fileName = "newWeaponData", menuName = "Data/Weapon Data/Basic Weapon Data", order = 0)]
public class WeaponDataSO : ScriptableObject
{
    [field: SerializeField] public int NumberOfAttacks { get; private set; }

    [field: SerializeField] public List<ComponentData> ComponentDatas { get; private set; }

    public T GetData<T>()
    {
        return ComponentDatas.OfType<T>().FirstOrDefault();
    }

    public void AddData(ComponentData data)
    {
        if (ComponentDatas.FirstOrDefault(t => t.GetType() == data.GetType()) != null)
            return;

        ComponentDatas.Add(data);
    }
}
