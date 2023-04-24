using SOB.CoreSystem;
using SOB.Weapons;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Localization.Editor;
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

    #region GameData Value
    public GameObject BaseWeaponPrefab;
    [HideInInspector]
    public List<WeaponData> Playerweapons = new List<WeaponData>();
    [HideInInspector]
    public List<StatsItemSO> Playeritems = new List<StatsItemSO>();
    private bool isWeaponDataSave = false;

    public float PlayerHealth;

    public List<Buff> Playerbuffs = new List<Buff>();


    [HideInInspector]
    public StatsData PlayerStatData;
    private bool isStatsDataSave = false;
    public string SceneName { get; private set; }
    #endregion

    #region Setting
    public AudioMixerGroup BGM;
    public AudioMixerGroup SFX;
    [HideInInspector][Range(0.0f, 1.0f)] public float BGM_Volume;
    [HideInInspector][Range(0.0f, 1.0f)] public float SFX_Volume;
    public Localization localizaion;
    public int localizationIdx;
    public LocalizationSettings localizationSettings;
    #endregion



    private void Awake()
    {
        if (_Inst)
        {
            Destroy(this.gameObject);
            return;
        }

        _Inst = this;
        DontDestroyOnLoad(this.gameObject);
    }
    private void OnDisable()
    {
        //UserKeySettingSave();
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
    public void PlayerInventoryDataLoad(Inventory inventory)
    {
        if (!isWeaponDataSave)
        {
            Debug.Log("저장된 Inventory Data가 없습니다.");
            return;
        }

        //inventory.weapons = Playerweapons;

        if (Playerweapons.Count > inventory.weaponDatas.Count)
        {
            for (int i = inventory.weaponDatas.Count; i < Playerweapons.Count; i++)
            {
                inventory.AddWeapon(Playerweapons[i]);
            }
        }

        for (int i = 0; i < inventory.weapons.Count; i++)
        {
            inventory.weapons[i].weaponData = Playerweapons[i];
            inventory.weapons[i].weaponGenerator.Init();
        }
        inventory.items = Playeritems;
        Debug.LogWarning("Success Inventory Data Load");
    }

    public void PlayerInventoryDataSave(List<Weapon> weaponList, List<StatsItemSO> itemList)
    {
        if (weaponList.Count > 0)
        {
            for (int i = 0; i < weaponList.Count; i++)
            {
                Playerweapons.Add(weaponList[i].weaponData);
            }
        }

        if (itemList.Count > 0)
        {
            Playeritems = itemList;
        }

        isWeaponDataSave = true;
        Debug.LogWarning("Success Inventory Data Save");
    }

    public void PlayerStatLoad(UnitStats stat)
    {
        if (!isStatsDataSave)
        {
            Debug.Log("저장된 StatsData가 없습니다.");
            return;
        }
        stat.SetStat(PlayerStatData, PlayerHealth);


        Debug.LogWarning("Success StatsData Load");
    }
    public void PlayerStatSave(UnitStats stat)
    {
        PlayerStatData = stat.StatsData;
        PlayerHealth = stat.CurrentHealth;
        isStatsDataSave = true;
        Debug.LogWarning("Success StatsData Save");
    }

    public void PlayerBuffSave(List<Buff> buffs)
    {
        Playerbuffs = buffs;
        Debug.LogWarning("Success BuffData Save");
    }
    public void PlayerBuffLoad(BuffSystem buffSystem)
    {
        buffSystem.buffs = Playerbuffs;
        Debug.LogWarning("Success BuffData Load");
    }

    public void SaveScene(string stage)
    {
        PlayerPrefs.SetString(GlobalValue.StageName, stage);
        Debug.LogWarning("Save SceneData Success");
    }

    public void LoadScene()
    {
        string stage = PlayerPrefs.GetString(GlobalValue.StageName, "CutScene1");
        Debug.LogWarning($"Load SceneData Success {stage}");
    }
    #endregion

    public void NextStage(string stage)
    {
        SceneName = stage;
    }
   
}
