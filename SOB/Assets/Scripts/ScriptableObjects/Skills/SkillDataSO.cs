using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using SOB.Weapons.Components;
using System;

[CreateAssetMenu(fileName = "newSkillData", menuName = "Data/Skill Data/Skill Data")]
public class SkillDataSO : ScriptableObject
{
    [field: SerializeField] public int NumberOfActions { get; private set; }
    [field: SerializeField] public GameObject EffectPrefab { get; private set; }
    [field: SerializeReference] public List<ComponentData> ComponentData { get; private set; }

    public T GetData<T>()
    {
        return ComponentData.OfType<T>().FirstOrDefault();
    }

    public List<Type> GetAllDependencies()
    {
        return ComponentData.Select(component => component.ComponentDependency).ToList();
    }
    public void AddData(ComponentData data)
    {
        if (ComponentData.FirstOrDefault(t => t.GetType() == data.GetType()) != null)
            return;

        ComponentData.Add(data);
    }
}
