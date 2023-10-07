using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.EventSystems;

public class PlayerInputHandler : MonoBehaviour
{
    public PlayerInput playerInput
    {
        get => GetComponent<PlayerInput>();
    }
    PlayerInputActions playerInputActions;
    private Camera cam;
    public Vector2 RawMovementInput { get; private set; }
    public int NormInputX { get; private set; }
    public int NormInputY { get; private set; }
    [HideInInspector]
    public bool JumpInput
                    , DashInput
                    , Skill1Input
                    , Skill2Input
                    , PrimaryInput
                    , ESCInput
                    , InteractionInput = false;
    [HideInInspector]
    public bool PrimaryInputStop
                    , JumpInputStop
                    , DashInputStop
                    , Skill1InputStop
                    , Skill2InputStop
                    , InteractionInputStop = true;
    [HideInInspector]
    public bool[] ActionInputs;

    [SerializeField]
    private float inputHoldTime = 0.2f;

    [HideInInspector]
    public float jumpInputStartTime,
                    dashInputStartTime,
                    skill1InputStartTime,
                    skill2InputStartTime,
                    escInputStartTime,
                    interactionInputStartTime = -1;
    private float[] ActionInputsStartTime;
    private float[] ActionInputsStopTime;


    public event Action OnESCInput_Action;
    private void Awake()
    {
        Init();

        //Debug.Log("This InputHandler ActionMap Name : " + playerInput.currentActionMap.name);
    }

    private void Init()
    {
        if (playerInputActions == null)
            playerInputActions = new PlayerInputActions();
        int count = Enum.GetValues(typeof(CombatInputs)).Length;

        JumpInput = false;
        DashInput = false;
        Skill1Input = false;
        Skill2Input = false;
        PrimaryInput = false;
        ESCInput = false;
        InteractionInput = false;

        PrimaryInputStop = true;
        JumpInputStop = true;
        DashInputStop = true;
        Skill1InputStop = true;
        Skill2InputStop = true;
        InteractionInputStop = true;

        ActionInputs = new bool[count];
        ActionInputsStartTime = new float[count];
        ActionInputsStopTime = new float[count];

        jumpInputStartTime = -1;
        dashInputStartTime = -1;
        skill1InputStartTime = -1;
        skill2InputStartTime = -1;
        escInputStartTime = -1;
        interactionInputStartTime = -1;

        if (cam == null)
            cam = Camera.main;
    }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {

    }

    /// <summary>
    /// Change ActionMap
    /// </summary>
    /// <param name="str">ActionMap Name</param>
    public void SwitchActionMap(string Name)
    {
        playerInput.SwitchCurrentActionMap(Name);
    }

    private void FixedUpdate()
    {
        bool jumpInput = JumpInput;
        bool dashInput = DashInput;
        bool skill1Input = Skill1Input;
        bool skill2Input = Skill2Input;
        bool interacInput = InteractionInput;
        bool[] attackInputs = ActionInputs;

        CheckHoldTime(ref jumpInput, ref jumpInputStartTime);
        CheckHoldTime(ref dashInput, ref dashInputStartTime);
        CheckHoldTime(ref skill1Input, ref skill1InputStartTime);
        CheckHoldTime(ref skill2Input, ref skill2InputStartTime);
        //CheckHoldTime(ref attackInputs, ref ActionInputsStartTime);
        CheckHoldTime(ref interacInput, ref interactionInputStartTime);

        JumpInput = jumpInput;
        DashInput = dashInput;
        Skill1Input = skill1Input;
        Skill2Input = skill2Input;
        //ActionInputs = attackInputs;
        InteractionInput = interacInput;
    }

    #region GamePlay
    //움직임 Input
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        RawMovementInput = context.ReadValue<Vector2>();

        NormInputX = Mathf.RoundToInt(RawMovementInput.x);
        NormInputY = Mathf.RoundToInt(RawMovementInput.y);

