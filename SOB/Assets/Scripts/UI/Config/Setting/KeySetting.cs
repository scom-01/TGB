using SOB.Manager;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputActionRebindingExtensions;

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
    public List<InputActionReference> m_Actions = new List<InputActionReference>();
    private PlayerInputHandler m_PlayerInputHandler
    {
        get
        {
            return GameManager.Inst.inputHandler;
        }
    }

    [SerializeField] private TextMeshProUGUI KeyNameTxt;
    [SerializeField] private TextMeshProUGUI CurrentKeyBtnNameTxt;
    [SerializeField] private TextMeshPro CurrentKeyBtnNameTxt_3D;
    [SerializeField] private string keyName;
    
    private InputActionRebindingExtensions.RebindingOperation m_Rebind;
    private int m_BindingIndex;
    private SettingUI settingUI;
    private void OnEnable()
    {
        Debug.Log(keyName + " Enable");
        KeyName = keyName;
        settingUI = GetComponentInParent<SettingUI>();

        for (int i = 0; i < m_Actions.Count; i++) 
        {
            if (m_Actions[i].action.type == InputActionType.Value)
            {
                m_BindingIndex = m_Actions[i].action.ChangeBinding("WASD").NextPartBinding(keyName).bindingIndex;
            }
            else
            {
                m_BindingIndex = m_Actions[i].action.GetBindingIndex();
            }
        }
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

        for (int i = 0; i < m_Actions.Count; i++)
        {
            if (CurrentKeyBtnNameTxt != null)
            {
                CurrentKeyBtnNameTxt.text = InputControlPath.ToHumanReadableString(
                m_Actions[i].action.bindings[m_BindingIndex].effectivePath,
                InputControlPath.HumanReadableStringOptions.OmitDevice);
            }

            if (CurrentKeyBtnNameTxt_3D != null)
            {
                CurrentKeyBtnNameTxt_3D.text = InputControlPath.ToHumanReadableString(
                m_Actions[i].action.bindings[m_BindingIndex].effectivePath,
                InputControlPath.HumanReadableStringOptions.OmitDevice);
            }

        }
            if (CurrentKeyBtnNameTxt != null)
        {
            CurrentKeyBtnNameTxt.text = InputControlPath.ToHumanReadableString(
            m_Action.action.bindings[m_BindingIndex].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice);
        }

        if (CurrentKeyBtnNameTxt_3D != null)
        {
            CurrentKeyBtnNameTxt_3D.text = InputControlPath.ToHumanReadableString(
            m_Action.action.bindings[m_BindingIndex].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice);
        }
    }

    private void Awake()
    {
    }

    public void OnClickChange()
    {
        Debug.Log("OnClick = " + keyName);
        m_PlayerInputHandler.SwitchActionMap("UI");
        if (settingUI != null)
        {
            settingUI.waitforinputText.gameObject.SetActive(true);
        }
        //for (int i = 0; i < m_Actions.Count; i++)
        //{
        //    m_PlayerInputHandler.SwitchActionMap(m_Actions[i].action.actionMap.name);
        //    m_Rebind = m_Actions[i].action.PerformInteractiveRebinding()
        //    .WithTargetBinding(m_BindingIndex)
        //    .WithControlsExcluding("Mouse")
        //    .OnMatchWaitForAnother(0.1f)
        //    .OnComplete(_ => UpdateDisplayText())
        //    .Start();
        //}
        m_Rebind = m_Action.action.PerformInteractiveRebinding()
        .WithTargetBinding(m_BindingIndex)
        .WithControlsExcluding("Mouse")
        .OnMatchWaitForAnother(0.1f)
        .OnComplete(_ => UpdateDisplayText())
        .Start();
        if (settingUI != null)
        {
            settingUI.waitforinputText.gameObject.SetActive(false);
        }
        m_PlayerInputHandler.SwitchActionMap("GamePlay");
    }
}