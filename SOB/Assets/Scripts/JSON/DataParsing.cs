using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using Unity.VisualScripting;
using UnityEditor.Localization.Plugins.XLIFF.V20;
using UnityEngine;


public class DataParsing : MonoBehaviour
{
    public string UnitInventoryData_FilePath
    {
        get
        {
            string str = UnitData_DirectoryPath + UnitInventoryData_FileName + ".json";
            return str;
        }
    }
    public string UnitGoodsData_FilePath
    {
        get
        {
            string str = UnitData_DirectoryPath + UnitGoodsData_FileName + ".json";
            return str;
        }
    }

    public string UnitData_DirectoryPath = "/Resources/json/";

    /// <summary>
    /// 인벤토리 데이터
    /// </summary>
    public string UnitInventoryData_FileName;
    /// <summary>
    /// 재화 데이터
    /// </summary>
    public string UnitGoodsData_FileName;

    public List<int> ItemListIdx;
    public List<int> WeaponListIdx;
    public int GoldAmount;
    public int ElementalSculptureAmount;

    private bool Json_InventoryParsing()
    {
        if (!Directory.Exists(Application.dataPath + UnitData_DirectoryPath))
        {
            Directory.CreateDirectory(Application.dataPath + UnitData_DirectoryPath);
        }
        try
        {
            if (File.Exists(Application.dataPath + UnitInventoryData_FilePath))
            {
                FileStream stream = new FileStream(Application.dataPath + UnitInventoryData_FilePath, FileMode.Open, FileAccess.ReadWrite);
                byte[] data = new byte[stream.Length];
                stream.Read(data, 0, data.Length);
                stream.Close();
                string jsonData = Encoding.UTF8.GetString(data);
                JSON_Inventory jtest2 = JsonConvert.DeserializeObject<JSON_Inventory>(jsonData);
                jtest2.Print();
                ItemListIdx = jtest2.items.ToList();
                WeaponListIdx = jtest2.weapons.ToList();
            }
            else
            {
                FileStream stream = new FileStream(Application.dataPath + UnitInventoryData_FilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                JSON_Inventory jTest1 = new JSON_Inventory();
                ItemListIdx = jTest1.items.ToList();
                WeaponListIdx = jTest1.weapons.ToList();
                string jsonData = JsonConvert.SerializeObject(jTest1);
                StreamWriter sw = new StreamWriter(stream, Encoding.UTF8);
                sw.WriteLine(jsonData);
                sw.Close();
                stream.Close();
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
            return false;
        }
        return true;
    }
    private bool Json_GoodsParsing()
    {
        if (!Directory.Exists(Application.dataPath + UnitData_DirectoryPath))
        {
            Directory.CreateDirectory(Application.dataPath + UnitData_DirectoryPath);
        }
        try
        {
            if (File.Exists(Application.dataPath + UnitGoodsData_FilePath))
            {
                FileStream stream = new FileStream(Application.dataPath + UnitGoodsData_FilePath, FileMode.Open, FileAccess.ReadWrite);
                byte[] data = new byte[stream.Length];
                stream.Read(data, 0, data.Length);
                stream.Close();
                string jsonData = Encoding.UTF8.GetString(data);
                JSON_Goods jtest2 = JsonConvert.DeserializeObject<JSON_Goods>(jsonData);
                jtest2.Print();
                GoldAmount = jtest2.gold;
                ElementalSculptureAmount = jtest2.elementalSculpture;
            }
            else
            {
                FileStream stream = new FileStream(Application.dataPath + UnitGoodsData_FilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                JSON_Goods jTest1 = new JSON_Goods();
                GoldAmount = jTest1.gold;
                ElementalSculptureAmount = jTest1.elementalSculpture;
                string jsonData = JsonConvert.SerializeObject(jTest1);
                byte[] data = Encoding.UTF8.GetBytes(jsonData);
                stream.Write(data, 0, data.Length);
                stream.Close();
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
            return false;
        }
        return true;
    }

    private bool Json_Parsing()
    {
        if (!Json_InventoryParsing())
        {
            Debug.Log("Inventory Parsing Fail");
            return false;
        }

        if (!Json_GoodsParsing())
        {
            Debug.Log("Goods Parsing Fail");
            return false;
        }
        return true;
    }

    private JSON_Inventory Json_Read_Inventory()
    {
        if (!Directory.Exists(Application.dataPath + UnitData_DirectoryPath))
        {
            Directory.CreateDirectory(Application.dataPath + UnitData_DirectoryPath);
        }
        try
        {
            if(File.Exists(Application.dataPath + UnitInventoryData_FilePath))
            {
                FileStream stream = new FileStream(Application.dataPath + UnitInventoryData_FilePath, FileMode.Open, FileAccess.ReadWrite);
                byte[] data = new byte[stream.Length];
                stream.Read(data, 0, data.Length);
                stream.Close();
                string jsonData = Encoding.UTF8.GetString(data);
                JSON_Inventory jtest2 = JsonConvert.DeserializeObject<JSON_Inventory>(jsonData);
                jtest2.Print();
                ItemListIdx = jtest2.items.ToList();
                WeaponListIdx = jtest2.weapons.ToList();
                return jtest2;
            }
            else
            {
                FileStream stream = new FileStream(Application.dataPath + UnitInventoryData_FilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                JSON_Inventory jTest1 = new JSON_Inventory();
                ItemListIdx = jTest1.items.ToList();
                WeaponListIdx = jTest1.weapons.ToList();
                string jsonData = JsonConvert.SerializeObject(jTest1);
                byte[] data = Encoding.UTF8.GetBytes(jsonData);
                stream.Write(data, 0, data.Length);
                stream.Close();
                return jTest1;
            }           
        }
        catch (Exception e)
        {                        
            Debug.LogWarning(e.Message);
            return null;
        }
    }

    private JSON_Goods Json_Read_Goods()
    {
        if (!Directory.Exists(Application.dataPath + UnitData_DirectoryPath))
        {
            Directory.CreateDirectory(Application.dataPath + UnitData_DirectoryPath);
        }
        try
        {
            if (File.Exists(Application.dataPath + UnitGoodsData_FilePath))
            {
                FileStream stream = new FileStream(Application.dataPath + UnitGoodsData_FilePath, FileMode.Open, FileAccess.ReadWrite);
                byte[] data = new byte[stream.Length];
                stream.Read(data, 0, data.Length);
                stream.Close();
                string jsonData = Encoding.UTF8.GetString(data);
                JSON_Goods jtest2 = JsonConvert.DeserializeObject<JSON_Goods>(jsonData);
                jtest2.Print();
                GoldAmount = jtest2.gold;
                return jtest2;
            }
            else
            {
                FileStream stream = new FileStream(Application.dataPath + UnitGoodsData_FilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                JSON_Goods jTest1 = new JSON_Goods();
                ElementalSculptureAmount = jTest1.elementalSculpture;
                ElementalSculptureAmount = jTest1.elementalSculpture;
                string jsonData = JsonConvert.SerializeObject(jTest1);
                byte[] data = Encoding.UTF8.GetBytes(jsonData);
                stream.Write(data, 0, data.Length);
                stream.Close();
                return jTest1;
            }
        }
        catch (Exception e)
        {
            Debug.LogWarning(e.Message);
            return null;
        }
    }

    public List<int> Json_Read_item()
    {
        if (Json_Read_Inventory() == null)
        {
            Debug.Log($"Json_Read_Inventory Exception");
            return null;
        }

        List<int> items = new List<int>();
        items = Json_Read_Inventory().items.ToList();
        return items;
    }
    public List<int> Json_Read_weapon()
    {        
        if (Json_Read_Inventory() == null)
        {
            Debug.Log($"Json_Read_Inventory Exception");
            return null;
        }
        List<int> weapons = new List<int>();
        weapons = Json_Read_Inventory().weapons.ToList();
        return weapons;
    }

    public int Json_Read_gold()
    {        
        if (Json_Read_Goods() == null)
        {
            Debug.Log($"Json_Read_Goods Exception");
            return 0;
        }
        int gold = Json_Read_Goods().gold;
        return gold;
    }

    public int Json_Read_elementalSculpture()
    {        
        if (Json_Read_Goods() == null)
        {
            Debug.Log($"Json_Read_Goods Exception");
            return 0;
        }
        int elementalSculpture = Json_Read_Goods().elementalSculpture;
        return elementalSculpture;
    }
    public bool Json_Overwrite_item(List<int> itemlist)
    {
        if (itemlist == null)
        {
            List<int> ints = new List<int>();
            ItemListIdx = ints;
            JSON_Inventory json = new JSON_Inventory();
            json.items = ItemListIdx.ToArray();
            json.weapons = WeaponListIdx.ToArray();
            if (!JSON_InventorySave(json))
            {
                Debug.Log("Inventory Item Save Fail");
                return false;
            }
            return false;
        }

        //기존 저장된 JSON을 찾지못하고 새로 만들었을 때
        if (!Json_Parsing())
        {
            ItemListIdx = itemlist;
            JSON_Inventory json = new JSON_Inventory();
            json.items = itemlist.ToArray();

            if (!JSON_InventorySave(json))
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
        {
            List<int> ints = new List<int>();
            WeaponListIdx = ints;
            JSON_Inventory json = new JSON_Inventory();
            json.items = ItemListIdx.ToArray();
            json.weapons = WeaponListIdx.ToArray();
            if (!JSON_InventorySave(json))
            {
                Debug.Log("Inventory Item Save Fail");
                return false;
            }
            return false;
        }

        //기존 저장된 JSON을 찾지못하고 새로 만들었을 때
        if (!Json_Parsing())
        {
            WeaponListIdx = weaponlist;
            JSON_Inventory json = new JSON_Inventory();
            json.weapons = WeaponListIdx.ToArray();

            if (!JSON_InventorySave(json))
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
    public bool Json_Overwrite_gold(int _gold)
    {
        //기존 저장된 JSON을 찾지못하고 새로 만들었을 때
        if (!Json_Parsing())
        {
            GoldAmount = _gold;
            JSON_Goods json = new JSON_Goods();
            json.gold = GoldAmount;

            if (!JSON_GoodsSave(json))
            {
                Debug.Log("Goods Item Save Fail");
                return false;
            }
        }
        //기존 저장된 JSON파일을 덧씌울 때
        else
        {
            GoldAmount = _gold;
            JSON_Goods json = new JSON_Goods();
            json.gold = GoldAmount;
            json.elementalSculpture = ElementalSculptureAmount;
            if (!JSON_GoodsSave(json))
            {
                Debug.Log("Goods Item Save Fail");
                return false;
            }
        }

        return true;
    }
    public bool Json_Overwrite_sculpture(int _elementalsculpture)
    {
        //기존 저장된 JSON을 찾지못하고 새로 만들었을 때
        if (!Json_Parsing())
        {
            ElementalSculptureAmount = _elementalsculpture;
            JSON_Goods json = new JSON_Goods();
            json.gold = ElementalSculptureAmount;

            if (!JSON_GoodsSave(json))
            {
                Debug.Log("Goods Item Save Fail");
                return false;
            }
        }
        //기존 저장된 JSON파일을 덧씌울 때
        else
        {
            ElementalSculptureAmount = _elementalsculpture;
            JSON_Goods json = new JSON_Goods();
            json.gold = GoldAmount;
            json.elementalSculpture = ElementalSculptureAmount;
            if (!JSON_GoodsSave(json))
            {
                Debug.Log("Goods Item Save Fail");
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
            if (!Directory.Exists(Application.dataPath + UnitData_DirectoryPath))
            {
                Directory.CreateDirectory(Application.dataPath + UnitData_DirectoryPath);
            }
            if (File.Exists(Application.dataPath + UnitInventoryData_FilePath))
            {
                string jsonData = JsonUtility.ToJson(inventory);
                File.WriteAllText(Application.dataPath + UnitInventoryData_FilePath, jsonData);
            }
        }
        catch (Exception e)
        {
            Debug.LogWarning("File not found");
            Debug.LogWarning(e.Message);
            return false;
        }
        return true;
    }
    private bool JSON_GoodsSave(JSON_Goods goods)
    {
        try
        {
            goods.Print();
            if (!Directory.Exists(Application.dataPath + UnitData_DirectoryPath))
            {
                Directory.CreateDirectory(Application.dataPath + UnitData_DirectoryPath);
            }
            if (File.Exists(Application.dataPath + UnitGoodsData_FilePath))
            {
                string jsonData = JsonUtility.ToJson(goods);
                File.WriteAllText(Application.dataPath + UnitGoodsData_FilePath, jsonData);
            }
        }
        catch (Exception e)
        {
            Debug.LogWarning("File not found");
            Debug.LogWarning(e.Message);
            return false;
        }
        return true;
    }

    public bool FileCheck(string filePath)
    {
        if(File.Exists(Application.dataPath + filePath))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public class JSON_Goods
    {
        public int gold;
        public int elementalSculpture;
        public JSON_Goods(int _gold = 0, int _elementalSculpture = 0)
        {
            gold = _gold;
            elementalSculpture = _elementalSculpture;
        }

        public void Print()
        {
            Debug.Log($"Gold = {gold}");
            Debug.Log($"ElementalSculpture = {elementalSculpture}");
        }
    }

    public class JSON_Inventory
    {
        public int[] items = { };
        public int[] weapons;
        public JSON_Inventory()
        {
            items = new int[0];
            weapons = new int[0];
        }

        public void Print()
        {
            foreach (int item in items)
            {
                Debug.Log($"items = {item}");
            }
            foreach (int item in weapons)
            {
                Debug.Log($"weapons = {item}");
            }
        }
    }
}
