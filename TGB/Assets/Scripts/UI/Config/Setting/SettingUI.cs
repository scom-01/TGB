using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SCOM.Manager
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
                foreach(var key in keySettings)
                {
                    key.UpdateDisplayText();
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

        public void ResetKeyRebind()
        {
            GameManager.Inst?.InputHandler.playerInput.actions.RemoveAllBindingOverrides();
            foreach(var keySetting in keySettings)
            {
                keySetting.UpdateDisplayText();
            }
            PlayerPrefs.DeleteKey(GlobalValue.RebindsKey);
        }
    }
}
