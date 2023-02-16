using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffPanelItem : MonoBehaviour
{
    public Buff buff;
    [SerializeField] private Sprite BackImgSprite;

    [SerializeField] private Image IconImg;
    [SerializeField] private Image BackImg;
    [SerializeField] private Image FilledImg;

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

        BuffDurationTime = buff.durationTime;
        BuffCurrentTime = ((Time.time - buff.startTime) / BuffDurationTime);
        FilledImg.fillAmount = BuffCurrentTime;
        if (BuffCurrentTime >= 1f)
        {
            Destroy(this.gameObject);
        }
    }

    [ContextMenu("Setting")]
    public void Setting()
    {
        if (IconImg != null)
        {
            IconImg.sprite = buff.buffSprite;
        }

        if (BackImg != null)
        {
            BackImg.sprite = BackImgSprite;
        }
    }
}
