using SOB.Weapons;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Events;
using static UnityEngine.Rendering.DebugUI;

public class ReforgingMaterial : MonoBehaviour
{
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

    [Tooltip("장착 무기")]
    [SerializeField] private EquipWeapon EquipWeaon;
    [Tooltip("재련 무기")]
    [SerializeField] private WeaponCommandDataSO ReforgingWeaponDataSO;

    [Tooltip("선택된 무기 이름")]
    [SerializeField] private TextMeshProUGUI WeaponName;
    [Tooltip("현재 골드")]
    [SerializeField] private TextMeshProUGUI CurrentGoldAmountText;
    [Tooltip("현재 원소 조각")]
    [SerializeField] private TextMeshProUGUI ReforgingCostGoldAmountText;
    [Tooltip("재련 필요 골드 ")]
    [SerializeField] private TextMeshProUGUI CurrentSculptureAmountText;
    [Tooltip("재련 필요 원소 조각")]
    [SerializeField] private TextMeshProUGUI ReforgingCostSculptureAmountText;

    [SerializeField] private Color enoughColor;
    [SerializeField] private Color ShortageColor;

    private int currentGoldAmount;
    private int reforgingCostGoldAmount;
    private int currentSculptureAmount;
    private int reforgingCostSculptureAmount;

    private void OnEnable()
    {
        if (DataManager.Inst != null)
        {
            DataManager.Inst.GameGoldLoad();
            DataManager.Inst.GameElementalsculptureLoad();
        }

        SetRendering();
    }
    
    private void GetCurrentGoods()
    {
        if (DataManager.Inst != null)
        {
            currentGoldAmount = DataManager.Inst.GoldCount;
            currentSculptureAmount = DataManager.Inst.ElementalsculptureCount;
        }
        else
        {
            currentGoldAmount = 0;
            currentSculptureAmount = 0;
        }

        if (CurrentGoldAmountText != null)
            CurrentGoldAmountText.text = string.Format("{0:#,###}", currentGoldAmount);

        if (CurrentSculptureAmountText != null)
            CurrentSculptureAmountText.text = string.Format("{0:#,###}", currentSculptureAmount);

    }
    private bool CheckReforging()
    {
        if (reforgingCostGoldAmount == -1 || reforgingCostSculptureAmount == -1)
        {
            Debug.Log("There is impossible reforging weapon");
            return false;
        }

        if (currentGoldAmount > reforgingCostGoldAmount && currentSculptureAmount > reforgingCostSculptureAmount)
        {            
            return true;
        }

        return false;
    }
    public void SetRendering()
    {
        GetCurrentGoods();

        if (ReforgingWeaponDataSO == null)
        {            
            if (WeaponName != null)
                WeaponName.text = "";

            if (ReforgingCostGoldAmountText != null)
            {
                ReforgingCostGoldAmountText.text = "";
            }
            if (ReforgingCostSculptureAmountText != null)
            {
                ReforgingCostSculptureAmountText.text = "";
            }
        }
        else
        {
            if (WeaponName != null)
            {
                WeaponName.text = ReforgingWeaponDataSO.WeaponName;
                var local = WeaponName.GetComponent<LocalizeStringEvent>();
                if (local) 
                {
                    if (ReforgingWeaponDataSO.WeaponNameLocal != null)
                    {
                        local.StringReference = ReforgingWeaponDataSO.WeaponNameLocal;
                    }
                }
                else
                {
                    Debug.Log($"{WeaponName.text} have not Localize String Event");
                }
            }

            if (EquipWeaon == null || EquipWeaon.weaponCommandDataSO == null || EquipWeaon.weaponCommandDataSO == ReforgingWeaponDataSO)
            {
                reforgingCostGoldAmount = -1;
                reforgingCostSculptureAmount = -1;

                if (ReforgingCostGoldAmountText != null)
                {
                    ReforgingCostGoldAmountText.text = "";
                }
                if (ReforgingCostSculptureAmountText != null)
                {
                    ReforgingCostSculptureAmountText.text = "";
                }

                return;
            }

            reforgingCostGoldAmount = ReforgingWeaponDataSO.WeaponClassLevel * 500;
            reforgingCostSculptureAmount = ReforgingWeaponDataSO.WeaponClassLevel * 1000;

            if (ReforgingCostGoldAmountText != null)
            {
                ReforgingCostGoldAmountText.color = currentGoldAmount < reforgingCostGoldAmount ? ShortageColor : enoughColor;
                ReforgingCostGoldAmountText.text = string.Format("{0:#,###}", reforgingCostGoldAmount);
            }
            if (ReforgingCostSculptureAmountText != null)
            {
                ReforgingCostSculptureAmountText.color = currentSculptureAmount < reforgingCostSculptureAmount ? ShortageColor : enoughColor;
                ReforgingCostSculptureAmountText.text = string.Format("{0:#,###}", reforgingCostSculptureAmount);
            }
        }
    }

    public void ClearRendering()
    {
        GetCurrentGoods();

        reforgingCostGoldAmount = -1;
        reforgingCostSculptureAmount = -1;

        if (ReforgingCostGoldAmountText != null)
        {
            ReforgingCostGoldAmountText.text = "";
        }
        if (ReforgingCostSculptureAmountText != null)
        {
            ReforgingCostSculptureAmountText.text = "";
        }
    }
    public void SetReforgingMaterial(WeaponCommandDataSO data)
    {
        ReforgingWeaponDataSO = data;
        SetRendering();
    }

    public void Reforging(EquipWeapon equip)
    {
        if (equip == null)
            return;
        
        if (ReforgingWeaponDataSO == null)
            return;

        if (CheckReforging())
        {
            if (GameManager.Inst.StageManager == null)
            {
                Debug.Log("StageManager is Null");
                return;
            }

            currentGoldAmount -= reforgingCostGoldAmount;
            currentSculptureAmount -= reforgingCostSculptureAmount;
            equip.SetWeaponCommandData(ReforgingWeaponDataSO);
            GameManager.Inst.StageManager?.player.Inventory.Weapon.SetCommandData(ReforgingWeaponDataSO);
        }
        else
        {
            Debug.Log("There is not enough goods");
        }
    }
}
