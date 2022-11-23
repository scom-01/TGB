using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 RawMovementInput { get; private set; }
    public int NormInputX { get; private set; } 
    public int NormInputY { get; private set; }
    public bool JumpInput { get; private set; }
    public bool JumpInputStop { get; private set; }
    public bool GrabInput { get; private set; }
    public bool DashInput { get; private set; }
    public bool DashInputStop { get; private set; }

    public bool Skill1Input { get; private set; }
    public bool Skill2Input { get; private set; }


    [SerializeField]
    private float inputHoldTime = 0.2f;

    private float jumpInputStartTime;
    private float dashInputStartTime;

    private void Update()
    {
        CheckJumpInputHoldTime();
        CheckDashInputHoldTime();
    }

    //움직임 Input
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        RawMovementInput = context.ReadValue<Vector2>();
        NormInputX = (int)(RawMovementInput * Vector2.right).normalized.x;
        //NormInputX = Mathf.Abs(RawMovementInput.x) > 0.3f ? (int)(RawMovementInput * Vector2.right).normalized.x : 0;
        //NormInputY = Mathf.Abs(RawMovementInput.x) > 0.3f ? (int)(RawMovementInput * Vector2.up).normalized.y : 0;        
        NormInputY = (int)(RawMovementInput * Vector2.up).normalized.y;        
    }

    //점프 Input
    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            JumpInput = true;
            JumpInputStop = false;
            jumpInputStartTime = Time.time;
        }

        if(context.canceled)
        {
            JumpInputStop = true;
        }
    }

    //Grab Input
    public void OnGrabInput(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            GrabInput = true;
        }

        if(context.canceled)
        {
            GrabInput = false;
        }
    }

    public void OnDashInput(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            DashInput = true;
            DashInputStop = false;
            dashInputStartTime = Time.time;
        }
        else if(context.canceled)
        {
            DashInputStop = true;
        }
    }

    public void OnSkill1Input(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            Debug.Log("OnSkill 1 Input");
            Debug.Log(context.action.bindings);
            Skill1Input = true;
        }
    }

    public void OnSkill2Input(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("OnSkill 2 Input");
            Debug.Log(context.action.bindings);
            Skill2Input = true;
        }
    }
    public void UseJumpInput() => JumpInput = false;
    public void UseDashInput() => DashInput = false;
    public void UseSkill1Input() => Skill1Input = false;
    public void UseSkill2Input() => Skill2Input = false;
    //점프 홀드 시간
    private void CheckJumpInputHoldTime()
    {
        if(Time.time >= jumpInputStartTime + inputHoldTime)
        {
            JumpInput = false;
        }
    }
    
    private void CheckDashInputHoldTime()
    {
        if(Time.time >= dashInputStartTime + inputHoldTime)
        {
            DashInput = false;
        }
    }
}
