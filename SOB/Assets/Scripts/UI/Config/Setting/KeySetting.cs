using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeySetting : MonoBehaviour
{
    public string KeyName
    {
        set
        {
            keyName = value;
            if (KeyNameTxt != null)
            {
                KeyNameTxt.text = keyName;
            }
        }
    }

    [SerializeField] private TextMeshProUGUI KeyNameTxt;
    [SerializeField] private TextMeshProUGUI CurrentKeyBtnNameTxt;
    [SerializeField] private string keyName;
    public InputAction KeyBtn;
    private string currentKeyBtnName;

    private void Start()
    {
        KeyName = keyName;
        CurrentKeyBtnNameTxt.text = KeyBtn.GetBindingDisplayString();
    }

    public void OnClickChange()
    {
        Debug.Log("OnClick = " + keyName);
        Debug.LogWarning("InputAction = " + KeyBtn.GetBindingDisplayString());
    }
}