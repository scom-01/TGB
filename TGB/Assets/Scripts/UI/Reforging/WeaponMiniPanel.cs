using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;


public class WeaponMiniPanel : MonoBehaviour
{
    #region Symbol Icon
    private Sprite[] SpriteAtlas
    {
        get
        {
            if (spriteAtlas.Length == 0)
            {
                spriteAtlas = Resources.LoadAll<Sprite>(GlobalValue.Sprites_UI_Path + "/Symbol_Sheet");
            }
            return spriteAtlas;
        }
    }

    private Sprite[] spriteAtlas = { };

    private Sprite NormalIcon
    {
        get
        {
            if (normalIcon == null)
            {
                foreach (var sprite in SpriteAtlas)
                {
                    if (sprite.name == GlobalValue.Symbol_Normal_Path)
                    {
                        normalIcon = sprite;
                        return normalIcon;
                    }
                }
                normalIcon = Resources.Load<Sprite>(GlobalValue.Sprites_UI_Path + "/" + GlobalValue.Symbol_Normal_Path);
            }
            return normalIcon;
        }
    }

    private Sprite normalIcon;

    private Sprite FireIcon
    {
        get
        {
            if (fireIcon == null)
            {
                foreach (var sprite in SpriteAtlas)
                {
                    if (sprite.name == GlobalValue.Symbol_Fire_Path)
                    {
                        fireIcon = sprite;
                        return fireIcon;
                    }
                }
                fireIcon = Resources.Load<Sprite>(GlobalValue.Sprites_UI_Path + "/" + GlobalValue.Symbol_Fire_Path);
            }
            return fireIcon;
        }
    }
    private Sprite fireIcon;
    private Sprite WaterIcon
    {
        get
        {
            if (waterIcon == null)
            {
                foreach (var sprite in SpriteAtlas)
                {
                    if (sprite.name == GlobalValue.Symbol_Water_Path)
                    {
                        waterIcon = sprite;
                        return waterIcon;
                    }
                }
                waterIcon = Resources.Load<Sprite>(GlobalValue.Sprites_UI_Path + "/" + GlobalValue.Symbol_Water_Path);
            }
            return waterIcon;
        }
    }
    private Sprite waterIcon;
    private Sprite EarthIcon
    {
        get
        {
            if (earthIcon == null)
            {
                foreach (var sprite in SpriteAtlas)
                {
                    if (sprite.name == GlobalValue.Symbol_Earth_Path)
                    {
                        earthIcon = sprite;
                        return earthIcon;
                    }
                }
                earthIcon = Resources.Load<Sprite>(GlobalValue.Sprites_UI_Path + "/" + GlobalValue.Symbol_Earth_Path);
            }
            return earthIcon;
        }
    }
    private Sprite earthIcon;
    private Sprite WindIcon
    {
        get
        {
            if (windIcon == null)
            {
                foreach (var sprite in SpriteAtlas)
                {
                    if (sprite.name == GlobalValue.Symbol_Wind_Path)
                    {
                        windIcon = sprite;
                        return windIcon;
                    }
                }
                windIcon = Resources.Load<Sprite>(GlobalValue.Sprites_UI_Path + "/" + GlobalValue.Symbol_Wind_Path);
            }
            return windIcon;
        }
    }
    private Sprite windIcon;
    #endregion
    [Header("Data")]
    public WeaponCommandDataSO weaponCommandDataSO;

    [HideInInspector] public Button Btn
    { 
        get
        {
            if(_btn == null)
            {
                _btn = GetComponent<Button>();
            }
            return _btn;
        }
    }
    private Button _btn;

    [Header("Setting")]
    [Tooltip("속성 이미지")]
    [SerializeField] protected Image Symbol_Img;
    [Tooltip("무기 등급 레벨")]
    [SerializeField] protected TextMeshProUGUI ClassLevel;
    [Tooltip("무기 이미지")]
    [SerializeField] protected Image WeaponImg;

    public Canvas Canvas
    {
        get
        {
            if (canvas == null)
            {
                canvas = GetComponent<Canvas>();
            }
            return canvas;
        }
    }
    private Canvas canvas;

    protected E_Power e_power;

    protected virtual void OnEnable()
    {
        SetRendering();
    }

    public void SetWeaponCommandPanel(WeaponCommandPanel panel)
    {
        panel.SetWeaponData(this.weaponCommandDataSO);
    }

    public void SetReforgigngMaterial(ReforgingMaterial reforgingMaterial)
    {
        if (this.weaponCommandDataSO == null)
        {
            Debug.LogWarning($"{this.name} WeaponCommandDataSO is Null");
            return;
        }
        reforgingMaterial.SetReforgingMaterial(this.weaponCommandDataSO);
    }

    public virtual void SetWeaponCommandData(WeaponCommandDataSO dataSO)
    {
        weaponCommandDataSO = dataSO;
        SetRendering();
    }

    private void SetRendering()
    {
        Canvas.enabled = weaponCommandDataSO != null ? true : false;
        Btn.enabled = weaponCommandDataSO != null ? true : false;

        if (weaponCommandDataSO == null)
        {
            return;
        }

        if (ClassLevel != null)
        {
            ClassLevel.text = weaponCommandDataSO.WeaponClassLevel >= 5 ? weaponCommandDataSO.WeaponClassLevel.ToString() : "Max";
        }

        e_power = weaponCommandDataSO.Elelemental_Power;
        if (Symbol_Img != null)
        {
            switch (e_power)
            {
                case E_Power.Normal:
                    Symbol_Img.sprite = NormalIcon;
                    break;
                case E_Power.Water:
                    Symbol_Img.sprite = WaterIcon;
                    break;
                case E_Power.Earth:
                    Symbol_Img.sprite = EarthIcon;
                    break;
                case E_Power.Wind:
                    Symbol_Img.sprite = WindIcon;
                    break;
                case E_Power.Fire:
                    Symbol_Img.sprite = FireIcon;
                    break;
            }
        }

        if (WeaponImg != null)
            WeaponImg.sprite = weaponCommandDataSO.WeaponImg;
    }
}
