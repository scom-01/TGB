using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Blessing : InteractiveObject
{
    private Animator animator;
    private Canvas canvas
    {
        get
        {
            if(m_canvas == null)
            {
                m_canvas = this.GetComponentInChildren<Canvas>();
            }
            return m_canvas;
        }
    }
    private Canvas m_canvas;
    protected override void Start()
    {
        base.Start();
        animator = this.GetComponentInChildren<Animator>();
    }

    public override void Interactive()
    {
        if (GlobalValue.ContainParam(animator, "Action"))
        {
            Debug.Log("Open");
            animator.SetBool("Action", true);
        }
        GameManager.Inst.InputHandler.ChangeCurrentActionMap(InputEnum.UI, true, true);
        canvas.enabled = true;
    }

    public override void UnInteractive()
    {
        GameManager.Inst.InputHandler.ChangeCurrentActionMap(InputEnum.GamePlay, true);
        canvas.enabled = false;
    }
}