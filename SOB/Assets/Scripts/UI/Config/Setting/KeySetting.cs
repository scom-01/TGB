using SOB.Manager;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputActionRebindingExtensions;
using static UnityEngine.InputSystem.InputBindingCompositeContext;
using static UnityEngine.Rendering.DebugUI.Table;

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
    [SerializeField] private InputActionReference m_Action = null;
    private PlayerInputHandler m_PlayerInputHandler
    {
        get
        {
            return GameManager.Inst.inputHandler;
        }
    }

    [SerializeField] private TextMeshProUGUI KeyNameTxt;
    [SerializeField] private TextMeshProUGUI CurrentKeyBtnNameTxt;
    [SerializeField] private string keyName;

    private InputActionRebindingExtensions.RebindingOperation m_Rebind;
    private int m_BindingIndex;
    private Button btn;

    private void OnEnable()
    {
        Debug.Log(keyName + " Enable");
        KeyName = keyName;
        if (m_Action.action.type == InputActionType.Value)
        {
            m_BindingIndex = m_Action.action.ChangeBinding("WASD").NextPartBinding(keyName).bindingIndex;
        }
        else
        {
            m_BindingIndex = m_Action.action.GetBindingIndex();
        }

        UpdateDisplayText();
    }
    private void OnDisable()
    {
        m_Rebind?.Dispose();
    }
    private void UpdateDisplayText()
    {
        m_Rebind?.Dispose();

        CurrentKeyBtnNameTxt.text = InputControlPath.ToHumanReadableString(
            m_Action.action.bindings[m_BindingIndex].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice);
    }

    private void Awake()
    {
        btn = this.GetComponentInChildren<Button>();
        //KeyName = keyName;
        //string rebinds = PlayerPrefs.GetString(RebindsKey, string.Empty);
        //if (string.IsNullOrEmpty(rebinds))
        //    return;
        //m_PlayerInputHandler.playerInput.actions.LoadBindingOverridesFromJson(rebinds);
        //UpdateDisplayText();
    }

    public void OnClickChange()
    {
        Debug.Log("OnClick = " + keyName);
        m_PlayerInputHandler.SwitchActionMap("UI");
        m_Rebind = m_Action.action.PerformInteractiveRebinding()
        .WithTargetBinding(m_BindingIndex)
        .WithControlsExcluding("Mouse")
        .OnMatchWaitForAnother(0.1f)
        .OnComplete(_ => UpdateDisplayText())
        .Start();
        m_PlayerInputHandler.SwitchActionMap("GamePlay");
    }    
}