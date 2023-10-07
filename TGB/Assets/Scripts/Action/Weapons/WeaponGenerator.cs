using TGB.Weapons.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace TGB.Weapons
{
    [Serializable]
    public struct WeaponData
    {
        public WeaponCommandDataSO weaponCommandDataSO;
        public WeaponDataSO weaponDataSO;
    }

    public class WeaponGenerator : MonoBehaviour
    {
        protected Weapon Weapon
        {
            get
            {
                if (weapon == null)
                    weapon = this.GetComponent<Weapon>();
                return weapon;
            }
            set
            {
                weapon = value;
            }
        }
        private Weapon weapon;
        [SerializeField] public WeaponData weaponData;

        private List<WeaponComponent> componentsAllreadyOnWeapon = new List<WeaponComponent>();
        private List<WeaponComponent> componentsAddedToWeapon = new List<WeaponComponent>();
        private List<Type> componentsDependencies = new List<Type>();

        private void Start()
        {
            Init();
        }

        public void Init()
        {
            if (weaponData.weaponCommandDataSO != null)
            {
                Weapon.SetCommandData(weaponData.weaponCommandDataSO);
            }
            else
            {
                if (weaponData.weaponDataSO != null)
                    GenerateWeapon(weaponData.weaponDataSO);
            }
        }

        [ContextMenu("Set Weapon Generate")]
        private void TestGeneration()
        {
            GenerateWeapon(weaponData.weaponDataSO);
        }

        [ContextMenu("Clear Weapon Generate")]
        private void ClearGeneration()
        {
            componentsAllreadyOnWeapon.Clear();
            componentsAddedToWeapon.Clear();
            componentsDependencies.Clear();
            componentsAllreadyOnWeapon = GetComponents<WeaponComponent>().ToList();
            foreach (var weaponComponent in componentsAllreadyOnWeapon)
            {
                DestroyImmediate(weaponComponent);
            }
        }

        public void GenerateWeapon(WeaponDataSO data)
        {
            Weapon.SetData(data);
            componentsAllreadyOnWeapon.Clear();
            componentsAddedToWeapon.Clear();
            componentsDependencies.Clear();

            componentsAllreadyOnWeapon = GetComponents<WeaponComponent>().ToList();

            componentsDependencies = data.GetAllDependencies();
            foreach (var dependency in componentsDependencies)
            {
                //componentsAddedToWeapon List안에 dependency와 같은 타입이 존재 할 때
                if (componentsAddedToWeapon.FirstOrDefault(component => component.GetType() == dependency))
                    continue;


                var weaponComponent = componentsAllreadyOnWeapon.FirstOrDefault(component => component.GetType() == dependency);
                
                //componentsAllreadyOnWeapon List안에 dependency와 같은 타입이 존재 하지않을 때
                if (weaponComponent == null)    
                {
                    weaponComponent = gameObject.AddComponent(dependency) as WeaponComponent;
                }

                weaponComponent.Init();

                componentsAddedToWeapon.Add(weaponComponent);
            }

            //componentsAllreadyOnWeapon안에 componentsAddedToWeapon와 같은 항목을 제거 ex) {1,2,3}.Except(1) = {2, 3}
            var componentsToRemove = componentsAllreadyOnWeapon.Except(componentsAddedToWeapon);

            foreach(var weaponComponent in componentsToRemove)
            {
                Destroy(weaponComponent);
            }
        }
        public bool GenerateWeapon(AnimCommand CommandData)
        {
            if (CommandData.animOC == null || CommandData.data == null)
                return false;
            Weapon.oc = CommandData.animOC;
            GenerateWeapon(CommandData.data);
            return true;
        }
    }
}
