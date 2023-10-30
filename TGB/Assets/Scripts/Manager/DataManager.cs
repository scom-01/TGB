using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TGB.CoreSystem;
using TGB.Weapons;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.Localization.Settings;

public class DataManager : MonoBehaviour
{
    public static DataManager Inst
    {
        get
        {
            if (_Inst == null)
            {
                _Inst = FindObjectOfType(typeof(DataManager)) as DataManager;
                if (_Inst == null)
                {
                    Debug.Log("no Singleton obj");
                }
                else
                {
                    DontDestroyOnLoad(_Inst.gameObject);
                }
            }
            return _Inst;
        }
    }

    private static DataManager _Inst = null;

    public DataParsing JSON_DataParsing
    {
        get
        {
            if (JsonDataParsing == null)
                JsonDataParsing = this.GetComponent<DataParsing>();
            return JsonDataParsing;
        }
    }
    private DataParsing JsonDataParsing;

    #region GameData Value
    [Header("DB")]
    public ItemDB All_ItemDB;
    public List<int> All_ItemDB_Idxs
    {
        get
        {
            if (all_ItemDB_Idxs.Count != All_ItemDB.ItemDBList.Count)
            {
                all_ItemDB_Idxs.Clear();
                for (int i = 0; i < All_ItemDB.ItemDBList.Count; i++)
                {
                    all_ItemDB_Idxs.Add(All_ItemDB.ItemDBList[i].ItemIdx);
                }
            }
            return all_ItemDB_Idxs;
        }
    }
    private List<int> all_ItemDB_Idxs = new List<int>();
    public WeaponDB All_WeaponDB;
    public BuffDB All_BuffDB;

    [HideInInspector] public List<StatsItemSO> UnlockItemList;
    [HideInInspector] public List<int> UnlockItemListidx;

    #endregion

    #region Setting
    public AudioMixerGroup BGM;
    public AudioMixerGroup SFX;
    [HideInInspector][Range(0.0f, 1.0f)] public float BGM_Volume;
    [HideInInspector][Range(0.0f, 1.0f)] public float SFX_Volume;

    [HideInInspector] public int localizationIdx;
    public LocalizationSettings localizationSettings;
    #endregion

