using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffPanelItem : MonoBehaviour
{
    public Sprite IconImgSprite;
    [SerializeField] private Sprite BackImgSprite;

    [SerializeField] private Image IconImg;
    [SerializeField] private Image IconImgFilled;
    [SerializeField] private Image BackImg;
    [SerializeField] private Image BackImgFilled;

    /// <summary>
    /// 버프 총 지속 시간
    /// </summary>
    [HideInInspector] public float BuffDurationTime;
    /// <summary>
    /// 버프 지속 남은 시간
    /// </summary>
    public float BuffCurrentTime
    {
        get => buffCurrentTime;
        set
        {
            buffCurrentTime = value;
            IconImgFilled.fillAmount = buffCurrentTime / BuffDurationTime;
            BackImgFilled.fillAmount = buffCurrentTime / BuffDurationTime;
        }
    }
    private float buffCurrentTime;

    // Start is called before the first frame update
    void Start()
    {
        Setting();
    }

    [ContextMenu("Setting")]
    private void Setting()
    {
        if(IconImgFilled ==null || BackImgFilled == null)
        {
            Debug.LogError($"IconImgFilled or BackImgFilled is Null");
            return;
        }
        
        if (IconImgFilled != null)
        {
            IconImgFilled.sprite = IconImgSprite;
        }

        if (IconImg != null)
        {
            IconImg.sprite = IconImgSprite;
        }

        if (BackImg != null)
        {
            BackImg.sprite = BackImgSprite;
        }

        if (BackImgFilled != null)
        {
            BackImgFilled.sprite = BackImgSprite;
        }
    }
}
