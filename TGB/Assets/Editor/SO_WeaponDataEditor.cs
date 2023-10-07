using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.Compilation;
using UnityEngine;
using TGB.Weapons.Components;

namespace TGB.Weapons
{
    [CustomEditor(typeof(WeaponDataSO))]
    public class SO_WeaponDataEditor : Editor
    {

        private static List<Type> dataCompTypes = new List<Type>();
        private WeaponDataSO dataSO;

        private bool showForceUpdateButtons;
        private bool showAddComponentButtons;

        private void OnEnable()
        {
            dataSO = target as WeaponDataSO;
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Setting Components"))
            {
                foreach (var dataCompType in dataCompTypes)
                {
                    var comp = Activator.CreateInstance(dataCompType) as ComponentData;

                    if (comp == null)
                    {
                        return;
                    }

                    comp.InitializeActionData(dataSO.NumberOfActions);

                    dataSO.AddData(comp);
                }
                    //foreach (var item in dataSO.ComponentData)
                    //{
                    //    item.InitializeActionData(dataSO.NumberOfActions);
                    //}
            }

            //showAddComponentButtons = EditorGUILayout.Foldout(showAddComponentButtons, "Add Components Buttons");            

            //if(showAddComponentButtons)
            //{
            //    foreach (var dataCompType in dataCompTypes)
            //    {
            //        if (GUILayout.Button(dataCompType.Name))
            //        {
            //            var comp = Activator.CreateInstance(dataCompType) as ComponentData;

            //            if (comp == null)
            //            {
            //                return;
            //            }

            //            comp.InitializeActionData(dataSO.NumberOfActions);

            //            dataSO.AddData(comp);
            //        }
            //    }
            //}

            showForceUpdateButtons = EditorGUILayout.Foldout(showForceUpdateButtons, "Force Update Buttons");
            if(showForceUpdateButtons)
            {
                if (GUILayout.Button("Force Update Component Names"))
                {
                    foreach(var item in dataSO.ComponentData)
                    {
                        item.SetComponentName();
                    }
                }
                if(GUILayout.Button("Force Update Attack Names"))
                {
                    foreach(var item in dataSO.ComponentData)
                    {
                        item.SetActionDataNames();
                    }
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
