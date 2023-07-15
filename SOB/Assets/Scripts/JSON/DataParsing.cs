using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

[Serializable]
public struct ElementalGoods
{
    public int FireGoods;
    public int WaterGoods;
    public int EarthGoods;
    public int WindGoods;
    public ElementalGoods(int fire = 0, int water = 0, int earth = 0, int wind = 0)
    {
        FireGoods = fire;
        WaterGoods = water;
        EarthGoods = earth;
        WindGoods = wind;
    }
    public static ElementalGoods operator +(ElementalGoods s1, ElementalGoods s2)
    {
        ElementalGoods temp = new ElementalGoods();
        temp.FireGoods = s1.FireGoods + s2.FireGoods;
        temp.WaterGoods = s1.WaterGoods + s2.WaterGoods;
        temp.EarthGoods = s1.EarthGoods + s2.EarthGoods;
        temp.WindGoods = s1.WindGoods + s2.WindGoods;
        return temp;
    }
    public static ElementalGoods operator *(ElementalGoods s1, int s2)
    {
        ElementalGoods temp = new ElementalGoods();
        temp.FireGoods = s1.FireGoods * s2;
        temp.WaterGoods = s1.WaterGoods * s2;
        temp.EarthGoods = s1.EarthGoods * s2;
        temp.WindGoods = s1.WindGoods * s2;
        return temp;
    }
    public static ElementalGoods operator *(int s1, ElementalGoods s2)
    {
        ElementalGoods temp = new ElementalGoods();
        temp.FireGoods = s2.FireGoods * s1;
        temp.WaterGoods = s2.WaterGoods * s1;
        temp.EarthGoods = s2.EarthGoods * s1;
        temp.WindGoods = s2.WindGoods * s1;
        return temp;
    }

    public int Count()
    {
        int count = 0;

        if (FireGoods > 0)
        {
            count++;
        }
        if (WaterGoods > 0)
        {
            count++;
        }
        if (EarthGoods > 0)
        {
            count++;
        }
        if (WindGoods > 0)
        {
            count++;
        }
        return count;
    }

    public static bool operator ==(ElementalGoods s1, ElementalGoods s2)
    {
        if (s1.FireGoods != s2.FireGoods)
        {
            return false;
        }
        if (s1.WaterGoods != s2.WaterGoods)
        {
            return false;
        }
        if (s1.EarthGoods != s2.EarthGoods)
        {
            return false;
        }
        if (s1.WindGoods != s2.WindGoods)
        {
            return false;
        }
        return true;
    }
    public static bool operator !=(ElementalGoods s1, ElementalGoods s2)
    {
        if (s1.FireGoods == s2.FireGoods)
        {
            return false;
        }
        if (s1.WaterGoods == s2.WaterGoods)
        {
            return false;
        }
        if (s1.EarthGoods == s2.EarthGoods)
        {
            return false;
        }
        if (s1.WindGoods == s2.WindGoods)
        {
            return false;
        }
        return true;
    }
    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        return obj is ElementalGoods && Equals((ElementalGoods)obj);
    }
    public bool Equals(ElementalGoods s1)
    {
        return FireGoods == s1.FireGoods && WaterGoods == s1.WaterGoods && EarthGoods == s1.EarthGoods && WindGoods == s1.WindGoods;
    }
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}

[Serializable]
public struct BuffData
{
    public int ButtItemIdx;

