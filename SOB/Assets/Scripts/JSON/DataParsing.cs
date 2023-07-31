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
        ElementalGoods temp = new ElementalGoods()
        {
            FireGoods = s1.FireGoods + s2.FireGoods,
            WaterGoods = s1.WaterGoods + s2.WaterGoods,
            EarthGoods = s1.EarthGoods + s2.EarthGoods,
            WindGoods = s1.WindGoods + s2.WindGoods
        };
        return temp;
    }
    public static ElementalGoods operator *(ElementalGoods s1, int s2)
    {
        ElementalGoods temp = new ElementalGoods()
        {
            FireGoods = s1.FireGoods * s2,
            WaterGoods = s1.WaterGoods * s2,
            EarthGoods = s1.EarthGoods * s2,
            WindGoods = s1.WindGoods * s2
        };
        return temp;
    }
    public static ElementalGoods operator *(int s1, ElementalGoods s2)
    {
        ElementalGoods temp = new ElementalGoods()
        {
            FireGoods = s2.FireGoods * s1,
            WaterGoods = s2.WaterGoods * s1,
            EarthGoods = s2.EarthGoods * s1,
            WindGoods = s2.WindGoods * s1
        };
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
        if (obj is null) /*ReferenceEquals(null, obj))*/ return false;
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
        public JSON_SceneData(int sceneNumber = 2, int _sceneDataIdx = 0, int playerHealth = -1, float playTime = 0, Enemy_Count _enemy_Count = new Enemy_Count())
        {
            SceneNumber = sceneNumber;
            SceneDataIdx = _sceneDataIdx;
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
        public int[] WaitUnlockItemIdxs = new int[0];

        public JSON_DefaultData()
        {
            SkipCutSceneList = new int[0];
            SkipBossCutScene = new int[0];
            UnlockItemIdxs = GlobalValue.DefaultUnlockItem;
            WaitUnlockItemIdxs = new int[0];
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

            if (WaitUnlockItemIdxs.Length > 0)
            {
                var str = "";
                foreach (int idx in WaitUnlockItemIdxs)
                {
                    str += idx.ToString() + " ";
                }
                Debug.Log($"WaitUnlockItemIdxs = {str}");
            }
        }
    }

    [HideInInspector] public List<int> SkipCutSceneList;
    [HideInInspector] public List<int> SkipBossCutScene;
    [HideInInspector] public List<int> UnlockItemList;
    [HideInInspector] public List<int> WaitUnlockItemList;
    [HideInInspector] public List<int> lockItemList;
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
                ElementalGoodsAmount = jtest2.elementalGoods;
            }
            else
            {
                FileStream stream = new FileStream(Application.dataPath + UnitGoodsData_FilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                JSON_Goods jTest1 = new JSON_Goods();
                GoldAmount = jTest1.gold;
                ElementalSculptureAmount = jTest1.elementalSculpture;
                ElementalGoodsAmount = jTest1.elementalGoods;
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
                SceneDataIdx = json.SceneDataIdx;
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
                SceneDataIdx = json.SceneDataIdx;
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
    public bool Json_DefaultDataParsing()
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
                WaitUnlockItemList = jtest2.WaitUnlockItemIdxs.ToList();
            }
            else
            {
                FileStream stream = new FileStream(Application.dataPath + DefaultData_FilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                JSON_DefaultData jTest1 = new JSON_DefaultData();
                SkipCutSceneList = jTest1.SkipCutSceneList.ToList();
                SkipBossCutScene = jTest1.SkipBossCutScene.ToList();
                UnlockItemList = jTest1.UnlockItemIdxs.ToList();
                WaitUnlockItemList = jTest1.WaitUnlockItemIdxs.ToList();
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
        else
        {
            Debug.Log("Inventory Parsing Success");
        }

        if (!Json_GoodsParsing())
        {
            Debug.Log("Goods Parsing Fail");
            return false;
        }
        else
        {
            Debug.Log("Goods Parsing Success");
        }


        if (!Json_SceneDataParsing())
        {
            Debug.Log("SceneData Parsing Fail");
            return false;
        }
        else
        {
            Debug.Log("SceneData Parsing Success");
        }


        if (!Json_DefaultDataParsing())
        {
            Debug.Log("Default Parsing Fail");
            return false;
        }
        else
        {
            Debug.Log("Default Parsing Success");
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
                WaitUnlockItemList = json.WaitUnlockItemIdxs.ToList();
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

    public bool JSON_InventorySave()
    {
        JSON_Inventory inventory = new JSON_Inventory()
        {
            items = ItemListIdx.ToArray(),
            weapons = WeaponListIdx.ToArray()
        };
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
    public bool JSON_GoodsSave()
    {
        JSON_Goods goods = new JSON_Goods(GoldAmount, ElementalSculptureAmount, ElementalGoodsAmount);

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
    public bool JSON_SceneDataSave()
    {
        PlayTime = GameManager.Inst.PlayTime;
        JSON_SceneData sceneData = new JSON_SceneData(SceneNumber, SceneDataIdx, PlayerHealth, PlayTime, EnemyCount)
        {
            BuffDatas = BuffDatas
        };
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

    public bool JSON_DefaultDataSave()
    {
        JSON_DefaultData Data = new JSON_DefaultData()
        {
            SkipCutSceneList = SkipCutSceneList.ToArray(),
            SkipBossCutScene = SkipBossCutScene.ToArray(),
            UnlockItemIdxs = UnlockItemList.ToArray(),
            WaitUnlockItemIdxs = WaitUnlockItemList.ToArray()
        };
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