        if (context.canceled)
        {
            NormInputX = 0;
            NormInputY = 0;
        }
    }


    //점프 Input
    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            JumpInput = true;
            JumpInputStop = false;
            jumpInputStartTime = Time.time;
        }

        if (context.canceled)
        {
            JumpInputStop = true;
        }
    }
    public void OnDashInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            DashInput = true;
            DashInputStop = false;
            dashInputStartTime = Time.time;
        }
        else if (context.canceled)
        {
            DashInputStop = true;
        }
    }

    public void OnSkill1Input(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("OnSkill 1 Input");
            Skill1Input = true;
            Skill1InputStop = false;
        }

        if (context.canceled)
        {
            Skill1InputStop = true;
        }
    }

    public void OnSkill2Input(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("OnSkill 2 Input");
            Skill2Input = true;
            Skill2InputStop = false;
        }

        if (context.canceled)
        {
            Skill2InputStop = true;
        }
    }

    public void OnPrimaryInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            ActionInputs[(int)CombatInputs.primary] = true;
            ActionInputsStartTime[(int)CombatInputs.primary] = Time.time;
        }

        if (context.canceled)
        {
            ActionInputsStopTime[(int)CombatInputs.primary] = Time.time;
            ActionInputs[(int)CombatInputs.primary] = false;
        }
    }

    public void OnSecondaryInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            ActionInputs[(int)CombatInputs.secondary] = true;
            ActionInputsStartTime[(int)CombatInputs.secondary] = Time.time;
        }

        if (context.canceled)
        {
            ActionInputs[(int)CombatInputs.secondary] = false;
        }
    }

    public void OnInteractionInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            InteractionInput = true;
            InteractionInputStop = false;
            interactionInputStartTime = Time.time;
        }

        if (context.canceled)
        {
            InteractionInput = false;
            InteractionInputStop = true;
        }
    }
    #endregion

    #region UI
    public void OnTapInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (GameManager.Inst.StageManager == null)
                return;

            if (OnESCInput_Action != null)
            {
                OnESCInput_Action?.Invoke();
                OnESCInput_Action = null;
                return;
            }

            Debug.Log("OnTapInput Start");
            if (playerInput.actions.actionMaps.ToArray().Length > 0)
            {
                if (playerInput.currentActionMap == playerInput.actions.FindActionMap(InputEnum.UI.ToString()))
                {
                    ChangeCurrentActionMap(InputEnum.GamePlay, true);
                }
                else if (playerInput.currentActionMap == playerInput.actions.FindActionMap(InputEnum.GamePlay.ToString()))
                {
                    ChangeCurrentActionMap(InputEnum.UI, true);
                }
            }
        }
        if (context.canceled)
        {
            Debug.Log("OnTapInput Cancled");
        }
    }
    public void OnESCInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (OnESCInput_Action != null)
            {
                OnESCInput_Action?.Invoke();
                OnESCInput_Action = null;
                return;
            }
            ESCInput = true;
            escInputStartTime = Time.time;
            Debug.Log("OnESCInput Start");

            if (GameManager.Inst?.StageManager != null)
            {
                if (!GameManager.Inst.StageManager.player.IsAlive)
                {
                    return;
                }
            }
            else
            {
                var _TitleManager = FindObjectOfType(typeof(TitleManager)) as TitleManager;
                if (_TitleManager != null)
                {
                    if(_TitleManager.UnlockItem_Canvas.GetComponent<Canvas>()?.enabled == true)
                    {                        
                        if (_TitleManager.buttons.Count > 0)
                        {
                            EventSystem.current.SetSelectedGameObject(_TitleManager.buttons[0].gameObject);
                        }
                        _TitleManager.UnlockItem_Canvas.GetComponent<Canvas>().enabled = false;
                        return;
                    }
                }
            }

            if (playerInput.actions.actionMaps.ToArray().Length > 0)
            {
                //로딩중엔 return
                if (GameManager.Inst.Curr_UIState == UI_State.Loading)
                    return;

                if (playerInput.currentActionMap == playerInput.actions.FindActionMap(InputEnum.UI.ToString()))
                {
                    ChangeCurrentActionMap(InputEnum.GamePlay, true);
                }
                else if (playerInput.currentActionMap == playerInput.actions.FindActionMap(InputEnum.GamePlay.ToString()))
                {
                    ChangeCurrentActionMap(InputEnum.Cfg, true);
                }
                else if (playerInput.currentActionMap == playerInput.actions.FindActionMap(InputEnum.Cfg.ToString()))
                {
                    if (GameManager.Inst.StageManager == null)
                    {
                        ChangeCurrentActionMap(InputEnum.Cfg, true);
                    }
                    else
                    {
                        foreach (var btn in GameManager.Inst.CfgUI.ConfigPanelUI.cfgBtns)
                        {
                            btn.OnClickActiveUI(false);
                        }
                        ChangeCurrentActionMap(InputEnum.GamePlay, true);
                    }
                }
            }
        }
        if (context.canceled)
        {
            Debug.Log("OnESCInput Cancled");
            ESCInput = false;
        }
    }
    #endregion

    /// <summary>
    /// Pause가 true이면 현재 isPause상태를 변경하고자 함
    /// </summary>
    /// <param name="inputEnum"></param>
    /// <param name="Pause"></param>
    public void ChangeCurrentActionMap(InputEnum inputEnum, bool Pause, bool Init = false)
    {
        //현재와 동일한 ActionMap으로 변경하려하면 ActionMap변경을 원치않으므로 Pause기능만 하도록
        if (playerInput.currentActionMap == playerInput.actions.FindActionMap(inputEnum.ToString()))
        {
            if (inputEnum == InputEnum.GamePlay)
            {
                return;
            }

            if (Pause)
            {
                GameManager.Inst.CheckPause(inputEnum, Init);
            }
        }
        else
        {
            playerInput.SwitchCurrentActionMap(inputEnum.ToString());

            if (Pause)
            {
                GameManager.Inst.CheckPause(inputEnum, Init);
            }
        }
    }

    public void UseInput(ref bool input) => input = false;

    //홀드 시간

    private void CheckHoldTime(ref bool input, ref float inputStartTime)
    {
        if (Time.time >= inputStartTime + inputHoldTime)
        {
            input = false;
        }
    }
    private void CheckHoldTime(ref int input, ref float inputStartTime)
    {
        if (Time.time >= inputStartTime + inputHoldTime)
        {
            input = 0;
        }
    }
    private void CheckHoldTime(ref float input, ref float inputStartTime)
    {
        if (Time.time >= inputStartTime + inputHoldTime)
        {
            input = 0.0f;
        }
    }
    private void CheckHoldTime(ref bool[] input, ref float[] inputStartTime)
    {
        if (input.Length == 0)
            return;

        for (int i = 0; i < input.Length; i++)
        {
            if (Time.time >= inputStartTime[i] + inputHoldTime)
            {
                {
                    input[i] = false;
                }
            }
        }
    }
}

public enum CombatInputs
{
    primary,
    secondary,
}