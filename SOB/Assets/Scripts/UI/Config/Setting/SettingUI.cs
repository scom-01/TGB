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

        private void OnDisable()
        {
            if(DataManager.Inst != null)
            {
                DataManager.Inst.UserKeySettingSave();
            }
            else
            {
                Debug.LogWarning("UserKeySettint Save Fail");
            }
        }
    }
}
