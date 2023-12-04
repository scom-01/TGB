using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Blessing : InteractiveObject
{
    private Animator animator;
    [SerializeField] private List<BlessingStats> BlessingStats;

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
        BlessingStats = this.GetComponentsInChildren<BlessingStats>().ToList();
    }
    public override void Start_Action()
    {
        if (BlessingStats.Count > 0)
        {
            GameManager.Inst.SetSelectedObject(BlessingStats[0].gameObject);
        }
    }

    public override void End_Action()
    {
        if (GlobalValue.ContainParam(animator, "Action"))
        {
            Debug.Log("Close");
            animator.SetBool("Action", false);
        }
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
        GameManager.Inst.InputHandler.OnESCInput_Action.Add(End_Action);
    }

    public override void UnInteractive()
    {
        GameManager.Inst.InputHandler.ChangeCurrentActionMap(InputEnum.GamePlay, true);
        canvas.enabled = false;
    }
}