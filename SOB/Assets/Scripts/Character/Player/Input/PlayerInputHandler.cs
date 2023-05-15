using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public PlayerInput playerInput
    {
        get => GetComponent<PlayerInput>();
    }
    PlayerInputActions playerInputActions;
    private InputActionMap oldInputActionMap;
    private InputAction MyAction;
    private Camera cam;
    public Vector2 RawMovementInput { get; private set; }
    public int NormInputX { get; private set; }
    public int NormInputY { get; private set; }
    [HideInInspector]
    public bool JumpInput
                    , JumpInputStop
                    , DashInput
                    , DashInputStop
                    , Skill1Input
                    , Skill1InputStop
                    , Skill2Input
                    , Skill2InputStop
                    , PrimaryInput
                    , PrimaryStop
                    , BlockInput
                    , BlockInputStop
                    , InteractionInput = false;

    [HideInInspector]
    public bool[] ActionInputs;

    [SerializeField]
    private float inputHoldTime = 0.2f;

    private float jumpInputStartTime;
    private float dashInputStartTime;
    private float skill1InputStartTime;
    private float skill2InputStartTime;
    private float blockInputStartTime;
    private float interactionInputStartTime;
    private float[] ActionInputsStartTime;
    private float[] ActionInputsStopTime;

    private void Awake()
    {
        if (playerInputActions == null)
            playerInputActions = new PlayerInputActions();
        MyAction = playerInputActions.GamePlay.Primary;
        int count = Enum.GetValues(typeof(CombatInputs)).Length;
        ActionInputs = new bool[count];
        ActionInputsStartTime = new float[count];
        ActionInputsStopTime = new float[count];

        if (cam == null)
            cam = Camera.main;

        //Debug.Log("This InputHandler ActionMap Name : " + playerInput.currentActionMap.name);
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

    private void Update()
    {
        bool jumpInput = JumpInput;
        bool dashInput = DashInput;
        bool blockInput = BlockInput;
        bool skill1Input = Skill1Input;
        bool skill2Input = Skill2Input;
        bool interacInput = InteractionInput;
        bool[] attackInputs = ActionInputs;

        CheckHoldTime(ref jumpInput, ref jumpInputStartTime);
        CheckHoldTime(ref dashInput, ref dashInputStartTime);
        CheckHoldTime(ref blockInput, ref blockInputStartTime);
        CheckHoldTime(ref skill1Input, ref skill1InputStartTime);
        CheckHoldTime(ref skill2Input, ref skill2InputStartTime);
        //CheckHoldTime(ref attackInputs, ref ActionInputsStartTime);
        CheckHoldTime(ref interacInput, ref interactionInputStartTime);

        JumpInput = jumpInput;
        DashInput = dashInput;
        BlockInput = blockInput;
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
            interactionInputStartTime = Time.time;
        }

        if (context.canceled)
        {
            InteractionInput = false;
        }
    }

    public void OnBlockInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            BlockInput = true;
            BlockInputStop = false;
            blockInputStartTime = Time.time;
        }
        else if (context.canceled)
        {
            BlockInputStop = true;
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

            Debug.Log("OnTapInput Start");
            if (playerInput.actions.actionMaps.ToArray().Length > 0)
            {
                if (playerInput.currentActionMap == playerInput.actions.FindActionMap("UI"))
                {
                    ChangeCurrentActionMap("GamePlay", false);
                }
                else if (playerInput.currentActionMap == playerInput.actions.FindActionMap("GamePlay"))
                {
                    GameManager.Inst.SubUI.InventorySubUI.PutInventoryItem();
                    ChangeCurrentActionMap("UI", true);
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
            Debug.Log("OnESCInput Start");
            if (playerInput.actions.actionMaps.ToArray().Length > 0)
            {
                if (playerInput.currentActionMap == playerInput.actions.FindActionMap("UI"))
                {
                    ChangeCurrentActionMap("GamePlay", false);
                }
                else if (playerInput.currentActionMap == playerInput.actions.FindActionMap("GamePlay"))
                {
                    ChangeCurrentActionMap("Cfg", true);
                }
                else if (playerInput.currentActionMap == playerInput.actions.FindActionMap("Cfg"))
                {
                    if (GameManager.Inst.StageManager == null)
                    {
                        ChangeCurrentActionMap("Cfg", true);
                    }
                    else
                    {
                        foreach (var btn in GameManager.Inst.CfgUI.ConfigPanelUI.cfgBtns)
                        {
                            btn.OnClickActiveUI(false);
                        }
                        ChangeCurrentActionMap("GamePlay", false);
                    }
                }
                oldInputActionMap = playerInput.currentActionMap;
            }
        }
        if (context.canceled)
        {
            Debug.Log("OnTapInput Cancled");
        }
    }
    #endregion

    private void OnGUI()
    {
    }

    public void ChangeCurrentActionMap(string actionMapName, bool Pause)
    {
        //현재와 동일한 ActionMap으로 변경하려하면 ActionMap변경을 원치않으므로 Pause기능만 하도록
        if (playerInput.currentActionMap == playerInput.actions.FindActionMap(actionMapName))
        {            
            if(actionMapName == "GamePlay")
            {
                return;
            }
            GameManager.Inst.CheckPause(actionMapName);
        }
        else
        {
            oldInputActionMap = playerInput.currentActionMap;
            playerInput.SwitchCurrentActionMap(actionMapName);
            GameManager.Inst.CheckPause(actionMapName, Pause);
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