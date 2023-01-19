using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInput playerInput;
    private Camera cam;
    public Vector2 RawMovementInput { get; private set; }
    public int NormInputX { get; private set; }
    public int NormInputY { get; private set; }
    [HideInInspector]
    public bool     JumpInput
                    , JumpInputStop
                    , GrabInput
                    , DashInput
                    , DashInputStop
                    , Skill1Input
                    , Skill1InputStop
                    , Skill2Input
                    , Skill2InputStop
                    , PrimaryInput
                    , PrimaryStop
                    , BlockInput
                    , BlockInputStop;
    [HideInInspector]
    public bool[] ActionInputs;

    [SerializeField]
    private float inputHoldTime = 0.2f;

    private float jumpInputStartTime;
    private float dashInputStartTime;
    private float skill1InputStartTime;
    private float skill2InputStartTime;
    private float blockInputStartTime;
    private float[] ActionInputsStartTime;


    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        int count = Enum.GetValues(typeof(CombatInputs)).Length;
        ActionInputs = new bool[count];
        ActionInputsStartTime = new float[count];
        cam = Camera.main;
    }

    private void Update()
    {
        bool jumpInput = JumpInput;
        bool dashInput = DashInput;
        bool blockInput = BlockInput;
        bool skill1Input = Skill1Input;
        bool skill2Input = Skill2Input;
        bool[] attackInputs = ActionInputs;

        CheckHoldTime(ref jumpInput, ref jumpInputStartTime);
        CheckHoldTime(ref dashInput, ref dashInputStartTime);
        CheckHoldTime(ref blockInput, ref blockInputStartTime);
        CheckHoldTime(ref skill1Input, ref skill1InputStartTime);
        CheckHoldTime(ref skill2Input, ref skill2InputStartTime);
        CheckHoldTime(ref attackInputs, ref ActionInputsStartTime);

        JumpInput = jumpInput;
        DashInput = dashInput;
        BlockInput = blockInput;
        Skill1Input = skill1Input;
        Skill2Input = skill2Input;
        ActionInputs = attackInputs;
    }

    //움직임 Input
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        RawMovementInput = context.ReadValue<Vector2>();

        NormInputX = Mathf.RoundToInt(RawMovementInput.x);
        NormInputY = Mathf.RoundToInt(RawMovementInput.y);

        //NormInputX = (int)(RawMovementInput * Vector2.right).normalized.x;
        //NormInputX = Mathf.Abs(RawMovementInput.x) > 0.3f ? (int)(RawMovementInput * Vector2.right).normalized.x : 0;
        //NormInputY = Mathf.Abs(RawMovementInput.x) > 0.3f ? (int)(RawMovementInput * Vector2.up).normalized.y : 0;        
        //NormInputY = (int)(RawMovementInput * Vector2.up).normalized.y;

        if (context.canceled)
        {
            NormInputX = 0;
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

    //Grab Input
    public void OnGrabInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            GrabInput = true;
        }

        if (context.canceled)
        {
            GrabInput = false;
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
    public void UseJumpInput() => JumpInput = false;
    public void UseDashInput() => DashInput = false;
    public void UseSkill1Input() => Skill1Input = false;
    public void UseSkill2Input() => Skill2Input = false;
    public void UseBlockInput() => BlockInput = false;

    public void UseInput(ref bool input) => input = false;

    //홀드 시간

    private void CheckHoldTime(ref bool input, ref float inputStartTime)
    {
        if (Time.time >= inputStartTime + inputHoldTime)
        {
            input = false;
        }
    }
    private void CheckHoldTime(ref bool[] input, ref float[] inputStartTime)
    {
        if (input.Length == 0)
            return;

        for(int i = 0; i< input.Length; i++)
        {
            if (Time.time >= inputStartTime[i] + inputHoldTime)
            {
                {
                    input[i] = false;  
                }
            }
        }
    }

    public enum CombatInputs
    {
        primary,
        secondary,
    }
}
