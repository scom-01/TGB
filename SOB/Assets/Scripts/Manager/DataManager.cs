using SOB.CoreSystem;
using SOB.Weapons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DataManager : MonoBehaviour
{
    public static DataManager Inst = null;
    [HideInInspector]
    public List<WeaponData> Playerweapons = new List<WeaponData>();
    [HideInInspector]
    public List<StatsItemSO> Playeritems = new List<StatsItemSO>();

    public string SceneName;

    private bool isWeaponDataSave = false;

    [HideInInspector]
    public StatsData PlayerStatData;
    public float PlayerHealth;
    private bool isStatsDataSave = false;

    private void Awake()
    {
        if (Inst)
        {
            Destroy(this.gameObject);
            return;
        }

        Inst = this;
        DontDestroyOnLoad(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UserKeySettingLoad()
    {
        string rebinds = PlayerPrefs.GetString(GlobalValue.RebindsKey, string.Empty);
        if (string.IsNullOrEmpty(rebinds))
        {
            Debug.LogWarning("Load Fails");
            return;
        }
        GameManager.Inst.inputHandler.playerInput.actions.LoadBindingOverridesFromJson(rebinds);
        Debug.LogWarning("Load UserKeySetting Success");
    }
    public void UserKeySettingSave()
    {
        string rebinds = GameManager.Inst.inputHandler.playerInput.actions.SaveBindingOverridesAsJson();
        PlayerPrefs.SetString(GlobalValue.RebindsKey, rebinds);
        Debug.LogWarning("Save UserKeySetting Success");
    }

    public void PlayerInventoryDataLoad(Inventory inventory)
    {
        if (!isWeaponDataSave)
        {
            Debug.Log("저장된 Inventory Data가 없습니다.");
            return;
        }

        //inventory.weapons = Playerweapons;

        for (int i = 0; i < inventory.weapons.Count; i++)
        {
            inventory.weapons[i].weaponData = Playerweapons[i];
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

    public void SaveScene()
    {
        string stage = SceneName;
        PlayerPrefs.SetString(GlobalValue.NextStageName, stage);
        Debug.LogWarning("Save SceneData Success");
    }

    public void LoadScene()
    {
        string stage = PlayerPrefs.GetString(GlobalValue.NextStageName, "Stage1");
        SceneName = stage;
        Debug.LogWarning("Load SceneData Success");
    }
}
