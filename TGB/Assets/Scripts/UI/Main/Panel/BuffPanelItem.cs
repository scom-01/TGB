using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuffPanelItem : MonoBehaviour
{
    public Buff buff;
    public BuffItemSO buffItem;
    [SerializeField] private Sprite BackImgSprite;

    [SerializeField] private Image IconImg;
    [SerializeField] private Image BackImg;
    [SerializeField] private Image FilledImg;
    [SerializeField] private TextMeshProUGUI CountText;
    /// <summary>
    /// 버프 지속 남은 시간
    /// </summary>
    private float BuffCurrentTime;
    // Start is called before the first frame update
    void Start()
    {
        Setting();
    }
    private void Update()
    {
        if (buff == null)
            return;
        if (GameManager.Inst == null)
            return;

        if (buff.CurrBuffCount > 0)
        {
            CountText.text = buff.CurrBuffCount == 1 ? "" : buff.CurrBuffCount.ToString();
            if (buff.buffItemSO.BuffData.BuffType == EVENT_BUFF_TYPE.Passive)
            {
                BuffCurrentTime = 0f;
            }
            else
            {
                BuffCurrentTime = ((GameManager.Inst.PlayTime - buff.startTime) / buff.buffItemSO.BuffData.DurationTime);
            }
            FilledImg.fillAmount = BuffCurrentTime;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    [ContextMenu("Setting")]
    public void Setting()
    {
        if (IconImg != null)
        {
            IconImg.sprite = buff.buffItemSO.itemData.ItemSprite;
        }

        if (BackImg != null)
        {
            BackImg.sprite = BackImgSprite;
        }
    }
}
