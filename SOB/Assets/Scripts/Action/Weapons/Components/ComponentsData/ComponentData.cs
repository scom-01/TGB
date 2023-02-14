using System;
using UnityEngine;

namespace SOB.Weapons.Components
{
    [Serializable]
    public class ComponentData
    {
        [SerializeField, HideInInspector] private string name;

        public Type ComponentDependency { get; protected set; }
        public ComponentData()
        {
            SetComponentName();
        }
        public void SetComponentName() => name = GetType().Name;

        public virtual void SetActionDataNames() { }

        public virtual void InitializeActionData(int numberOfActions) { }
    }

    [Serializable]
    public class ComponentData<T> : ComponentData where T : ActionData
    {
        [SerializeField] private T[] actionData;
        public T[] ActionData { get => actionData; private set => actionData = value; }
        public override void SetActionDataNames()
        {
            base.SetActionDataNames();
            for(var i = 0; i <ActionData.Length; i++)
            {
                ActionData[i].SetAttackName(i + 1);
            }
        }

        public override void InitializeActionData(int numberOfActions)
        {
            base.InitializeActionData(numberOfActions);

            var oldLen = ActionData != null ? actionData.Length : 0;

            if (oldLen == numberOfActions)
                return;

            Array.Resize(ref actionData, numberOfActions);

            if (oldLen < numberOfActions)
            {
                for (var i = oldLen; i < actionData.Length; i++)
                {
                    var newObj = Activator.CreateInstance(typeof(T)) as T;
                    actionData[i] = newObj;
                }
            }

            SetActionDataNames();
        }
    }
}

