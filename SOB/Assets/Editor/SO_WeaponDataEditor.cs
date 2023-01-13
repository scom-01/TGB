using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.Compilation;
using UnityEngine;
using SOB.Weapons.Components;

namespace SOB.Weapons
{


    [CustomEditor(typeof(WeaponDataSO))]
    public class SO_WeaponDataEditor : Editor
    {

        private static List<Type> dataCompTypes = new List<Type>();
        private WeaponDataSO dataSO;

        private void OnEnable()
        {
            dataSO = target as WeaponDataSO;
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            foreach (var dataCompType in dataCompTypes)
            {
                if (GUILayout.Button(dataCompType.Name))
                {
                    var comp = Activator.CreateInstance(dataCompType) as ComponentData;

                    if (comp == null)
                    {
                        return;
                    }

                    dataSO.AddData(comp);
                }
            }
        }

        [DidReloadScripts]
        private static void OnRecompfile()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var types = assemblies.SelectMany(assembly => assembly.GetTypes());
            var filteredTypes = types.Where(type => type.IsSubclassOf(typeof(ComponentData)) && !type.ContainsGenericParameters && type.IsClass);
            dataCompTypes = filteredTypes.ToList();
        }
    }
}
