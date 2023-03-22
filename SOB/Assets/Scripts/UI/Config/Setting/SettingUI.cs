using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SOB.Manager
{
    public class SettingUI : MonoBehaviour
    {
        private KeySetting[] keySettings;
        
        void Start()
        {
            keySettings = GetComponentsInChildren<KeySetting>();
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
