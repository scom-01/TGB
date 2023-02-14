using SOB.Weapons.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace SOB.Weapons
{
    public class WeaponGenerator : MonoBehaviour
    {
        private Weapon weapon;
        [SerializeField] private WeaponDataSO weaponData;

        private List<WeaponComponent> componentsAllreadyOnWeapon = new List<WeaponComponent>();
        private List<WeaponComponent> componentsAddedToWeapon = new List<WeaponComponent>();
        private List<Type> componentsDependencies = new List<Type>();
        private void Awake()
        {
            weapon = this.GetComponent<Weapon>();
        }
        private void Start()
        {
            GenerateWeapon(weaponData);
        }

        [ContextMenu("Test Generate")]
        private void TestGeneration()
        {
            GenerateWeapon(weaponData);
        }

        public void GenerateWeapon(WeaponDataSO data)
        {
            weapon.SetData(data);
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
    }
}
