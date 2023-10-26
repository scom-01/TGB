using TMPro;
using UnityEngine;


public class CostText : MonoBehaviour
{
    [SerializeField] private InventoryItem item;
    private TMP_Text Txt;
    [SerializeField] private GOODS_TPYE goods_tpye;
    private int cost = 0;
    private string old_cost = "";

    // Start is called before the first frame update
    void Start()
    {
        Txt = this.GetComponent<TMP_Text>();
        Txt.text = "";
    }

    private void Update()
    {
        if (item?.StatsItemData == null)
        {
            Txt.text = "";
            return;
        }
    }
    private void OnEnable()
    {
        if (item != null)
        {
            item.OnChangeStatItemData -= UpdateCost;
            item.OnChangeStatItemData += UpdateCost;
        }
    }
    private void OnDisable()
    {
        if (item != null)
        {
            item.OnChangeStatItemData -= UpdateCost;
        }
    }
    private void UpdateCost()
    {
        Debug.Log("Enum = " + item?.StatsItemData?.itemData.ItemLevel);
        Debug.Log("Enum(int) = " + (int)item?.StatsItemData?.itemData.ItemLevel);
        cost = (int)item.StatsItemData.itemData.ItemLevel * GameManager.Inst.StageManager.StageLevel * GlobalValue.Gold_Inflation;
        Txt.text = string.Format("{0:#,###}", cost);
        switch (goods_tpye)
        {
            case GOODS_TPYE.Gold:
                Txt.text += "<color=yellow>G</color>";
                break;
            case GOODS_TPYE.FireGoods:
                Txt.text += "<color=red>G</color>";
                break;
            case GOODS_TPYE.WaterGoods:
                Txt.text += "<color=blue>W</color>";
                break;
            case GOODS_TPYE.EarthGoods:
                Txt.text += "<color=grey>E</color>";
                break;
            case GOODS_TPYE.WindGoods:
                Txt.text += "<color=white>Wind</color>";
                break;
        }
        old_cost = Txt.text;
    }
}
