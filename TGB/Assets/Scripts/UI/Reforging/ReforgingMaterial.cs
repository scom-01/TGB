using System.Collections.Generic;
using TMPro;
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
    private int currentShardsAmount;
    private int reforgingCostShardsAmount;
    private Goods_Data currentGoodsAmount;
    private Goods_Data reforgingCostGoodsAmount;

    [SerializeField] private Transform GoodsTransform;
    [SerializeField] private List<GameObject> GoodsList;
    [SerializeField] private GameObject GoodsMaterial;
    [SerializeField] private LocalizeStringEvent NotEnoughMessage_Local;
    [SerializeField] private Animator NotEnoughMessage_Anim;

    public AudioSource audio = new AudioSource();
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
            DataManager.Inst.GameGoodsLoad();
        }

        SetRendering();
    }

    private void GetCurrentGoods()
    {
        if (DataManager.Inst != null)
        {
            currentGoodsAmount = DataManager.Inst.GameGoodsLoad();
        }
        else
        {
            currentGoldAmount = 0;
            currentShardsAmount = 0;
            currentGoodsAmount = new Goods_Data();
        }

        for (int i = 0; i < GoodsTransform.childCount; i++)
        {
            Destroy(GoodsTransform.GetChild(i).gameObject);
        }
        GoodsList.Clear();
    }
    private bool CheckReforging()
    {
        if (reforgingCostGoldAmount == -1 || reforgingCostShardsAmount == -1)
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
                reforgingCostShardsAmount = -1;
                reforgingCostGoodsAmount = new Goods_Data();
                return;
            }

            reforgingCostGoldAmount = ReforgingWeaponDataSO.WeaponClassLevel * 500;
            reforgingCostShardsAmount = ReforgingWeaponDataSO.WeaponClassLevel * 1000;
            reforgingCostGoodsAmount = ReforgingWeaponDataSO.GoodsCost;
            if (ReforgingWeaponDataSO.GoodsCost.Gold > 0)
            {
                var goods = Instantiate(GoodsMaterial, GoodsTransform);
                GoodsList.Add(goods);
                goods.GetComponent<GoodsMaterial>().UpdateGoodsMaterial(GOODS_TPYE.Gold, currentGoodsAmount.Gold, reforgingCostGoodsAmount.FireGoods);
            }
            if (ReforgingWeaponDataSO.GoodsCost.FireGoods > 0)
            {
                var goods = Instantiate(GoodsMaterial, GoodsTransform);
                GoodsList.Add(goods);
                goods.GetComponent<GoodsMaterial>().UpdateGoodsMaterial(GOODS_TPYE.FireGoods, currentGoodsAmount.FireGoods, reforgingCostGoodsAmount.FireGoods);
            }
            if (ReforgingWeaponDataSO.GoodsCost.WaterGoods > 0)
            {
                var goods = Instantiate(GoodsMaterial, GoodsTransform);
                GoodsList.Add(goods);
                goods.GetComponent<GoodsMaterial>().UpdateGoodsMaterial(GOODS_TPYE.WaterGoods, currentGoodsAmount.WaterGoods, reforgingCostGoodsAmount.WaterGoods);
            }
            if (ReforgingWeaponDataSO.GoodsCost.EarthGoods > 0)
            {
                var goods = Instantiate(GoodsMaterial, GoodsTransform);
                GoodsList.Add(goods);
                goods.GetComponent<GoodsMaterial>().UpdateGoodsMaterial(GOODS_TPYE.EarthGoods, currentGoodsAmount.EarthGoods, reforgingCostGoodsAmount.EarthGoods);
            }
            if (ReforgingWeaponDataSO.GoodsCost.WindGoods > 0)
            {
                var goods = Instantiate(GoodsMaterial, GoodsTransform);
                GoodsList.Add(goods);
                goods.GetComponent<GoodsMaterial>().UpdateGoodsMaterial(GOODS_TPYE.WindGoods, currentGoodsAmount.WindGoods, reforgingCostGoodsAmount.WindGoods);
            }
        }
    }

    public void ClearRendering()
    {
        GetCurrentGoods();

        reforgingCostShardsAmount = -1;
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
                DataManager.Inst.CalculateGoods(GoodsList[i].GetComponent<GoodsMaterial>().Type, -GoodsList[i].GetComponent<GoodsMaterial>().CostGoodsCount);                
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
            //재화 부족
            if (NotEnoughMessage_Anim != null && NotEnoughMessage_Local != null)
            {
                NotEnoughMessage_Local.StringReference.SetReference("Goods_Table", "Goods_NotEnough");
                NotEnoughMessage_Anim.Play("Action", -1, 0f);
            }
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