using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;


public class WeaponMiniPanel : MonoBehaviour
{
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
                    //e_powerImg.sprite = 
                    break;
                case E_Power.Water:
                    break;
                case E_Power.Earth:
                    break;
                case E_Power.Wind:
                    break;
                case E_Power.Fire:
                    break;
            }

        }

        if (WeaponImg != null)
            WeaponImg.sprite = weaponCommandDataSO.WeaponImg;
    }
}