    private void Awake()
    {
        if (_Inst)
        {
            var managers = Resources.FindObjectsOfTypeAll(typeof(DataManager));
            for (int i = 0; i < managers.Length; i++)
            {
                Debug.Log($"{managers[i]} = {i}");
                if (i > 0)
                {
                    Destroy(managers[i].GameObject());
                }
            }
            return;
        }

        //Localization LocalizeStringEvent의 Key값이 존재하지 않을 때 PrintWarning (즉, 빈 string 리턴)


        _Inst = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private void OnApplicationQuit()
    {
        UserKeySettingSave();
    }

    public void Init()
    {
    }

    #region Setting Func
    public void UserKeySettingLoad()
    {
        if (GameManager.Inst == null)
        {
            Debug.LogWarning("UserKeySetting Load Fails");
            return;
        }

        string rebinds = PlayerPrefs.GetString(GlobalValue.RebindsKey, string.Empty);
        if (string.IsNullOrEmpty(rebinds))
        {
            Debug.LogWarning("UserKeySetting Load Fails");
            return;
        }
        GameManager.Inst?.InputHandler.playerInput.actions.LoadBindingOverridesFromJson(rebinds);
        Debug.LogWarning("Load UserKeySetting Success");
    }
    public void UserKeySettingSave()
    {
        if (GameManager.Inst == null)
        {
            Debug.LogWarning("UserKeySetting Save Fails");
            return;
        }

        string rebinds = GameManager.Inst?.InputHandler.playerInput.actions.SaveBindingOverridesAsJson();
        PlayerPrefs.SetString(GlobalValue.RebindsKey, rebinds);
        Debug.LogWarning("Save UserKeySetting Success");
    }

    public void PlayerCfgSFXSave(float sfx)
    {
        SFX_Volume = sfx;
        PlayerPrefs.SetFloat(GlobalValue.SFX_Vol, SFX_Volume);
        //Debug.LogWarning("Success Cfg SFX Data Save");
    }
    public void PlayerCfgSFXLoad()
    {
        float sfx = PlayerPrefs.GetFloat(GlobalValue.SFX_Vol, 1.0f);
        SFX.audioMixer.SetFloat(GlobalValue.SFX_Vol, Mathf.Log10(sfx) * 20);
        SFX_Volume = sfx;
        //Debug.LogWarning($"Success Cfg SFX Data Load {SFX_Volume}");
    }
    public void PlayerCfgBGMSave(float bgm)
    {
        BGM_Volume = bgm;
        PlayerPrefs.SetFloat(GlobalValue.BGM_Vol, BGM_Volume);
        //Debug.LogWarning($"Success Cfg BGM Data Save {BGM_Volume}");
    }
    public void PlayerCfgBGMLoad()
    {
        float bgm = PlayerPrefs.GetFloat(GlobalValue.BGM_Vol, 1.0f);
        BGM.audioMixer.SetFloat(GlobalValue.BGM_Vol, Mathf.Log10(bgm) * 20);
        BGM_Volume = bgm;
        //Debug.LogWarning($"Success Cfg BGM Data Load {BGM_Volume}");
    }

    public void PlayerCfgQualityLoad()
    {
        int quality = PlayerPrefs.GetInt(GlobalValue.Quality, QualitySettings.names.Length);
        QualitySettings.SetQualityLevel(quality);
        //Debug.LogWarning("Success Cfg QualityLevel Data Load");
    }
    public void PlayerCfgQualitySave()
    {
        PlayerPrefs.SetInt(GlobalValue.Quality, QualitySettings.GetQualityLevel());
        //Debug.LogWarning("Success Cfg QualityLevel Data Save");
    }

    public void PlayerCfgLanguageLoad()
    {
        localizationIdx = PlayerPrefs.GetInt(GlobalValue.Language, 0);
        if (localizationIdx < 0)
        {
            localizationIdx = 0;
            PlayerPrefs.SetInt(GlobalValue.Language, localizationIdx);
        }
        StartCoroutine(ChangeRoutine(localizationIdx));
        //Debug.LogWarning("Success Cfg Language Data Load");
    }
    public void PlayerCfgLanguageSave()
    {
        PlayerPrefs.SetInt(GlobalValue.Language, localizationIdx);
        //Debug.LogWarning("Success Cfg Language Data Save");
    }

    IEnumerator ChangeRoutine(int index)
    {
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[index];
    }
    #endregion


    #region GameData

    public bool CheckJSONFile()
    {
        if (!JSON_DataParsing.FileCheck(JSON_DataParsing.SceneData_FilePath))
        {
            Debug.LogWarning($"{JSON_DataParsing.SceneData_FilePath} File is not found");
            return false;
        }
        return true;
    }

    public void DeleteJSONFile()
    {
        foreach (var item in GameManager.Inst.SubUI.InventorySubUI.InventoryItems.Items)
        {
            item.StatsItemData = null;
        }

        if (File.Exists(Application.dataPath + JSON_DataParsing.SceneData_FilePath))
        {
            try
            {
                File.Delete(Application.dataPath + JSON_DataParsing.SceneData_FilePath);
                JSON_DataParsing.m_JSON_SceneData = new DataParsing.JSON_SceneData();
            }
            catch (Exception e)
            {
                Console.WriteLine("The deletion failed: {0}", e.Message);
            }
        }
        else
        {
            Console.WriteLine("Specified file doesn't exist");
        }

    }

    public void DeleteDefaultFile()
    {
        if (File.Exists(Application.dataPath + JSON_DataParsing.DefaultData_FilePath))
        {
            try
            {
                File.Delete(Application.dataPath + JSON_DataParsing.DefaultData_FilePath);
                JSON_DataParsing.m_JSON_DefaultData = new DataParsing.JSON_DefaultData();
                JSON_DataParsing.LockItemList = new List<int>();
                if (GameManager.Inst != null)
                {
                    GameManager.Inst.GoTitleScene();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The deletion failed: {0}", e.Message);
            }
        }
        else
        {
            Console.WriteLine("Specified file doesn't exist");
        }
    }
    public void PlayerInventoryDataLoad(Inventory inventory)
    {
        var inventory_Itemlist = JSON_DataParsing.m_JSON_SceneData.Items.ToList();

        for (int i = 0; i < inventory_Itemlist.Count; i++)
        {
            if (All_ItemDB.ItemDBList[inventory_Itemlist[i]] == null)
            {
                continue;
            }
            inventory.AddInventoryItem(All_ItemDB.ItemDBList[inventory_Itemlist[i]]);
        }

        var inventory_Weaponlist = JSON_DataParsing.m_JSON_SceneData.Weapons;

        if (inventory_Weaponlist.Count > 0)
        {
            if (inventory.Weapon != null)
            {
                inventory.Weapon.SetCommandData(All_WeaponDB.WeaponDBList[inventory_Weaponlist[0]]);
            }
            else
            {
                Debug.LogWarning("Inventory.weapon is Null");
            }
        }
    }

    public void PlayerInventoryDataSave(Weapon weapon, List<ItemSet> itemList)
    {
        List<int> items = new List<int>();
        if (itemList == null)
        {
            JSON_DataParsing.m_JSON_SceneData.Items = items;
        }
        else
        {
            for (int i = 0; i < itemList.Count; i++)
            {
                items.Add(itemList[i].item.ItemIdx);
            }

            JSON_DataParsing.m_JSON_SceneData.Items = items;
        }


        List<int> weapons = new List<int>();
        if (weapon == null)
        {
            JSON_DataParsing.m_JSON_SceneData.Weapons = weapons;
        }
        else
        {
            weapons.Add(weapon.weaponData.weaponCommandDataSO.WeaponIdx);
            JSON_DataParsing.m_JSON_SceneData.Weapons = weapons;
        }

        Debug.LogWarning("Success Inventory Data Save");
    }

    public void PlayerUnlockItem()
    {
        //WaitUnlockItemIdxs = 해금 대기 아이템 인덱스 리스트
        for (int i = 0; i < JSON_DataParsing.m_JSON_DefaultData.WaitUnlockItemIdxs.Count; i++)
        {
            //해금 리스트에 해금 대기 아이템 인덱스 리스트의 아이템이 있으면 continue;
            if (JSON_DataParsing.m_JSON_DefaultData.UnlockItemIdxs.Contains(JSON_DataParsing.m_JSON_DefaultData.WaitUnlockItemIdxs[i]))
            {
                continue;
            }
            //해금 리스트에 해금 대기 아이템 인덱스 리스트의 아이템추가
            JSON_DataParsing.m_JSON_DefaultData.UnlockItemIdxs.Add(JSON_DataParsing.m_JSON_DefaultData.WaitUnlockItemIdxs[i]);
            //Title 해금 이펙트 리스트에 추가
            GameManager.Inst.EffectTextUI.UnlockitemNames.Add(All_ItemDB.ItemDBList[JSON_DataParsing.m_JSON_DefaultData.WaitUnlockItemIdxs[i]].itemData.ItemNameLocal);
        }
    }

    public void PlayerCurrHealthLoad(UnitStats stats)
    {
        if (JSON_DataParsing.m_JSON_SceneData.PlayerHealth == -1)
        {
            return;
        }
        stats.SetHealth(JSON_DataParsing.m_JSON_SceneData.PlayerHealth);
        Debug.LogWarning("Success CurrHealth Save");
    }
    public void PlayerCurrHealthSave(int m_playerHealth)
    {
        JSON_DataParsing.m_JSON_SceneData.PlayerHealth = m_playerHealth;
        Debug.LogWarning("Success CurrHealth Save");
    }
    public Goods_Data GameGoodsLoad()
    {
        return JSON_DataParsing.m_JSON_SceneData.Goods;
    }

    public void LoadScene()
    {
        if (JSON_DataParsing.Json_Read_SceneData() == null)
        {
            Debug.LogWarning("SceneName Load Fail");
            return;
        }

        Debug.LogWarning("Success SceneName Save");
    }
    public void SaveBuffs(List<Buff> _buffs)
    {
        if (_buffs == null)
            return;

        var _buffDatas = new List<BuffData>();
        for (int i = 0; i < _buffs.Count; i++)
        {
            var buff = new BuffData() { ButtItemIdx = _buffs[i].buffItemSO.ItemIdx, startTime = _buffs[i].startTime, CurrBuffCount = _buffs[i].CurrBuffCount };
            _buffDatas.Add(buff);
        }
        JSON_DataParsing.m_JSON_SceneData.BuffDatas = _buffDatas;

    }

    public void LoadBuffs(BuffSystem buffSystem)
    {
        var buffDatas = JSON_DataParsing.m_JSON_SceneData.BuffDatas;

        if (buffDatas == null)
            return;

        var buff = new List<Buff>();

        for (int i = 0; i < buffDatas.Count; i++)
        {
            var item = new Buff();
            item.buffItemSO = All_BuffDB.BuffDBList[buffDatas[i].ButtItemIdx];
            item.CurrBuffCount = buffDatas[i].CurrBuffCount;
            item.startTime = buffDatas[i].startTime;
            buff.Add(item);
        }

        buffSystem.buffs = buff;
        if (buffSystem.buffs != null)
        {
            buffSystem.SetBuff();
        }
    }

    public List<int> LoadSceneDataIdx()
    {
        return JSON_DataParsing.m_JSON_SceneData.SceneDataIdxs;
    }

    public void LoadPlayTime()
    {
        if (GameManager.Inst == null)
            return;

        GameManager.Inst.PlayTime = JSON_DataParsing.m_JSON_SceneData.PlayTime;
    }
    #endregion

    #region Data Controll Func

    /// <summary>
    /// Title = 2
    /// </summary>
    /// <param name="stageIndex"></param>
    public void NextStage(int stageIndex)
    {
        JSON_DataParsing.m_JSON_SceneData.SceneNumber = stageIndex;
    }

    public void CalculateGoods(GOODS_TPYE type, int goodsAmount)
    {
        switch (type)
        {
            case GOODS_TPYE.Gold:
                JSON_DataParsing.m_JSON_SceneData.Goods.Gold += goodsAmount;
                break;
            case GOODS_TPYE.FireGoods:
                JSON_DataParsing.m_JSON_SceneData.Goods.FireGoods += goodsAmount;
                break;
            case GOODS_TPYE.WaterGoods:
                JSON_DataParsing.m_JSON_SceneData.Goods.WaterGoods += goodsAmount;
                break;
            case GOODS_TPYE.EarthGoods:
                JSON_DataParsing.m_JSON_SceneData.Goods.EarthGoods += goodsAmount;
                break;
            case GOODS_TPYE.WindGoods:
                JSON_DataParsing.m_JSON_SceneData.Goods.WindGoods += goodsAmount;
                break;
            case GOODS_TPYE.HammerShards:
                JSON_DataParsing.m_JSON_DefaultData.hammer_piece += goodsAmount;
                break;
        }
        JSON_DataParsing.InvokeAction();
    }
    #endregion


    #region ItemDB Spawn

    public void SetLockItemList()
    {
        JSON_DataParsing.LockItemList.Clear();
        //전체 아이템 인덱스 리스트 스캔
        for (int i = 0; i < All_ItemDB.ItemDBList.Count; i++)
        {
            //해금 아이템 인덱스 리스트 중 All_ItemDB.ItemDBList[i].ItemIdx의 인덱스가 있으면 continue;
            if (JSON_DataParsing.m_JSON_DefaultData.UnlockItemIdxs.Contains(All_ItemDB.ItemDBList[i].ItemIdx))
            {
                continue;
            }
            //미해금 아이템 인덱스 리스트에 해금된 아이템을 제외한 아이템 인덱스 추가
            JSON_DataParsing.LockItemList.Add(All_ItemDB.ItemDBList[i].ItemIdx);
        }
    }
    /// <summary>
    /// 일정확률로 미해금 아이템 스폰
    /// </summary>
    /// <param name="_pos">아이템 스폰 위치</param>
    /// <param name="_percent">미해금 아이템 스폰 확률</param>
    /// <param name="_spawnAmount">스폰 할 아이템 수</param>
    /// <param name="_spawnInterval">아이템 스폰 위치 간격</param>
    public void RandomUnLockItemSpawn(Vector3 _pos, float _percent, int _spawnAmount, float _spawnInterval)
    {
        if (GameManager.Inst == null)
            return;

        if (GameManager.Inst.StageManager == null)
            return;

        //스폰한 아이템 리스트
        List<int> SpawnItemList = new List<int>();

        //아이템 스폰
        for (int i = 0; i < _spawnAmount; i++)
        {
            var pos = _pos + (Vector3.right * _spawnInterval * (i - ((_spawnAmount - 1) / 2)));
            int idx = 0;
            //새로운 아이템 언락
            if (JSON_DataParsing.m_JSON_SceneData.SceneDataIdxs[i] <= (GlobalValue.MaxSceneIdx * (_percent / 100f)))
            {
                //전체 미해금 아이템 인덱스 리스트
                List<int> All_LockItemIdxs = JSON_DataParsing.LockItemList.ToList();
                //이미 스폰한 아이템은 제외
                for (int j = 0; j < SpawnItemList.Count; j++)
                {
                    if (All_LockItemIdxs.Contains(SpawnItemList[j]))
                    {
                        All_LockItemIdxs.Remove(SpawnItemList[j]);
                    }
                }

                //필드 드랍아이템이 아닐 시 제외
                for (int j = 0; j < All_LockItemIdxs.Count; j++)
                {
                    if (All_ItemDB.ItemDBList[All_LockItemIdxs[j]].isFieldSpawn == false)
                    {
                        All_LockItemIdxs.RemoveAt(j);
                    }
                }

                idx = DataManager.Inst.JSON_DataParsing.m_JSON_SceneData.SceneDataIdxs[i] % ((All_LockItemIdxs.Count > 0) ? All_LockItemIdxs.Count : 0);
                //itemData = 전체 해금 아이템 중 보유하고 있지 않은 아이템 리스트 중 일부
                var itemData = All_ItemDB.ItemDBList[All_LockItemIdxs[idx]];

                if (itemData == null)
                    return;

                //아이템 스폰
                var item = GameManager.Inst.StageManager.SPM.SpawnItem(GameManager.Inst.StageManager.IM?.InventoryItem, pos, GameManager.Inst.StageManager.IM?.transform, itemData);
                if (item)
                {
                    //아이템 선택 매니저 아이템 리스트에 추가
                    if (GameManager.Inst.StageManager.ChoiceItemManager != null)
                        GameManager.Inst.StageManager.ChoiceItemManager.ItemList.Add(item);
                    //스폰한 아이템 리스트에 추가
                    SpawnItemList.Add(All_LockItemIdxs[idx]);
                    Debug.Log($"SpawnItem {itemData.name}");
                }
            }
            //기존 해금 아이템 중 스폰
            else
            {
                //전체 해금 아이템 인덱스 리스트
                List<int> All_UnlockItemIdxs = JSON_DataParsing.m_JSON_DefaultData.UnlockItemIdxs.ToList();

                //플레이어 인벤토리에 아이템이 존재할 때
                if (GameManager.Inst.StageManager.player.Inventory.Items.Count > 0)
                {
                    //itemidxs = 플레이어 인벤토리에 있는 아이템 인덱스 리스트
                    List<int> playeritemidxs = new List<int>();

                    for (int j = 0; j < GameManager.Inst.StageManager.player.Inventory.Items.Count; j++)
                    {
                        playeritemidxs.Add(GameManager.Inst.StageManager.player.Inventory.Items[j].item.ItemIdx);
                    }

                    //보유하고있지 않은 아이템 목록
                    //AllUnlockItemIdxs에서 플레이어가 소유하고 있는 아이템은 제외
                    for (int j = 0; j < playeritemidxs.Count; j++)
                    {
                        if (All_UnlockItemIdxs.Contains(playeritemidxs[j]))
                        {
                            All_UnlockItemIdxs.Remove(playeritemidxs[j]);
                        }
                    }
                }

                //이미 스폰한 아이템은 제외
                for (int j = 0; j < SpawnItemList.Count; j++)
                {
                    if (All_UnlockItemIdxs.Contains(SpawnItemList[j]))
                    {
                        All_UnlockItemIdxs.Remove(SpawnItemList[j]);
                    }
                }

                //필드 드랍아이템이 아닐 시 제외
                for (int j = 0; j < All_UnlockItemIdxs.Count; j++)
                {
                    if (All_ItemDB.ItemDBList[All_UnlockItemIdxs[j]].isFieldSpawn == false)
                    {
                        All_UnlockItemIdxs.RemoveAt(j);
                    }
                }

                //AllUnlockItemIdxs.Count = 전체 해금 아이템 인덱스 리스트 - 플레이어가 소유하고있는 아이템 인덱스 리스트
                //idx = 랜덤인덱스 % AllItemIdx.Count;
                idx = DataManager.Inst.JSON_DataParsing.m_JSON_SceneData.SceneDataIdxs[i] % ((All_UnlockItemIdxs.Count > 0) ? All_UnlockItemIdxs.Count : 0);
                //itemData = 전체 해금 아이템 중 보유하고 있지 않은 아이템 리스트 중 일부
                var itemData = All_ItemDB.ItemDBList[All_UnlockItemIdxs[idx]];
                if (itemData == null)
                    return;

                //아이템 스폰
                var item = GameManager.Inst.StageManager.SPM.SpawnItem(GameManager.Inst.StageManager?.IM?.InventoryItem, pos, GameManager.Inst.StageManager?.IM?.transform, itemData);
                if (item != null)
                {
                    //아이템 선택 매니저 아이템 리스트에 추가
                    if (GameManager.Inst.StageManager.ChoiceItemManager != null)
                        GameManager.Inst.StageManager.ChoiceItemManager.ItemList.Add(item);
                    //스폰한 아이템 리스트에 추가
                    SpawnItemList.Add(All_UnlockItemIdxs[idx]);
                    Debug.Log($"SpawnItem {itemData.name}");
                }
            }
        }
    }
    #endregion
}
