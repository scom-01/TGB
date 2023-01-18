using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using SOB.CoreSystem;

namespace SOB.CoreSystem
{
    public class Core : MonoBehaviour
    {
        private readonly List<CoreComponent> CoreComponents = new List<CoreComponent>();

        public void LogicUpdate()
        {
            foreach(CoreComponent component in CoreComponents)
            {
                component.LogicUpdate();
            }
        }

        public void AddComponent(CoreComponent component)
        {
            if(!CoreComponents.Contains(component))
            {
                CoreComponents.Add(component);
            }
        }


        public T GetCoreComponent<T>() where T : CoreComponent
        {
            var comp = CoreComponents.OfType<T>().FirstOrDefault();

            if (comp)
                return comp;

            comp = GetComponentInChildren<T>();

            if (comp)
                return comp;

            Debug.LogWarning($"{typeof(T)} not found on {transform.parent.name}");
            return null;
        }

        public T GetCoreComponent<T>(ref T value) where T : CoreComponent
        {
            value = GetCoreComponent<T>();
            return value;
        }

        public Unit Unit
        {
            get => GenericNotImplementedError<Unit>.TryGet(unit, transform.parent.name);
            private set => unit = value;
        }
        public CollisionSenses CollisionSenses
        {
            get => GenericNotImplementedError<CollisionSenses>.TryGet(collisionSenses, transform.parent.name);
            private set => collisionSenses = value;
        }
        public Combat Combat
        {
            get => GenericNotImplementedError<Combat>.TryGet(combat, transform.parent.name);
            private set => combat = value;
        }

        private Movement movement;
        private Unit unit;
        private CollisionSenses collisionSenses;
        private Combat combat;
        public virtual void Awake()
        {
            movement = GetComponentInChildren<Movement>();
            collisionSenses = GetComponentInChildren<CollisionSenses>();
            unit = GetComponentInParent<Unit>();
            combat = GetComponentInParent<Combat>();
        }
    }
}