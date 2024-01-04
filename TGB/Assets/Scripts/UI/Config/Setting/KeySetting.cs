using System.Collections.Generic;
using System.Linq;
using TGB.Manager;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputActionRebindingExtensions;

public class KeySetting : MonoBehaviour
{
    public string KeyName
    {
        set
        {
            keyName = value;
        }
    }
    [SerializeField] private InputActionReference m_Action = null;
    public List<InputActionReference> m_Actions = new List<InputActionReference>();
    private PlayerInputHandler m_PlayerInputHandler
    {
        get
        {
            return GameManager.Inst.InputHandler;
        }
    }

    [SerializeField] private TextMeshProUGUI CurrentKeyBtnNameTxt;
    [SerializeField] private TextMeshPro CurrentKeyBtnNameTxt_3D;
    [SerializeField] private string keyName;
    [SerializeField] private Canvas WaitforInputCanvas;
    
    private InputActionRebindingExtensions.RebindingOperation m_Rebind;
    private bool allCompositeParts
    {
        get
        {
            if(m_Action.action.bindings[m_BindingIndex].isComposite)
            {
                var firstPartIndex = m_BindingIndex + 1;
                if (firstPartIndex < m_Action.action.bindings.Count && m_Action.action.bindings[firstPartIndex].isPartOfComposite)
                    return true;
            }
            return false;
        }
    }
    private int m_BindingIndex;
    private SettingUI settingUI;
    [SerializeField] private bool isHold;
    private void OnEnable()
    {
        Debug.Log(keyName + " Enable");
        KeyName = keyName;

        if(settingUI == null)
            settingUI = GetComponentInParent<SettingUI>();

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
    public void UpdateDisplayText()
    {
        m_Rebind?.Dispose();

        if (WaitforInputCanvas != null) 
        {
            WaitforInputCanvas.enabled = false;
        }
        if (CurrentKeyBtnNameTxt != null)
        {
            CurrentKeyBtnNameTxt.text = InputControlPath.ToHumanReadableString(
            m_Action.action.bindings[m_BindingIndex].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice);
            if(CurrentKeyBtnNameTxt.text == "Up Arrow")
            {
                CurrentKeyBtnNameTxt.text = "↑";
            }
            else if(CurrentKeyBtnNameTxt.text == "Down Arrow")
            {
                CurrentKeyBtnNameTxt.text = "↓";
            }
            else if (CurrentKeyBtnNameTxt.text == "Left Arrow")
            {
                CurrentKeyBtnNameTxt.text = "←";
            }
            else if (CurrentKeyBtnNameTxt.text == "Right Arrow")
            {
                CurrentKeyBtnNameTxt.text = "→";
            }
            if (isHold) CurrentKeyBtnNameTxt.text += "(Hold)";
        }

        if (CurrentKeyBtnNameTxt_3D != null)
        {
            CurrentKeyBtnNameTxt_3D.text = InputControlPath.ToHumanReadableString(
            m_Action.action.bindings[m_BindingIndex].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice);
            if (CurrentKeyBtnNameTxt_3D.text == "Up Arrow")
            {
                CurrentKeyBtnNameTxt_3D.text = "↑";
            }
            else if (CurrentKeyBtnNameTxt_3D.text == "Down Arrow")
            {
                CurrentKeyBtnNameTxt_3D.text = "↓";
            }
            else if (CurrentKeyBtnNameTxt_3D.text == "Left Arrow")
            {
                CurrentKeyBtnNameTxt_3D.text = "←";
            }
            else if (CurrentKeyBtnNameTxt_3D.text == "Right Arrow")
            {
                CurrentKeyBtnNameTxt_3D.text = "→";
            }
            if (isHold) CurrentKeyBtnNameTxt_3D.text += "(Hold)";
        }
    }
    public void OnClickChange()
    {
        Debug.Log("OnClick = " + keyName);
        Debug.Log($"Press a {m_Action.action.expectedControlType}");
        m_Action.action.Dispose();
        m_PlayerInputHandler.SwitchActionMap(InputEnum.UI.ToString());
        m_Rebind = m_Action.action.PerformInteractiveRebinding()
        .WithTargetBinding(m_BindingIndex)
        .WithControlsExcluding("Mouse")
        .WithControlsExcluding("<Keyboard>/enter")      //enter => cancel
        .WithControlsExcluding("<Keyboard>/numpadEnter")      //enter => cancel
        .WithCancelingThrough("<Keyboard>/anyKey")
        .WithCancelingThrough("<Keyboard>/escape")      //ESC => cancel
        .OnMatchWaitForAnother(0.1f)
        .OnCancel(operaction =>
        {
            if (WaitforInputCanvas != null)
            {
                WaitforInputCanvas.enabled = false;
            }
            m_Action.action.Enable();
            operaction.Dispose();
        })
        .OnComplete(operaction =>
        {
            Debug.Log($"newbindKey = {operaction.action}");
            if (m_Action.action.bindings[m_BindingIndex].effectivePath == "<Keyboard>/anyKey")
            {
                Debug.Log("<keyboard>/anyKey");
                if (WaitforInputCanvas != null)
                {
                    WaitforInputCanvas.enabled = false;
                }
                m_Action.action.Enable();
                operaction.Dispose();
                return;
            }
            //DoRebind():중복 시 false
            if (!DoRebind(m_Action, m_BindingIndex, allCompositeParts))
            {
                m_Action.action.RemoveBindingOverride(m_BindingIndex);                
                CleanUp();
                if (WaitforInputCanvas != null)
                {
                    WaitforInputCanvas.enabled = false;
                }
                m_Action.action.Enable();
                operaction.Dispose();
                return;
            }
            UpdateDisplayText();
            for (int i = 0; i < m_Actions.Count; i++)
            {
                m_PlayerInputHandler.SwitchActionMap(m_Actions[i].action.actionMap.name);
                m_Actions[i].action.ChangeBinding(0).WithPath($"<{m_Action.action.controls[m_BindingIndex].parent.name}>/{m_Action.action.controls[m_BindingIndex].name}");
            }
            GameManager.Inst.InputHandler.ChangeCurrentActionMap(InputEnum.Cfg, false);
            m_Action.action.Enable();
            operaction.Dispose();
        })
        .Start();
    }

    private bool DoRebind(InputAction action, int bindingIndex, bool allCompositeParts = false)
    {
        InputBinding newBinding = action.bindings[bindingIndex];

        //액션 리스트에서 현재 입력한 액션은 제외
        var checkbindinglist = action.actionMap.bindings.ToList();
        checkbindinglist.Remove(newBinding);

        //액션리스트에서 중복확인
        foreach (InputBinding binding in checkbindinglist)
        {
            if(binding.action == newBinding.action)
            {
                Debug.Log("Duplicate binding found : " + newBinding.effectivePath);
                return false;
            }
            if(binding.effectivePath == newBinding.effectivePath)
            {
                Debug.Log("Duplicate binding found : " + newBinding.effectivePath);
                return false;
            }

            if(allCompositeParts)
            {
                for (int i = 0; i < bindingIndex; i++)
                {
                    if (action.bindings[i].effectivePath == newBinding.effectivePath)
                    {
                        Debug.Log("Duplicate binding found : " + newBinding.effectivePath);
                        return false;
                    }
                }
            }
        }
        return true;
    }    

    private void CleanUp()
    {
        m_Rebind?.Dispose();
        m_Rebind = null;
    }
}