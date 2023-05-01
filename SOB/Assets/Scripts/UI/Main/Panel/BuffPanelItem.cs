using System.Collections;
using System.Collections.Generic;
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
    /// 버프 총 지속 시간
    /// </summary>
    [HideInInspector] public float BuffDurationTime;
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

        BuffDurationTime = buff.buffItem.BuffData.DurationTime;
        BuffCurrentTime = ((Time.time - buff.startTime) / BuffDurationTime);
        FilledImg.fillAmount = BuffCurrentTime;
        if(buff.CurrBuffCount <= 1)
        {
            CountText.text = "";
        }
        else
        {
            CountText.text = buff.CurrBuffCount.ToString();
        }

        if (BuffCurrentTime >= 1f)
        {
            if(GameManager.Inst != null && GameManager.Inst.StageManager != null)
            {   
                if (GameManager.Inst.StageManager.player.GetComponent<BuffSystem>().buffItems.Contains(buffItem))
                {
                    GameManager.Inst.StageManager.player.GetComponent<BuffSystem>().buffItems.Remove(buffItem);
                }           
            }
            Destroy(this.gameObject);
        }
    }

    [ContextMenu("Setting")]
    public void Setting()
    {
        if (IconImg != null)
        {
            IconImg.sprite = buff.buffItem.ItemSprite;
        }

        if (BackImg != null)
        {
            BackImg.sprite = BackImgSprite;
        }
    }
}
