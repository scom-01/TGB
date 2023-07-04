using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Localization.Components;

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

    private int currentGoldAmount;
    private int reforgingCostGoldAmount;
    private int currentSculptureAmount;
    private int reforgingCostSculptureAmount;
    private ElementalGoods currentElementalGoodsAmount;
    private ElementalGoods reforgingCostElementalGoodsAmount;

    [SerializeField] private Transform GoodsTransform;
    [SerializeField] private List<GameObject> GoodsList;
    [SerializeField] private GameObject GoodsMaterial;

    public AudioSource audio;
    private AudioClip ReforgingSuccessClip
    {
        get
        {
            if(reforgingSuccessClip==null)
            {
                reforgingSuccessClip = Resources.Load<AudioClip>(GlobalValue.Sounds_UI_Path + GlobalValue.Reforging_Success);
            }
            return reforgingSuccessClip;
        }
    }
    private AudioClip reforgingSuccessClip;
    private AudioClip ReforgingFailureClip
    {
        get
        {
            if (reforgingFailureClip == null)
            {
                reforgingFailureClip = Resources.Load<AudioClip>(GlobalValue.Sounds_UI_Path + GlobalValue.Reforging_Failure);
            }
            return reforgingFailureClip;
        }
    }
    private AudioClip reforgingFailureClip;

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
            currentElementalGoodsAmount = DataManager.Inst.ElementalGoodsCount;
        }
        else
        {
            currentGoldAmount = 0;
            currentSculptureAmount = 0;
            currentElementalGoodsAmount = new ElementalGoods();
        }

        for (int i = 0; i < GoodsTransform.childCount; i++)
        {
            Destroy(GoodsTransform.GetChild(i).gameObject);
        }
        GoodsList.Clear();
    }
    private bool CheckReforging()
    {
        if (reforgingCostGoldAmount == -1 || reforgingCostSculptureAmount == -1)
        {
            Debug.Log("There is impossible reforging weapon");
            return false;
        }

        if(GoodsList.Count == 0)
        {

            return false;
        }

        for (int i = 0; i < GoodsList.Count; i++)
        {
            if (!GoodsList[i].GetComponent<GoodsMaterial>().CanReforging)
            {
                return false;
            }
        }
        return true;
    }
    public void SetRendering()
    {
        GetCurrentGoods();

        if (ReforgingWeaponDataSO == null)
        {
            if (WeaponName != null)
                WeaponName.text = "";
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
                reforgingCostElementalGoodsAmount = new ElementalGoods();
                return;
            }

            reforgingCostGoldAmount = ReforgingWeaponDataSO.WeaponClassLevel * 500;
            reforgingCostSculptureAmount = ReforgingWeaponDataSO.WeaponClassLevel * 1000;
            reforgingCostElementalGoodsAmount = ReforgingWeaponDataSO.elementalgoods;
            if (reforgingCostGoldAmount > 0)
            {
                var goods = Instantiate(GoodsMaterial, GoodsTransform);
                GoodsList.Add(goods);
                goods.gameObject.GetComponent<GoodsMaterial>().UpdateGoodsMaterial(GOODS_TPYE.Gold, currentGoldAmount, reforgingCostGoldAmount);
            }
            if (ReforgingWeaponDataSO.elementalgoods.FireGoods > 0)
            {
                var goods = Instantiate(GoodsMaterial, GoodsTransform);
                GoodsList.Add(goods);
                goods.gameObject.GetComponent<GoodsMaterial>().UpdateGoodsMaterial(GOODS_TPYE.FireGoods, currentElementalGoodsAmount.FireGoods, reforgingCostElementalGoodsAmount.FireGoods);
            }
            if (ReforgingWeaponDataSO.elementalgoods.WaterGoods > 0)
            {
                var goods = Instantiate(GoodsMaterial, GoodsTransform);
                GoodsList.Add(goods);
                goods.gameObject.GetComponent<GoodsMaterial>().UpdateGoodsMaterial(GOODS_TPYE.WaterGoods, currentElementalGoodsAmount.WaterGoods, reforgingCostElementalGoodsAmount.WaterGoods);
            }
            if (ReforgingWeaponDataSO.elementalgoods.EarthGoods > 0)
            {
                var goods = Instantiate(GoodsMaterial, GoodsTransform);
                GoodsList.Add(goods);
                goods.gameObject.GetComponent<GoodsMaterial>().UpdateGoodsMaterial(GOODS_TPYE.EarthGoods, currentElementalGoodsAmount.EarthGoods, reforgingCostElementalGoodsAmount.EarthGoods);
            }
            if (ReforgingWeaponDataSO.elementalgoods.WindGoods > 0)
            {
                var goods = Instantiate(GoodsMaterial, GoodsTransform);
                GoodsList.Add(goods);
                goods.gameObject.GetComponent<GoodsMaterial>().UpdateGoodsMaterial(GOODS_TPYE.WindGoods, currentElementalGoodsAmount.WindGoods, reforgingCostElementalGoodsAmount.WindGoods);
            }
        }
    }

    public void ClearRendering()
    {
        GetCurrentGoods();

        reforgingCostGoldAmount = -1;
        reforgingCostSculptureAmount = -1;

    }
    public void SetReforgingMaterial(WeaponCommandDataSO data)
    {
        ReforgingWeaponDataSO = data;
        SetRendering();
    }

    /// <summary>
    /// ReforgingBtn EventTrigger
    /// </summary>
    /// <param name="equip"></param>
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

            if (DataManager.Inst == null)
            {
                Debug.Log("DataManager is Null");
                return;
            }

            for(int i = 0; i < GoodsList.Count; i++)
            {
                DataManager.Inst.DecreseGoods(GoodsList[i].GetComponent<GoodsMaterial>().Type, GoodsList[i].GetComponent<GoodsMaterial>().CostGoodsCount);                
            }

            equip.SetWeaponCommandData(ReforgingWeaponDataSO);

            if (audio != null && ReforgingSuccessClip != null)
            {
                audio.clip = ReforgingSuccessClip;
                audio.loop = false;
                audio.playOnAwake = false;
                audio.Play();
            }

            GameManager.Inst.StageManager.player.Inventory.Weapon.SetCommandData(ReforgingWeaponDataSO);
            GameManager.Inst.StageManager.player.Inventory.weaponData = GameManager.Inst.StageManager.player.Inventory.Weapon.weaponData;
        }
        else
        {
            Debug.Log("There is not enough goods");
            if (audio != null && ReforgingFailureClip != null)
            {
                audio.clip = ReforgingFailureClip;
                audio.loop = false;
                audio.playOnAwake = false;
                audio.Play();
            }
        }
    }
}