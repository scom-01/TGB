using UnityEngine;
using UnityEngine.EventSystems;

public class Merchant : InteractiveObject
{
    private Animator animator;
    [SerializeField] private InventoryItems items;
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
        if (items != null)
        {
            EventSystem.current.SetSelectedGameObject(items?.Items[0].gameObject);
            if (GameManager.Inst.StageManager?.player == null)
            {
                return;
            }

            for (int i = 0; i < items.Items.Count; i++)
            {
                if (i < GameManager.Inst.StageManager?.player.Inventory.Items.Count)
                {
                    items.Items[i].StatsItemData = GameManager.Inst.StageManager?.player.Inventory.Items[i].item;
                }
                else
                {
                    items.Items[i].StatsItemData = null;
                }
            }
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
        GameManager.Inst.InputHandler.OnESCInput_Action -= End_Action;
        GameManager.Inst.InputHandler.OnESCInput_Action += End_Action;
    }
    public override void UnInteractive()
    {
        GameManager.Inst.InputHandler.ChangeCurrentActionMap(InputEnum.GamePlay, true);
        canvas.enabled = false;
    }
}