    public int CurrBuffCount;
    public float startTime;
}
[Serializable]
public struct Enemy_Count
{
    public int Normal_Enemy_Count;
    public int Elete_Enemy_Count;
    public int Boss_Enemy_Count;
    public Enemy_Count(int ne = 0, int ee = 0, int be = 0)
    {
        Normal_Enemy_Count = ne;
        Elete_Enemy_Count = ee;
        Boss_Enemy_Count = be;
    }
}
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
    public string SceneData_FilePath
    {
        get
        {
            string str = UnitData_DirectoryPath + SceneData_FileName + ".json";
            return str;
        }
    }
    public string DefaultData_FilePath
    {
        get
        {
            string str = UnitData_DirectoryPath + DefaultData_FileName + ".json";
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
    public string SceneData_FileName;
    public string DefaultData_FileName;

    #region JSON_Inventory
    public class JSON_Inventory
    {
        public int[] items;
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
    [HideInInspector] public List<int> ItemListIdx = new List<int>();
    [HideInInspector] public List<int> WeaponListIdx = new List<int>();
    #endregion

    #region JSON_Goods
    public class JSON_Goods
    {
        public int gold;
        public int elementalSculpture;
        public ElementalGoods elementalGoods;
        public JSON_Goods(int _gold = 0, int _elementalSculpture = 0, ElementalGoods elemental = new ElementalGoods())
        {
            gold = _gold;
            elementalSculpture = _elementalSculpture;
            elementalGoods = elemental;
        }

        public void Print()
        {
            Debug.Log($"Gold = {gold}");
            Debug.Log($"ElementalSculpture = {elementalSculpture}");
            Debug.Log($"ElementalGoods = {elementalGoods}");
        }
    }
    [HideInInspector] public int GoldAmount;
    [HideInInspector] public int ElementalSculptureAmount;
    [HideInInspector] public ElementalGoods ElementalGoodsAmount;

    #endregion

    #region JSON_SceneData
    public class JSON_SceneData
    {
        /// <summary>
        /// 씬 넘버
        /// Title = 2
        /// </summary>
        public int SceneNumber;
        /// <summary>
        /// 해당 게임의 랜덤성을 결정할 Int
        /// </summary>
        public int SceneDataIdx;
        public int PlayerHealth;
        public float PlayTime;
        public Enemy_Count EnemyCount;
        public List<BuffData> BuffDatas = new List<BuffData>();
        public JSON_SceneData(int sceneNumber = 2, int playerHealth = -1, float playTime = 0, Enemy_Count _enemy_Count = new Enemy_Count())
        {
            SceneNumber = sceneNumber;
            PlayerHealth = playerHealth;
            PlayTime = playTime;
            EnemyCount = _enemy_Count;
        }
        public void Print()
        {
            Debug.Log($"SceneNumber = {SceneNumber}");
            Debug.Log($"SceneDataIdx = {SceneDataIdx}");
            Debug.Log($"PlayerHealth = {PlayerHealth}");
            Debug.Log($"PlayTime = {PlayTime}");
            Debug.Log($"EnemyCount.Normal_Enemy_Count = {EnemyCount.Normal_Enemy_Count}");
            Debug.Log($"EnemyCount.Elete_Enemy_Count = {EnemyCount.Elete_Enemy_Count}");
            Debug.Log($"EnemyCount.Boss_Enemy_Count = {EnemyCount.Boss_Enemy_Count}");
        }
    }
    [HideInInspector] public int SceneNumber = 2;
    [HideInInspector] public int SceneDataIdx;
    [HideInInspector] public int PlayerHealth = -1;
    [HideInInspector] public float PlayTime;
    [HideInInspector] public Enemy_Count EnemyCount;
    [HideInInspector] public List<BuffData> BuffDatas;

    #endregion

    #region JSON_DefaultData
    public class JSON_DefaultData
    {
        public int[] SkipCutSceneList = new int[0];
        public int[] SkipBossCutScene = new int[0];
        public int[] UnlockItemIdxs = GlobalValue.DefaultUnlockItem;
        public JSON_DefaultData()
        {
            SkipCutSceneList = new int[0];
            SkipBossCutScene = new int[0];
            UnlockItemIdxs = GlobalValue.DefaultUnlockItem;
        }
        public void Print()
        {
            if (SkipCutSceneList.Length > 0)
            {
                var str = "";
                foreach (int item in SkipCutSceneList)
                {
                    str += item.ToString() + " ";
                }
                Debug.Log($"SkipCutSceneList = {str}");

            }

            if (SkipBossCutScene.Length > 0)
            {
                var str = "";
                foreach (int item in SkipBossCutScene)
                {
                    str += item.ToString() + " ";
                }
                Debug.Log($"SkipBossCutScene = {str}");

            }

            if (UnlockItemIdxs.Length > 0)
            {
                var str = "";
                foreach (int idx in UnlockItemIdxs)
                {
                    str += idx.ToString() + " ";
                }
                Debug.Log($"UnlockItemIdxs = {str}");
            }
        }
    }

    [HideInInspector] public List<int> SkipCutSceneList;
    [HideInInspector] public List<int> SkipBossCutScene;
    [HideInInspector] public List<int> UnlockItemList;
    #endregion

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
    private bool Json_SceneDataParsing()
    {
        if (!Directory.Exists(Application.dataPath + UnitData_DirectoryPath))
        {
            Directory.CreateDirectory(Application.dataPath + UnitData_DirectoryPath);
        }
        try
        {
            if (File.Exists(Application.dataPath + SceneData_FilePath))
            {
                FileStream stream = new FileStream(Application.dataPath + SceneData_FilePath, FileMode.Open, FileAccess.ReadWrite);
                byte[] data = new byte[stream.Length];
                stream.Read(data, 0, data.Length);
                stream.Close();
                string jsonData = Encoding.UTF8.GetString(data);
                JSON_SceneData json = JsonConvert.DeserializeObject<JSON_SceneData>(jsonData);
                json.Print();
                SceneNumber = json.SceneNumber;
                PlayerHealth = json.PlayerHealth;
                PlayTime = json.PlayTime;
                EnemyCount = json.EnemyCount;
                BuffDatas = json.BuffDatas;                
            }
            else
            {
                FileStream stream = new FileStream(Application.dataPath + SceneData_FilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                JSON_SceneData json = new JSON_SceneData();
                SceneNumber = json.SceneNumber;
                PlayerHealth = json.PlayerHealth;
                PlayTime = json.PlayTime;
                EnemyCount = json.EnemyCount;
                BuffDatas = json.BuffDatas;
                string jsonData = JsonConvert.SerializeObject(json);
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
    private bool Json_DefaultDataParsing()
    {
        if (!Directory.Exists(Application.dataPath + UnitData_DirectoryPath))
        {
            Directory.CreateDirectory(Application.dataPath + UnitData_DirectoryPath);
        }
        try
        {
            if (File.Exists(Application.dataPath + DefaultData_FilePath))
            {
                FileStream stream = new FileStream(Application.dataPath + DefaultData_FilePath, FileMode.Open, FileAccess.ReadWrite);
                byte[] data = new byte[stream.Length];
                stream.Read(data, 0, data.Length);
                stream.Close();
                string jsonData = Encoding.UTF8.GetString(data);
                JSON_DefaultData jtest2 = JsonConvert.DeserializeObject<JSON_DefaultData>(jsonData);
                jtest2.Print();
                SkipCutSceneList = jtest2.SkipCutSceneList.ToList();
                SkipBossCutScene = jtest2.SkipBossCutScene.ToList();
                UnlockItemList = jtest2.UnlockItemIdxs.ToList();
            }
            else
            {
                FileStream stream = new FileStream(Application.dataPath + DefaultData_FilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                JSON_DefaultData jTest1 = new JSON_DefaultData();
                SkipCutSceneList = jTest1.SkipCutSceneList.ToList();
                SkipBossCutScene = jTest1.SkipBossCutScene.ToList();
                SkipCutSceneList = jTest1.SkipCutSceneList.ToList();
                UnlockItemList = jTest1.UnlockItemIdxs.ToList();
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

    public bool Json_Parsing()
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

        if (!Json_SceneDataParsing())
        {
            Debug.Log("SceneData Parsing Fail");
            return false;
        }

        if (!Json_DefaultDataParsing())
        {
            Debug.Log("Default Parsing Fail");
            return false;
        }

        return true;
    }

    public JSON_Inventory Json_Read_Inventory()
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
                JSON_Inventory json = JsonConvert.DeserializeObject<JSON_Inventory>(jsonData);
                json.Print();
                ItemListIdx = json.items.ToList();
                WeaponListIdx = json.weapons.ToList();
                return json;
            }
            else
            {
                FileStream stream = new FileStream(Application.dataPath + UnitInventoryData_FilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                JSON_Inventory json = new JSON_Inventory();
                ItemListIdx = json.items.ToList();
                WeaponListIdx = json.weapons.ToList();
                string jsonData = JsonConvert.SerializeObject(json);
                byte[] data = Encoding.UTF8.GetBytes(jsonData);
                stream.Write(data, 0, data.Length);
                stream.Close();
                return json;
            }
        }
        catch (Exception e)
        {
            Debug.LogWarning(e.Message);
            return null;
        }
    }

    public JSON_Goods Json_Read_Goods()
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
                JSON_Goods json = JsonConvert.DeserializeObject<JSON_Goods>(jsonData);
                json.Print();
                GoldAmount = json.gold;
                ElementalSculptureAmount = json.elementalSculpture;
                ElementalGoodsAmount = json.elementalGoods;
                return json;
            }
            else
            {
                FileStream stream = new FileStream(Application.dataPath + UnitGoodsData_FilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                JSON_Goods json = new JSON_Goods();
                GoldAmount = json.gold;
                ElementalSculptureAmount = json.elementalSculpture;
                ElementalGoodsAmount = json.elementalGoods;
                string jsonData = JsonConvert.SerializeObject(json);
                byte[] data = Encoding.UTF8.GetBytes(jsonData);
                stream.Write(data, 0, data.Length);
                stream.Close();
                return json;
            }
        }
        catch (Exception e)
        {
            Debug.LogWarning(e.Message);
            return null;
        }
    }
    public JSON_SceneData Json_Read_SceneData()
    {
        if (!Directory.Exists(Application.dataPath + UnitData_DirectoryPath))
        {
            Directory.CreateDirectory(Application.dataPath + UnitData_DirectoryPath);
        }
        try
        {
            if (File.Exists(Application.dataPath + SceneData_FilePath))
            {
                FileStream stream = new FileStream(Application.dataPath + SceneData_FilePath, FileMode.Open, FileAccess.ReadWrite);
                byte[] data = new byte[stream.Length];
                stream.Read(data, 0, data.Length);
                stream.Close();
                string jsonData = Encoding.UTF8.GetString(data);
                JSON_SceneData json = JsonConvert.DeserializeObject<JSON_SceneData>(jsonData);
                json.Print();
                SceneNumber = json.SceneNumber;
                PlayerHealth = json.PlayerHealth;
                SceneDataIdx = json.SceneDataIdx;
                PlayTime = json.PlayTime;
                EnemyCount = json.EnemyCount;
                BuffDatas = json.BuffDatas;
                return json;
            }
            else
            {
                FileStream stream = new FileStream(Application.dataPath + SceneData_FilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                JSON_SceneData json = new JSON_SceneData();
                SceneNumber = json.SceneNumber;
                PlayerHealth = json.PlayerHealth;
                SceneDataIdx = json.SceneDataIdx;
                PlayTime = json.PlayTime;
                EnemyCount = json.EnemyCount;
                BuffDatas = json.BuffDatas;
                string jsonData = JsonConvert.SerializeObject(json);
                byte[] data = Encoding.UTF8.GetBytes(jsonData);
                stream.Write(data, 0, data.Length);
                stream.Close();
                return json;
            }
        }
        catch (Exception e)
        {
            Debug.LogWarning(e.Message);
            return null;
        }
    }

    public JSON_DefaultData Json_Read_DefaultData()
    {
        if (!Directory.Exists(Application.dataPath + UnitData_DirectoryPath))
        {
            Directory.CreateDirectory(Application.dataPath + UnitData_DirectoryPath);
        }
        try
        {
            if (File.Exists(Application.dataPath + DefaultData_FilePath))
            {
                FileStream stream = new FileStream(Application.dataPath + DefaultData_FilePath, FileMode.Open, FileAccess.ReadWrite);
                byte[] data = new byte[stream.Length];
                stream.Read(data, 0, data.Length);
                stream.Close();
                string jsonData = Encoding.UTF8.GetString(data);
                JSON_DefaultData json = JsonConvert.DeserializeObject<JSON_DefaultData>(jsonData);
                json.Print();
                return json;
            }
            else
            {
                FileStream stream = new FileStream(Application.dataPath + DefaultData_FilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                JSON_DefaultData json = new JSON_DefaultData();
                SkipCutSceneList = json.SkipCutSceneList.ToList();
                SkipBossCutScene = json.SkipBossCutScene.ToList();
                UnlockItemList = json.UnlockItemIdxs.ToList();
                string jsonData = JsonConvert.SerializeObject(json);
                byte[] data = Encoding.UTF8.GetBytes(jsonData);
                stream.Write(data, 0, data.Length);
                stream.Close();
                return json;
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

    public bool Json_Overwrite_item(List<int> _itemlist)
    {
        if (_itemlist == null)
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
            ItemListIdx = _itemlist;
            JSON_Inventory json = new JSON_Inventory();
            json.items = _itemlist.ToArray();

            if (!JSON_InventorySave(json))
            {
                Debug.Log("Inventory Item Save Fail");
                return false;
            }
        }
        //기존 저장된 JSON파일을 덧씌울 때
        else
        {
            ItemListIdx = _itemlist;
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

    public bool Json_Overwrite_weapon(List<int> _weaponlist)
    {
        if (_weaponlist == null)
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
            WeaponListIdx = _weaponlist;
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
            WeaponListIdx = _weaponlist;
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
            json.elementalGoods = ElementalGoodsAmount;
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
            json.elementalSculpture = ElementalSculptureAmount;

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
            json.elementalGoods = ElementalGoodsAmount;
            if (!JSON_GoodsSave(json))
            {
                Debug.Log("Goods Item Save Fail");
                return false;
            }
        }

        return true;
    }
    public bool Json_Overwrite_ElementalGoods(ElementalGoods _elementalgoods)
    {
        //기존 저장된 JSON을 찾지못하고 새로 만들었을 때
        if (!Json_Parsing())
        {
            ElementalGoodsAmount = _elementalgoods;
            JSON_Goods json = new JSON_Goods();
            json.elementalGoods = ElementalGoodsAmount;

            if (!JSON_GoodsSave(json))
            {
                Debug.Log("Goods Item Save Fail");
                return false;
            }
        }
        //기존 저장된 JSON파일을 덧씌울 때
        else
        {
            ElementalGoodsAmount = _elementalgoods;
            JSON_Goods json = new JSON_Goods();
            json.gold = GoldAmount;
            json.elementalSculpture = ElementalSculptureAmount;
            json.elementalGoods = ElementalGoodsAmount;
            if (!JSON_GoodsSave(json))
            {
                Debug.Log("Goods Item Save Fail");
                return false;
            }
        }

        return true;
    }

    public bool Json_Overwrite_SceneName(int _sceneNumber)
    {
        //기존 저장된 JSON을 찾지못하고 새로 만들었을 때
        if (!Json_Parsing())
        {
            SceneNumber = _sceneNumber;
            JSON_SceneData json = new JSON_SceneData();
            json.SceneNumber = SceneNumber;

            if (!JSON_SceneDataSave(json))
            {
                Debug.Log("SceneData Save Fail");
                return false;
            }
        }
        //기존 저장된 JSON파일을 덧씌울 때
        else
        {
            SceneNumber = _sceneNumber;
            JSON_SceneData json = new JSON_SceneData();
            json.SceneNumber = SceneNumber;
            json.PlayerHealth = PlayerHealth;
            json.PlayTime = PlayTime;
            json.EnemyCount = EnemyCount;
            json.BuffDatas = BuffDatas;
            if (!JSON_SceneDataSave(json))
            {
                Debug.Log("SceneData Save Fail");
                return false;
            }
        }
        return true;
    }
    public bool Json_Overwrite_PlayerHealth(int _playerHealth)
    {
        //기존 저장된 JSON을 찾지못하고 새로 만들었을 때
        if (!Json_Parsing())
        {
            PlayerHealth = _playerHealth;
            JSON_SceneData json = new JSON_SceneData();
            json.PlayerHealth = PlayerHealth;

            if (!JSON_SceneDataSave(json))
            {
                Debug.Log("SceneData Save Fail");
                return false;
            }
        }
        //기존 저장된 JSON파일을 덧씌울 때
        else
        {
            PlayerHealth = _playerHealth;
            JSON_SceneData json = new JSON_SceneData();
            json.SceneNumber = SceneNumber;
            json.PlayerHealth = PlayerHealth;
            json.SceneDataIdx = SceneDataIdx;
            json.PlayTime = PlayTime;
            json.EnemyCount = EnemyCount;
            json.BuffDatas = BuffDatas;
            if (!JSON_SceneDataSave(json))
            {
                Debug.Log("SceneData Save Fail");
                return false;
            }
        }
        return true;
    }
    public bool Json_Overwrite_SceneDataIdx(int _sceneDataIdx)
    {
        //기존 저장된 JSON을 찾지못하고 새로 만들었을 때
        if (!Json_Parsing())
        {
            SceneDataIdx = _sceneDataIdx;
            JSON_SceneData json = new JSON_SceneData();
            json.PlayerHealth = PlayerHealth;

            if (!JSON_SceneDataSave(json))
            {
                Debug.Log("SceneData Save Fail");
                return false;
            }
        }
        //기존 저장된 JSON파일을 덧씌울 때
        else
        {
            SceneDataIdx = _sceneDataIdx;
            JSON_SceneData json = new JSON_SceneData();
            json.SceneNumber = SceneNumber;
            json.PlayerHealth = PlayerHealth;
            json.SceneDataIdx = SceneDataIdx;
            json.PlayTime = PlayTime;
            json.EnemyCount = EnemyCount;
            json.BuffDatas = BuffDatas;
            if (!JSON_SceneDataSave(json))
            {
                Debug.Log("SceneData Save Fail");
                return false;
            }
        }
        return true;
    }
    public bool Json_Overwrite_PlayTime(float _playTime)
    {
        //기존 저장된 JSON을 찾지못하고 새로 만들었을 때
        if (!Json_Parsing())
        {
            PlayTime = _playTime;
            JSON_SceneData json = new JSON_SceneData();
            json.PlayerHealth = PlayerHealth;

            if (!JSON_SceneDataSave(json))
            {
                Debug.Log("SceneData Save Fail");
                return false;
            }
        }
        //기존 저장된 JSON파일을 덧씌울 때
        else
        {
            PlayTime = _playTime;
            JSON_SceneData json = new JSON_SceneData();
            json.SceneNumber = SceneNumber;
            json.PlayerHealth = PlayerHealth;
            json.PlayTime = PlayTime;
            json.EnemyCount = EnemyCount;
            json.BuffDatas = BuffDatas;
            if (!JSON_SceneDataSave(json))
            {
                Debug.Log("SceneData Save Fail");
                return false;
            }
        }
        return true;
    }
    public bool Json_Overwrite_EnemyCount(Enemy_Count _enemyCount)
    {
        //기존 저장된 JSON을 찾지못하고 새로 만들었을 때
        if (!Json_Parsing())
        {
            EnemyCount = _enemyCount;
            JSON_SceneData json = new JSON_SceneData();
            json.EnemyCount = EnemyCount;

            if (!JSON_SceneDataSave(json))
            {
                Debug.Log("SceneData Save Fail");
                return false;
            }
        }
        //기존 저장된 JSON파일을 덧씌울 때
        else
        {
            EnemyCount = _enemyCount;
            JSON_SceneData json = new JSON_SceneData();
            json.SceneNumber = SceneNumber;
            json.PlayerHealth = PlayerHealth;
            json.PlayTime = PlayTime;
            json.EnemyCount = EnemyCount;
            json.BuffDatas = BuffDatas;

            if (!JSON_SceneDataSave(json))
            {
                Debug.Log("SceneData Save Fail");
                return false;
            }
        }
        return true;
    }
    public bool Json_Overwrite_Buff(List<BuffData> _BuffList)
    {
        //기존 저장된 JSON을 찾지못하고 새로 만들었을 때
        if (!Json_Parsing())
        {
            BuffDatas = _BuffList;
            JSON_SceneData json = new JSON_SceneData();
            json.BuffDatas = BuffDatas;

            if (!JSON_SceneDataSave(json))
            {
                Debug.Log("SceneData Save Fail");
                return false;
            }
        }
        //기존 저장된 JSON파일을 덧씌울 때
        else
        {
            BuffDatas = _BuffList;
            JSON_SceneData json = new JSON_SceneData();
            json.SceneNumber = SceneNumber;
            json.PlayerHealth = PlayerHealth;
            json.PlayTime = PlayTime;
            json.EnemyCount = EnemyCount;
            json.BuffDatas = BuffDatas;
            if (!JSON_SceneDataSave(json))
            {
                Debug.Log("SceneData Save Fail");
                return false;
            }
        }
        return true;
    }

    public bool Json_Overwrite_SkipCutScene(List<int> _cutScene)
    {
        if (_cutScene == null)
        {
            List<int> ints = new List<int>();
            SkipCutSceneList = ints;
            JSON_DefaultData json = new JSON_DefaultData();
            json.SkipCutSceneList = SkipCutSceneList.ToArray();
            json.SkipBossCutScene = SkipBossCutScene.ToArray();
            json.UnlockItemIdxs = UnlockItemList.ToArray();
            if (!JSON_DefaultDataSave(json))
            {
                Debug.Log("DefaultData Save Fail");
                return false;
            }
            return false;
        }

        //기존 저장된 JSON을 찾지못하고 새로 만들었을 때
        if (!Json_Parsing())
        {
            SkipCutSceneList = _cutScene;
            JSON_DefaultData json = new JSON_DefaultData();
            json.SkipCutSceneList = SkipCutSceneList.ToArray();
            json.SkipBossCutScene = SkipBossCutScene.ToArray();
            json.UnlockItemIdxs = UnlockItemList.ToArray();
            if (!JSON_DefaultDataSave(json))
            {
                Debug.Log("DefaultData Save Fail");
                return false;
            }
        }
        //기존 저장된 JSON파일을 덧씌울 때
        else
        {
            SkipCutSceneList = _cutScene;
            JSON_DefaultData json = new JSON_DefaultData();
            json.SkipCutSceneList = SkipCutSceneList.ToArray();
            json.SkipBossCutScene = SkipBossCutScene.ToArray();
            json.UnlockItemIdxs = UnlockItemList.ToArray();
            if (!JSON_DefaultDataSave(json))
            {
                Debug.Log("DefaultData Save Fail");
                return false;
            }
        }

        return true;
    }
    public bool Json_Overwrite_SkipBossCutScene(List<int> _cutScene)
    {
        if (_cutScene == null)
        {
            List<int> ints = new List<int>();
            SkipBossCutScene = ints;
            JSON_DefaultData json = new JSON_DefaultData();
            json.SkipCutSceneList = SkipCutSceneList.ToArray();
            json.SkipBossCutScene = SkipBossCutScene.ToArray();
            json.UnlockItemIdxs = UnlockItemList.ToArray();
            if (!JSON_DefaultDataSave(json))
            {
                Debug.Log("DefaultData Save Fail");
                return false;
            }
            return false;
        }

        //기존 저장된 JSON을 찾지못하고 새로 만들었을 때
        if (!Json_Parsing())
        {
            SkipBossCutScene = _cutScene;
            JSON_DefaultData json = new JSON_DefaultData();
            json.SkipCutSceneList = SkipCutSceneList.ToArray();
            json.SkipBossCutScene = SkipBossCutScene.ToArray();
            json.UnlockItemIdxs = UnlockItemList.ToArray();
            if (!JSON_DefaultDataSave(json))
            {
                Debug.Log("DefaultData Save Fail");
                return false;
            }
        }
        //기존 저장된 JSON파일을 덧씌울 때
        else
        {
            SkipBossCutScene = _cutScene;
            JSON_DefaultData json = new JSON_DefaultData();
            json.SkipCutSceneList = SkipCutSceneList.ToArray();
            json.SkipBossCutScene = SkipBossCutScene.ToArray();
            json.UnlockItemIdxs = UnlockItemList.ToArray();
            if (!JSON_DefaultDataSave(json))
            {
                Debug.Log("DefaultData Save Fail");
                return false;
            }
        }

        return true;
    }
    public bool Json_Overwrite_UnlockItem(List<int> _UnlockItemIdx)
    {
        if (_UnlockItemIdx == null)
        {
            List<int> ints = new List<int>();
            UnlockItemList = ints;
            JSON_DefaultData json = new JSON_DefaultData();
            json.SkipCutSceneList = SkipCutSceneList.ToArray();
            json.SkipBossCutScene = SkipBossCutScene.ToArray();
            json.UnlockItemIdxs = UnlockItemList.ToArray();
            if (!JSON_DefaultDataSave(json))
            {
                Debug.Log("DefaultData Save Fail");
                return false;
            }
            return false;
        }

        //기존 저장된 JSON을 찾지못하고 새로 만들었을 때
        if (!Json_Parsing())
        {
            UnlockItemList = _UnlockItemIdx;
            JSON_DefaultData json = new JSON_DefaultData();
            json.SkipCutSceneList = SkipCutSceneList.ToArray();
            json.SkipBossCutScene = SkipBossCutScene.ToArray();
            json.UnlockItemIdxs = UnlockItemList.ToArray();

            if (!JSON_DefaultDataSave(json))
            {
                Debug.Log("DefaultData Save Fail");
                return false;
            }
        }
        //기존 저장된 JSON파일을 덧씌울 때
        else
        {
            UnlockItemList = _UnlockItemIdx;
            JSON_DefaultData json = new JSON_DefaultData();
            json.SkipCutSceneList = SkipCutSceneList.ToArray();
            json.SkipBossCutScene = SkipBossCutScene.ToArray();
            json.UnlockItemIdxs = UnlockItemList.ToArray();
            if (!JSON_DefaultDataSave(json))
            {
                Debug.Log("DefaultData Save Fail");
                return false;
            }
        }

        return true;
    }
    public bool Json_Addwrite_SkipCutScene(List<int> _cutScene)
    {
        if (_cutScene == null)
        {
            List<int> ints = new List<int>();
            SkipCutSceneList = ints;
            JSON_DefaultData json = new JSON_DefaultData();
            json.SkipCutSceneList = SkipCutSceneList.ToArray();
            json.SkipBossCutScene = SkipBossCutScene.ToArray();
            json.UnlockItemIdxs = UnlockItemList.ToArray();
            if (!JSON_DefaultDataSave(json))
            {
                Debug.Log("DefaultData Save Fail");
                return false;
            }
            return false;
        }

        //기존 저장된 JSON을 찾지못하고 새로 만들었을 때
        if (!Json_Parsing())
        {
            foreach (var item in _cutScene)
            {
                SkipCutSceneList.Add(item);
            }
            JSON_DefaultData json = new JSON_DefaultData();
            json.SkipCutSceneList = SkipCutSceneList.ToArray();
            json.SkipBossCutScene = SkipBossCutScene.ToArray();
            json.UnlockItemIdxs = UnlockItemList.ToArray();
            if (!JSON_DefaultDataSave(json))
            {
                Debug.Log("DefaultData Save Fail");
                return false;
            }
        }
        //기존 저장된 JSON파일을 덧씌울 때
        else
        {
            SkipCutSceneList = _cutScene;
            JSON_DefaultData json = new JSON_DefaultData();
            json.SkipCutSceneList = SkipCutSceneList.ToArray();
            json.SkipBossCutScene = SkipBossCutScene.ToArray();
            json.UnlockItemIdxs = UnlockItemList.ToArray();
            if (!JSON_DefaultDataSave(json))
            {
                Debug.Log("DefaultData Save Fail");
                return false;
            }
        }

        return true;
    }
    public bool Json_Addwrite_UnlockItem(List<int> _UnlockItemIdx)
    {
        if (_UnlockItemIdx == null)
        {
            List<int> ints = new List<int>();
            UnlockItemList = ints;
            JSON_DefaultData json = new JSON_DefaultData();
            json.SkipCutSceneList = SkipCutSceneList.ToArray();
            json.SkipBossCutScene = SkipBossCutScene.ToArray();
            json.UnlockItemIdxs = UnlockItemList.ToArray();
            if (!JSON_DefaultDataSave(json))
            {
                Debug.Log("DefaultData Save Fail");
                return false;
            }
            return false;
        }

        //기존 저장된 JSON을 찾지못하고 새로 만들었을 때
        if (!Json_Parsing())
        {
            foreach (var item in _UnlockItemIdx)
            {
                UnlockItemList.Add(item);
            }
            JSON_DefaultData json = new JSON_DefaultData();
            json.SkipCutSceneList = SkipCutSceneList.ToArray();
            json.SkipBossCutScene = SkipBossCutScene.ToArray();
            json.UnlockItemIdxs = UnlockItemList.ToArray();

            if (!JSON_DefaultDataSave(json))
            {
                Debug.Log("DefaultData Save Fail");
                return false;
            }
        }
        //기존 저장된 JSON파일을 덧씌울 때
        else
        {
            for (int i = 0; i < _UnlockItemIdx.Count; i++)
            {
                if (!UnlockItemList.Contains(_UnlockItemIdx[i]))
                {
                    UnlockItemList.Add(_UnlockItemIdx[i]);
                }
            }
            JSON_DefaultData json = new JSON_DefaultData();
            json.SkipCutSceneList = SkipCutSceneList.ToArray();
            json.SkipBossCutScene = SkipBossCutScene.ToArray();
            json.UnlockItemIdxs = UnlockItemList.ToArray();
            if (!JSON_DefaultDataSave(json))
            {
                Debug.Log("DefaultData Save Fail");
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
            Debug.LogWarning($"{Application.dataPath + UnitInventoryData_FilePath} File not found");
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
            Debug.LogWarning($"{Application.dataPath + UnitGoodsData_FilePath} File not found");
            Debug.LogWarning(e.Message);
            return false;
        }
        return true;
    }
    private bool JSON_SceneDataSave(JSON_SceneData sceneData)
    {
        try
        {
            sceneData.Print();
            if (!Directory.Exists(Application.dataPath + UnitData_DirectoryPath))
            {
                Directory.CreateDirectory(Application.dataPath + UnitData_DirectoryPath);
            }
            if (File.Exists(Application.dataPath + SceneData_FilePath))
            {
                string jsonData = JsonUtility.ToJson(sceneData);
                File.WriteAllText(Application.dataPath + SceneData_FilePath, jsonData);
            }
        }
        catch (Exception e)
        {
            Debug.LogWarning($"{Application.dataPath + SceneData_FilePath} File not found");
            Debug.LogWarning(e.Message);
            return false;
        }
        return true;
    }

    private bool JSON_DefaultDataSave(JSON_DefaultData Data)
    {
        try
        {
            Data.Print();
            if (!Directory.Exists(Application.dataPath + UnitData_DirectoryPath))
            {
                Directory.CreateDirectory(Application.dataPath + UnitData_DirectoryPath);
            }
            if (File.Exists(Application.dataPath + DefaultData_FilePath))
            {
                string jsonData = JsonUtility.ToJson(Data);
                File.WriteAllText(Application.dataPath + DefaultData_FilePath, jsonData);
            }
        }
        catch (Exception e)
        {
            Debug.LogWarning($"{Application.dataPath + DefaultData_FilePath} File not found");
            Debug.LogWarning(e.Message);
            return false;
        }
        return true;
    }

    public bool FileCheck(string filePath)
    {
        if (File.Exists(Application.dataPath + filePath))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
