using SOB.CoreSystem;
using SOB.Weapons;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Experimental.AI;
using UnityEngine.InputSystem;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

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
            if (all_ItemDB_Idxs.Count == 0)
            {
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
        Debug.LogWarning("Success Cfg SFX Data Save");
    }
    public void PlayerCfgSFXLoad()
    {
        float sfx = PlayerPrefs.GetFloat(GlobalValue.SFX_Vol, 1.0f);
        SFX.audioMixer.SetFloat(GlobalValue.SFX_Vol, Mathf.Log10(sfx) * 20);
        SFX_Volume = sfx;
        Debug.LogWarning($"Success Cfg SFX Data Load {SFX_Volume}");
    }
    public void PlayerCfgBGMSave(float bgm)
    {
        BGM_Volume = bgm;
        PlayerPrefs.SetFloat(GlobalValue.BGM_Vol, BGM_Volume);
        Debug.LogWarning($"Success Cfg BGM Data Save {BGM_Volume}");
    }
    public void PlayerCfgBGMLoad()
    {
        float bgm = PlayerPrefs.GetFloat(GlobalValue.BGM_Vol, 1.0f);
        BGM.audioMixer.SetFloat(GlobalValue.BGM_Vol, Mathf.Log10(bgm) * 20);
        BGM_Volume = bgm;
        Debug.LogWarning($"Success Cfg BGM Data Load {BGM_Volume}");
    }

    public void PlayerCfgQualityLoad()
    {
        int quality = PlayerPrefs.GetInt(GlobalValue.Quality, QualitySettings.names.Length);
        QualitySettings.SetQualityLevel(quality);
        Debug.LogWarning("Success Cfg QualityLevel Data Load");
    }
    public void PlayerCfgQualitySave()
    {
        PlayerPrefs.SetInt(GlobalValue.Quality, QualitySettings.GetQualityLevel());
        Debug.LogWarning("Success Cfg QualityLevel Data Save");
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
        Debug.LogWarning("Success Cfg Language Data Load");
    }
    public void PlayerCfgLanguageSave()
    {
        PlayerPrefs.SetInt(GlobalValue.Language, localizationIdx);
        Debug.LogWarning("Success Cfg Language Data Save");
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
        if (!JSON_DataParsing.FileCheck(JSON_DataParsing.UnitInventoryData_FilePath))
        {
            Debug.LogWarning($"{JSON_DataParsing.UnitInventoryData_FilePath} File is not found");
            return false;
        }

        if (!JSON_DataParsing.FileCheck(JSON_DataParsing.UnitGoodsData_FilePath))
        {
            Debug.LogWarning($"{JSON_DataParsing.UnitGoodsData_FilePath} File is not found");
            return false;
        }

        if (!JSON_DataParsing.FileCheck(JSON_DataParsing.SceneData_FilePath))
        {
            Debug.LogWarning($"{JSON_DataParsing.SceneData_FilePath} File is not found");
            return false;
        }

        return true;
    }

    public void DeleteJSONFile()
    {
        if (File.Exists(Application.dataPath + JSON_DataParsing.UnitInventoryData_FilePath))
        {
            try
            {
                File.Delete(Application.dataPath + JSON_DataParsing.UnitInventoryData_FilePath);
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

        if (File.Exists(Application.dataPath + JSON_DataParsing.UnitGoodsData_FilePath))
        {
            try
            {
                File.Delete(Application.dataPath + JSON_DataParsing.UnitGoodsData_FilePath);
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

        if (File.Exists(Application.dataPath + JSON_DataParsing.SceneData_FilePath))
        {
            try
            {
                File.Delete(Application.dataPath + JSON_DataParsing.SceneData_FilePath);
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
        var inventory_Itemlist = JSON_DataParsing.ItemListIdx;

        for (int i = 0; i < inventory_Itemlist.Count; i++)
        {
            if (All_ItemDB.ItemDBList[inventory_Itemlist[i]] == null)
            {
                continue;
            }
            inventory.AddInventoryItem(All_ItemDB.ItemDBList[inventory_Itemlist[i]]);
        }

        var inventory_Weaponlist = JSON_DataParsing.WeaponListIdx;

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
            JSON_DataParsing.ItemListIdx = items;
        }
        else
        {
            for (int i = 0; i < itemList.Count; i++)
            {
                items.Add(itemList[i].item.ItemIdx);
            }

            JSON_DataParsing.ItemListIdx = items;
        }


        List<int> weapons = new List<int>();
        if (weapon == null)
        {
            JSON_DataParsing.WeaponListIdx = weapons;
        }
        else
        {
            weapons.Add(weapon.weaponData.weaponCommandDataSO.WeaponIdx);
            JSON_DataParsing.WeaponListIdx = weapons;
        }

        Debug.LogWarning("Success Inventory Data Save");
    }

    public void PlayerUnlockItem()
    {
        var idxs = new List<int>();

        for (int i = 0; i < JSON_DataParsing.WaitUnlockItemList.Count; i++)
        {
            if (JSON_DataParsing.UnlockItemList.Contains(JSON_DataParsing.WaitUnlockItemList[i]))
            {
                continue;
            }
            JSON_DataParsing.UnlockItemList.Add(JSON_DataParsing.WaitUnlockItemList[i]);
            GameManager.Inst.EffectTextUI.UnlockitemNames.Add(All_ItemDB.ItemDBList[JSON_DataParsing.WaitUnlockItemList[i]].itemData.ItemNameLocal);
        }
    }

    public void PlayerCurrHealthLoad(UnitStats stats)
    {
        if (JSON_DataParsing.PlayerHealth == -1)
        {
            return;
        }
        stats.CurrentHealth = JSON_DataParsing.PlayerHealth;
        Debug.LogWarning("Success CurrHealth Save");
    }
    public void PlayerCurrHealthSave(int m_playerHealth)
    {
        JSON_DataParsing.PlayerHealth = m_playerHealth;
        Debug.LogWarning("Success CurrHealth Save");
    }

    //public void PlayerBuffSave(List<Buff> buffs)
    //{
    //    //Playerbuffs = buffs;
    //    //Debug.LogWarning("Success BuffData Save");
    //}
    //public void PlayerBuffLoad(BuffSystem buffSystem)
    //{
    //    //buffSystem.buffs = Playerbuffs;
    //    //Debug.LogWarning("Success BuffData Load");
    //}

    public int GameGoldLoad()
    {
        return JSON_DataParsing.GoldAmount;
    }
    public int GameElementalsculptureLoad()
    {
        return JSON_DataParsing.ElementalSculptureAmount;
    }
    //public bool GameElementalsculptureSave(int m_Elementalsculpture)
    //{
    //    if (m_Elementalsculpture <= 0)
    //        return false;

    //    JSON_DataParsing.ElementalSculptureAmount = m_Elementalsculpture;

    //    return true;
    //}
    public ElementalGoods GameElementalGoodsLoad()
    {
        return JSON_DataParsing.ElementalGoodsAmount;
    }

    public void SaveScene(int _stageNumber)
    {
        if (_stageNumber == 0)
        {
            for (int i = 0; i < GameManager.Inst.SceneNameList.Count; i++)
            {
                if (GameManager.Inst.SceneNameList[i] == GameManager.Inst.StageManager.CurrStageName)
                {
                    JSON_DataParsing.SceneNumber = i;
                    return;
                }
            }
        }
        JSON_DataParsing.SceneNumber = _stageNumber;
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

    //public Enemy_Count LoadEnemyCount()
    //{
    //    return JSON_DataParsing.EnemyCount;
    //}
    //public void SaveEnemyCount(Enemy_Count enemy_Count)
    //{
    //    JSON_DataParsing.EnemyCount = enemy_Count;
    //}

    //public void SavePlayTime(float _playTime)
    //{
    //    JSON_DataParsing.PlayTime = _playTime;
    //}

    //public void SaveSceneDataIdx(int _sceneDataIdx)
    //{
    //    JSON_DataParsing.SceneDataIdx = _sceneDataIdx;
    //}
    public void SaveBuffs(List<Buff> _buffs)
    {
        var _buffDatas = new List<BuffData>();
        for (int i = 0; i < _buffs.Count; i++)
        {
            var buff = new BuffData() { ButtItemIdx = _buffs[i].buffItemSO.ItemIdx, startTime = _buffs[i].startTime, CurrBuffCount = _buffs[i].CurrBuffCount };
            _buffDatas.Add(buff);
        }
        JSON_DataParsing.BuffDatas = _buffDatas;

    }

    public void LoadBuffs(BuffSystem buffSystem)
    {
        var buffDatas = JSON_DataParsing.BuffDatas;

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

    public int LoadSceneDataIdx()
    {
        return JSON_DataParsing.SceneDataIdx;
    }

    public void LoadPlayTime()
    {
        if (GameManager.Inst == null)
            return;

        GameManager.Inst.PlayTime = JSON_DataParsing.PlayTime;
    }

    public void SaveSkipCutSceneList(List<int> idxs)
    {
        if (idxs == null)
        {
            return;
        }

        if (idxs.Count == 0)
        {
            return;
        }
        JSON_DataParsing.SkipCutSceneList = idxs;
    }

    public List<int> LoadSkipCutSceneList()
    {
        return JSON_DataParsing.SkipCutSceneList;
    }
    public void SaveSkipBossCutSceneList(List<int> idxs)
    {
        if (idxs == null)
        {
            return;
        }

        if (idxs.Count == 0)
        {
            return;
        }
        JSON_DataParsing.SkipBossCutScene = idxs;
    }

    //public List<int> LoadSkipBossCutSceneList()
    //{
    //    return JSON_DataParsing.SkipBossCutScene;
    //}

    //public List<int> LoadUnlockItemList()
    //{
    //    return JSON_DataParsing.UnlockItemList;
    //}
    #endregion

    #region Data Controll Func

    /// <summary>
    /// Title = 2
    /// </summary>
    /// <param name="stageIndex"></param>
    public void NextStage(int stageIndex)
    {
        JSON_DataParsing.SceneNumber = stageIndex;
    }

    public void IncreaseGoods(GOODS_TPYE type, int goodsAmount)
    {
        switch (type)
        {
            case GOODS_TPYE.Gold:
                JSON_DataParsing.GoldAmount += goodsAmount;
                break;
            case GOODS_TPYE.FireGoods:
                JSON_DataParsing.ElementalGoodsAmount.FireGoods += goodsAmount;
                break;
            case GOODS_TPYE.WaterGoods:
                JSON_DataParsing.ElementalGoodsAmount.WaterGoods += goodsAmount;
                break;
            case GOODS_TPYE.EarthGoods:
                JSON_DataParsing.ElementalGoodsAmount.EarthGoods += goodsAmount;
                break;
            case GOODS_TPYE.WindGoods:
                JSON_DataParsing.ElementalGoodsAmount.WindGoods += goodsAmount;
                break;
        }
    }
    public void DecreseGoods(GOODS_TPYE type, int goodsAmount)
    {
        switch (type)
        {
            case GOODS_TPYE.Gold:
                JSON_DataParsing.GoldAmount -= goodsAmount;
                break;
            case GOODS_TPYE.FireGoods:
                JSON_DataParsing.ElementalGoodsAmount.FireGoods -= goodsAmount;
                break;
            case GOODS_TPYE.WaterGoods:
                JSON_DataParsing.ElementalGoodsAmount.WaterGoods -= goodsAmount;
                break;
            case GOODS_TPYE.EarthGoods:
                JSON_DataParsing.ElementalGoodsAmount.EarthGoods -= goodsAmount;
                break;
            case GOODS_TPYE.WindGoods:
                JSON_DataParsing.ElementalGoodsAmount.WindGoods -= goodsAmount;
                break;
        }
    }
    #endregion


    #region ItemDB Spawn

    public void SetLockItemList()
    {
        for (int i = 0; i < All_ItemDB.ItemDBList.Count; i++)
        {
            if (JSON_DataParsing.UnlockItemList.Contains(All_ItemDB.ItemDBList[i].ItemIdx))
            {
                continue;
            }
            JSON_DataParsing.lockItemList.Add(All_ItemDB.ItemDBList[i].ItemIdx);
        }
    }
    /// <summary>
    /// UnLockItemSpawn
    /// </summary>
    /// <param name="pos">Spawn Pos</param>
    public void UnLockItemSpawn(Vector3 pos)
    {
        if (GameManager.Inst == null)
            return;

        if (GameManager.Inst.StageManager == null)
            return;

        if(JSON_DataParsing.SceneDataIdx >= GlobalValue.MaxSceneIdx)
        {
            if (JSON_DataParsing.lockItemList.Count > 0)
            {
                var idx = DataManager.Inst.JSON_DataParsing.SceneDataIdx % JSON_DataParsing.lockItemList.Count;
                var itemData = All_ItemDB.ItemDBList[JSON_DataParsing.lockItemList[idx]];

                if (itemData == null)
                    return;

                if (GameManager.Inst.StageManager.SPM.SpawnItem(GameManager.Inst.StageManager.IM.InventoryItem, pos, GameManager.Inst.StageManager.IM.transform, itemData))
                {
                    Debug.Log($"SpawnItem {itemData.name}");
                    JSON_DataParsing.WaitUnlockItemList.Add(JSON_DataParsing.lockItemList[idx]);
                }
                return;
            }
        }        

        if (GameManager.Inst.StageManager.player.Inventory.Items.Count > 0)
        {
            List<int> itemidxs = new List<int>();
            for (int i = 0; i < GameManager.Inst.StageManager.player.Inventory.Items.Count; i++)
            {
                itemidxs.Add(GameManager.Inst.StageManager.player.Inventory.Items[i].item.ItemIdx);
            }

            List<int> AllItemIdxs = All_ItemDB_Idxs;

            //보유하고있지 않은 아이템 목록
            for (int i = 0; i < itemidxs.Count; i++) 
            {
                if (AllItemIdxs.Contains(itemidxs[i]))
                {
                    AllItemIdxs.Remove(itemidxs[i]);
                }
            }

            var idx = DataManager.Inst.JSON_DataParsing.SceneDataIdx % AllItemIdxs.Count;
            var itemData = All_ItemDB.ItemDBList[AllItemIdxs[idx]];

            if (itemData == null)
                return;

            if (GameManager.Inst.StageManager.SPM.SpawnItem(GameManager.Inst.StageManager.IM.InventoryItem, pos, GameManager.Inst.StageManager.IM.transform, itemData))
            {
                Debug.Log($"SpawnItem {itemData.name}");
            }
        }
    }
    #endregion
}
