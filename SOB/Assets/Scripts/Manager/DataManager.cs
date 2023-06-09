using SOB.CoreSystem;
using SOB.Weapons;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEditor.Localization.Editor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;

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
    public WeaponDB All_WeaponDB;
    public ItemDB Lock_ItemDB;
    public ItemDB Unlock_ItemDB;

    [HideInInspector] public List<StatsItemSO> UnlockItemList;
    [HideInInspector] public List<int> UnlockItemListidx;

    [HideInInspector] public float PlayerHealth;
    //Goods
    [HideInInspector] public int GoldCount;
    [HideInInspector] public int ElementalsculptureCount;

    public List<Buff> Playerbuffs = new List<Buff>();

    public string SceneName { get; private set; }
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

        _Inst = this;
        DontDestroyOnLoad(this.gameObject);
    }
    private void OnDisable()
    {
        //UserKeySettingSave();
    }

    public void Init()
    {
    }

    #region Setting Func
    public void UserKeySettingLoad()
    {
        string rebinds = PlayerPrefs.GetString(GlobalValue.RebindsKey, string.Empty);
        if (string.IsNullOrEmpty(rebinds))
        {
            Debug.LogWarning("Load Fails");
            return;
        }
        GameManager.Inst?.inputHandler.playerInput.actions.LoadBindingOverridesFromJson(rebinds);
        Debug.LogWarning("Load UserKeySetting Success");
    }
    public void UserKeySettingSave()
    {
        string rebinds = GameManager.Inst?.inputHandler.playerInput.actions.SaveBindingOverridesAsJson();
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
            return false;
        }

        if (!JSON_DataParsing.FileCheck(JSON_DataParsing.UnitGoodsData_FilePath))
        {
            return false;
        }

        return true;
    }
    public void PlayerInventoryDataLoad(Inventory inventory)
    {
        if (JSON_DataParsing.Json_Read_item() == null)
        {
            Debug.Log("Json_Read_item is Fail");
            return;
        }

        var inventory_Itemlist = JSON_DataParsing.Json_Read_item();

        for (int i = 0; i < inventory_Itemlist.Count; i++)
        {
            inventory.AddInventoryItem(All_ItemDB.ItemDBList[inventory_Itemlist[i]]);
        }

        if (JSON_DataParsing.Json_Read_weapon() == null)
        {
            Debug.Log("Json_Read_weapon is Fail");
            return;
        }

        var inventory_Weaponlist = JSON_DataParsing.Json_Read_weapon();

        if(inventory_Weaponlist.Count > 0)
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
        else
        {
            
        }        
    }

    public void PlayerInventoryDataSave(Weapon weapon, List<ItemSet> itemList)
    {
        List<int> items = new List<int>();
        if (itemList == null)
        {
            if (!JSON_DataParsing.Json_Overwrite_item(null))
            {
                Debug.Log($"{items} is null");
            }
        }
        else
        {
            for (int i = 0; i < itemList.Count; i++)
            {
                items.Add(itemList[i].item.ItemIdx);
            }

            if (!JSON_DataParsing.Json_Overwrite_item(items))
            {
                Debug.Log($"{items} is null");
            }
        }


        List<int> weapons = new List<int>();
        if (weapon == null)
        {
            if (!JSON_DataParsing.Json_Overwrite_weapon(null))
            {
                Debug.Log($"{weapons} is null");
            }
        }
        else
        {
            weapons.Add(weapon.weaponData.weaponCommandDataSO.WeaponIdx);
            if (!JSON_DataParsing.Json_Overwrite_weapon(weapons))
            {
                Debug.Log($"{weapons} is null");
            }
        }

        Debug.LogWarning("Success Inventory Data Save");
    }

    public void PlayerUnlockItem()
    {
        for (int i = 0; i< UnlockItemList.Count; i++)
        {
            DataManager.Inst.Unlock_ItemDB.ItemDBList.Add(UnlockItemList[i]);
            for (int j = 0; j < Lock_ItemDB.ItemDBList.Count; j++)
            {
                if (Lock_ItemDB.ItemDBList[j] == UnlockItemList[i])
                {
                    DataManager.Inst.Lock_ItemDB.ItemDBList.RemoveAt(j);
                    j = 0;
                }
            }
        }
    }

    public void PlayerCurrHealthLoad(UnitStats stats)
    {
        if (JSON_DataParsing.Json_Read_SceneData() == null)
        {
            Debug.LogWarning("CurrHealth Load Fail");
            return;
        }
        if(JSON_DataParsing.Json_Read_SceneData().PlayerHealth == -1)
        {
            return;
        }    
        stats.CurrentHealth = JSON_DataParsing.Json_Read_SceneData().PlayerHealth;
        Debug.LogWarning("Success CurrHealth Save");
    }
    public void PlayerCurrHealthSave(int _playerHealth)
    {
        if(!JSON_DataParsing.Json_Overwrite_PlayerHealth(_playerHealth))
        {
            Debug.LogWarning("CurrHealth Save Fail");
        }
        Debug.LogWarning("Success CurrHealth Save");
    }

    public void PlayerBuffSave(List<Buff> buffs)
    {
        //Playerbuffs = buffs;
        //Debug.LogWarning("Success BuffData Save");
    }
    public void PlayerBuffLoad(BuffSystem buffSystem)
    {
        //buffSystem.buffs = Playerbuffs;
        //Debug.LogWarning("Success BuffData Load");
    }

    public void GameGoldLoad()
    {
        if (JSON_DataParsing.Json_Read_Goods() == null)
        {
            GoldCount = 0;
            return;
        }
        GoldCount = JSON_DataParsing.Json_Read_Goods().gold;
    }
    public bool GameGoldSave(int gold)
    {
        if (gold <= 0)
            return false;

        if (!JSON_DataParsing.Json_Overwrite_gold(gold))
        {
            Debug.Log($"{gold} is save fail");
            return false;
        }

        return true;
    }
    public void GameElementalsculptureLoad()
    {
        if(JSON_DataParsing.Json_Read_Goods() ==null)
        {
            ElementalsculptureCount = 0;
            return;
        }
        ElementalsculptureCount = JSON_DataParsing.Json_Read_Goods().elementalSculpture;
    }
    public bool GameElementalsculptureSave(int Elementalsculpture)
    {
        if (Elementalsculpture <= 0)
            return false;

        if (!JSON_DataParsing.Json_Overwrite_sculpture(Elementalsculpture))
        {
            Debug.Log($"{Elementalsculpture} is save fail");
            return false;
        }

        return true;
    }

    public void SaveScene(string _stage)
    {
        JSON_DataParsing.Json_Overwrite_sceneName(_stage);

        //PlayerPrefs.SetString(GlobalValue.StageName, _stage);
        //Debug.LogWarning("Save SceneData Success");
    }

    public void LoadScene()
    {
        if (JSON_DataParsing.Json_Read_SceneData() == null)
        {
            Debug.LogWarning("SceneName Load Fail");
            return;
        }
        if (JSON_DataParsing.Json_Read_SceneData().SceneName == "")
        {
            List<string> SceneList = new List<string>();

            for (int i = 0; i<SceneManager.sceneCountInBuildSettings; i++)
            {
                SceneList.Add(System.IO.Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i)));
            }

            SceneName = SceneList[3];
            SceneList = null;
            Debug.LogWarning("Default SceneName Save");
            return;
        }
        SceneName = JSON_DataParsing.Json_Read_SceneData().SceneName;
        Debug.LogWarning("Success SceneName Save");

        //string stage = PlayerPrefs.GetString(GlobalValue.StageName, "CutScene1");
        //Debug.LogWarning($"Load SceneName Success {stage}");
    }

    public void SavePlayTime(float _playTime)
    {
        JSON_DataParsing.Json_Overwrite_PlayTime(_playTime);
    }

    public void LoadPlayTime()
    {
        if (JSON_DataParsing.Json_Read_SceneData() == null)
        {
            Debug.LogWarning("PlayTime Load Fail");
            return;
        }

        if (GameManager.Inst == null)
            return;

        GameManager.Inst.PlayTime = JSON_DataParsing.Json_Read_SceneData().PlayTime;
    }
    #endregion

    #region Data Controll Func

    public void NextStage(string stage)
    {
        SceneName = stage;
    }

    public void IncreaseGold(int gold)
    {
        GoldCount += gold;
        //GameGoldSave(GoldCount);
    }
    public void DecreseGold(int gold)
    {
        GoldCount -= gold;
        //GameGoldSave(GoldCount);
    }
    public void IncreaseElementalsculpture(int sculpture)
    {
        ElementalsculptureCount += sculpture;
        //GameElementalsculptureSave(ElementalsculptureCount);
    }
    public void DecreseElementalsculpture(int sculpture)
    {
        ElementalsculptureCount -= sculpture;
        //GameElementalsculptureSave(ElementalsculptureCount);
    }
    #endregion


}
