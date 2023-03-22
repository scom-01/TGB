using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputActionRebindingExtensions;
using static UnityEngine.Rendering.DebugUI.Table;

public class KeySetting : MonoBehaviour
{
    public InputAction m_Action;
    public int m_BindingIndex;    
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
    private bool isChangeKey;

    private void OnEnable()
    {
        m_BindingIndex = KeyBtn.GetBindingIndex();
        UpdateDisplayText();
    }
    private void OnDisable()
    {
        m_Rebind?.Dispose();
    }
    private void UpdateDisplayText()
    {
        m_Rebind?.Dispose();
        //CurrentKeyBtnNameTxt.text = m_Action.GetBindingDisplayString(m_BindingIndex);
        CurrentKeyBtnNameTxt.text = KeyBtn.GetBindingDisplayString();
    }
    private InputActionRebindingExtensions.RebindingOperation m_Rebind;
    private void Start()
    {
        KeyName = keyName;
        CurrentKeyBtnNameTxt.text = KeyBtn.GetBindingDisplayString();
    }

    public void OnClickChange()
    {
        Debug.Log("OnClick = " + keyName);
        m_Rebind = KeyBtn.PerformInteractiveRebinding()
            .WithControlsExcluding("Mouse")
            .OnMatchWaitForAnother(0.1f)
            .WithTargetBinding(m_BindingIndex)
            .OnComplete(_ => UpdateDisplayText())
            .Start();
        Debug.LogWarning("ChangeInput = " + KeyBtn.GetBindingDisplayString());
        //KeyBtn.Reset();
        //KeyBtn.PerformInteractiveRebinding()
        //        //// To avoid accidental input from mouse motion
        //        //.WithControlsExcluding("Mouse")
        //        //.OnMatchWaitForAnother(0.1f)
        //        .Start();
        //CurrentKeyBtnNameTxt.text = KeyBtn.GetBindingDisplayString();
        //isChangeKey = true;
    }

    private void OnGUI()
    {
        if (!isChangeKey)
            return;
        
    }
}