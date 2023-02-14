using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffPanelItem : MonoBehaviour
{
    public Buff buff;
    public Sprite IconImgSprite;
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
        BuffCurrentTime = 1f - ((Time.time - buff.startTime) / BuffDurationTime);
        FilledImg.fillAmount = BuffCurrentTime;
        if (BuffCurrentTime <= 0f)
        {
            Destroy(this.gameObject);
        }
    }

    [ContextMenu("Setting")]
    private void Setting()
    {
        if (IconImg != null)
        {
            IconImg.sprite = IconImgSprite;
        }

        if (BackImg != null)
        {
            BackImg.sprite = BackImgSprite;
        }
    }
}
