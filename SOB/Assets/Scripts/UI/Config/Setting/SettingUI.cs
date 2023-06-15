using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SOB.Manager
{
    public class SettingUI : MonoBehaviour
    {
        public Canvas Canvas
        {
            get
            {
                if (canvas == null)
                {
                    canvas = GetComponent<Canvas>();
                }
                return canvas;
            }
        }
        private Canvas canvas;
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
                Debug.LogWarning("UserKeySetting Save Fail");
            }
        }

        public void ResetKeyRebind()
        {
            GameManager.Inst?.inputHandler.playerInput.actions.RemoveAllBindingOverrides();
            foreach(var keySetting in keySettings)
            {
                keySetting.UpdateDisplayText();
            }
            PlayerPrefs.DeleteKey(GlobalValue.RebindsKey);
        }
    }
}
