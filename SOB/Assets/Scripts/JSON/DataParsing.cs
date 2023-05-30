using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using Unity.VisualScripting;
using UnityEditor.Localization.Plugins.XLIFF.V20;
using UnityEngine;


public class DataParsing : MonoBehaviour
{
    public string UnitStatData_FilePath;

    public ItemDB ItemDB;
    public WeaponDB WeaponDB;

    public List<int> ItemListIdx;
    public List<int> WeaponListIdx;

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            FileStream stream = new FileStream(Application.dataPath + UnitStatData_FilePath, FileMode.Open);

            byte[] data = new byte[stream.Length];
            stream.Read(data, 0, data.Length);
            stream.Close();
            string jsonData = Encoding.UTF8.GetString(data);
            JSON_Inventory jtest2 = JsonConvert.DeserializeObject<JSON_Inventory>(jsonData);
            jtest2.Print();
            ItemListIdx = jtest2.items.ToList();
            WeaponListIdx = jtest2.weapons.ToList();
            foreach (var item in ItemListIdx)
            {
                Debug.Log($"ItemListIdx = {item}");
            }
            foreach (var item in WeaponListIdx)
            {
                Debug.Log($"WeaponListIdx = {item}");
            }
        }
        catch (Exception e)
        {
            FileStream stream = new FileStream(Application.dataPath + UnitStatData_FilePath, FileMode.OpenOrCreate);
            JSON_Inventory jTest1 = new JSON_Inventory();
            string jsonData = JsonConvert.SerializeObject(jTest1);
            byte[] data = Encoding.UTF8.GetBytes(jsonData);
            stream.Write(data, 0, data.Length);
            stream.Close();

            Debug.LogWarning("stream is null");
            Debug.LogWarning(e.Message);
        }
    }

    private bool Json_Parsing()
    {
        try
        {
            FileStream stream = new FileStream(Application.dataPath + UnitStatData_FilePath, FileMode.Open);
            byte[] data = new byte[stream.Length];
            stream.Read(data, 0, data.Length);
            stream.Close();
            string jsonData = Encoding.UTF8.GetString(data);
            JSON_Inventory jtest2 = JsonConvert.DeserializeObject<JSON_Inventory>(jsonData);
            jtest2.Print();
            ItemListIdx = jtest2.items.ToList();
            WeaponListIdx = jtest2.weapons.ToList();
            foreach (var item in ItemListIdx)
            {
                Debug.Log($"ItemListIdx = {item}");
            }
            foreach (var item in WeaponListIdx)
            {
                Debug.Log($"WeaponListIdx = {item}");
            }
        }
        catch (Exception e)
        {
            FileStream stream = new FileStream(Application.dataPath + UnitStatData_FilePath, FileMode.OpenOrCreate);
            JSON_Inventory jTest1 = new JSON_Inventory();
            string jsonData = JsonConvert.SerializeObject(jTest1);
            byte[] data = Encoding.UTF8.GetBytes(jsonData);
            stream.Write(data, 0, data.Length);
            stream.Close();
            Debug.LogWarning(e.Message);
            return false;
        }
        return true;
    }
private JSON_Inventory Json_Parsing_Stream()
    {
        try
        {
            FileStream stream = new FileStream(Application.dataPath + UnitStatData_FilePath, FileMode.Open);
            byte[] data = new byte[stream.Length];
            stream.Read(data, 0, data.Length);
            stream.Close();
            string jsonData = Encoding.UTF8.GetString(data);
            JSON_Inventory jtest2 = JsonConvert.DeserializeObject<JSON_Inventory>(jsonData);
            jtest2.Print();
            ItemListIdx = jtest2.items.ToList();
            WeaponListIdx = jtest2.weapons.ToList();
            foreach (var item in ItemListIdx)
            {
                Debug.Log($"ItemListIdx = {item}");
            }
            foreach (var item in WeaponListIdx)
            {
                Debug.Log($"WeaponListIdx = {item}");
            }
            return jtest2;
        }
        catch (Exception e)
        {
            FileStream stream = new FileStream(Application.dataPath + UnitStatData_FilePath, FileMode.OpenOrCreate);
            JSON_Inventory jTest1 = new JSON_Inventory();
            string jsonData = JsonConvert.SerializeObject(jTest1);
            byte[] data = Encoding.UTF8.GetBytes(jsonData);
            stream.Write(data, 0, data.Length);
            stream.Close();
            Debug.LogWarning(e.Message);
            return jTest1;
        }
    }

    public List<int> Json_Read_item()
    {
        List<int> items = new List<int>();
        items = Json_Parsing_Stream().items.ToList();
        return items;
    }
    public List<int> Json_Read_weapon()
    {
        List<int> weapons = new List<int>();
        weapons = Json_Parsing_Stream().weapons.ToList();
        return weapons;
    }
    public bool Json_Overwrite_item(List<int> itemlist)
    {
        if (itemlist == null)
            return false;

        //기존 저장된 JSON을 찾지못하고 새로 만들었을 때
        if (!Json_Parsing())
        {
            ItemListIdx = itemlist;
            JSON_Inventory json = new JSON_Inventory();
            json.items = itemlist.ToArray();

            if(!JSON_InventorySave(json))
            {
                Debug.Log("Inventory Item Save Fail");
                return false;
            }
        }
        //기존 저장된 JSON파일을 덧씌울 때
        else
        {
            ItemListIdx = itemlist;
            JSON_Inventory json = new JSON_Inventory();
            json.items = ItemListIdx.ToArray();
            json.weapons = WeaponListIdx.ToArray();
            if (!JSON_InventorySave(json))
            {
                Debug.Log("Inventory Item Save Fail");
                return false;
            }
        }

        return true;
    }
    

    public bool Json_Overwrite_weapon(List<int> weaponlist)
    {
        if (weaponlist == null)
            return false;

        //기존 저장된 JSON을 찾지못하고 새로 만들었을 때
        if (!Json_Parsing())
        {
            WeaponListIdx = weaponlist;
            JSON_Inventory json = new JSON_Inventory();
            json.weapons = WeaponListIdx.ToArray();

            if(!JSON_InventorySave(json))
            {
                Debug.Log("Inventory Item Save Fail");
                return false;
            }
        }
        //기존 저장된 JSON파일을 덧씌울 때
        else
        {
            WeaponListIdx = weaponlist;
            JSON_Inventory json = new JSON_Inventory();
            json.items = ItemListIdx.ToArray();
            json.weapons = WeaponListIdx.ToArray();
            if (!JSON_InventorySave(json))
            {
                Debug.Log("Inventory Item Save Fail");
                return false;
            }
        }

        return true;
    }

    private bool JSON_InventorySave(JSON_Inventory inventory)
    {
        try
        {
            inventory.Print();
            FileStream stream = new FileStream(Application.dataPath + UnitStatData_FilePath, FileMode.Open);
            string jsonData = JsonConvert.SerializeObject(inventory);
            byte[] data = Encoding.UTF8.GetBytes(jsonData);
            stream.Write(data, 0, data.Length);
            stream.Close();
        }
        catch (Exception e)
        {
            Debug.LogWarning("File not found");
            Debug.LogWarning(e.Message);
            return false;
        }
        return true;
    }

    public class JSON_Inventory
    {
        public int[] items = { };
        public int[] weapons;
        public JSON_Inventory()
        {
            items = new int[0] ;
            weapons = new int[0] ;
        }

        public void Print()
        {
            foreach(int item in items)
            {
                Debug.Log($"items = {item}");
            }
            foreach(int item in weapons)
            {
                Debug.Log($"weapons = {item}");
            }
        }
    }
}
