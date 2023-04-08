using SOB.Weapons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DataManager : MonoBehaviour
{
    public static DataManager Inst = null;
    public List<Weapon> Playerweapons = new List<Weapon>();
    public List<StatsItemSO> Playeritems = new List<StatsItemSO>();

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
        Debug.LogWarning("Load Success");
    }
    public void UserKeySettingSave()
    {
        string rebinds = GameManager.Inst.inputHandler.playerInput.actions.SaveBindingOverridesAsJson();
        PlayerPrefs.SetString(GlobalValue.RebindsKey, rebinds);
        Debug.LogWarning("Save Success");
    }

    public void PlayerDataLoad(Inventory inventory)
    {
        inventory.weapons = Playerweapons;
        inventory.items = Playeritems;
    }

    public void PlayerDataSave(List<Weapon> weaponList, List<StatsItemSO> itemList)
    {
        if(weaponList.Count>0)
        {
            Playerweapons = weaponList;
        }

        if(itemList.Count>0)
        {
            Playeritems = itemList;
        }
    }
}
