using UnityEngine;
using UnityEngine.EventSystems;

public class Merchant : InteractiveObject
{
    private Animator animator;
    public InventoryItems Inventoryitems;
    public InventoryItems Shopitems;
    private Canvas canvas
    {
        get
        {
            if (m_canvas == null)
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
    public override void Start_Action()
    {
        if (Inventoryitems != null)
        {
            GameManager.Inst.SetSelectedObject(Inventoryitems?.Items[0].gameObject);            
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

        //Set item StatsItem
        if (GameManager.Inst.StageManager?.player == null)
        {
            return;
        }

        //Inventory Item Setup
        for (int i = 0; i < Inventoryitems.Items.Count; i++)
        {
            if (i < GameManager.Inst.StageManager?.player.Inventory.Items.Count)
            {
                Inventoryitems.Items[i].StatsItemData = GameManager.Inst.StageManager?.player.Inventory.Items[i].item;
            }
            else
            {
                Inventoryitems.Items[i].StatsItemData = null;
            }
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
