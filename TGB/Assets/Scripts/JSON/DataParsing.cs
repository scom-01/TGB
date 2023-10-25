using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// 인벤토리 데이터 경로
    /// </summary> 
    public string UnitInventoryData_FileName;
    /// <summary>
    /// 재화 데이터 경로
    /// </summary>
    public string UnitGoodsData_FileName;
    /// <summary>
    ///  씬 데이터 경로
    /// </summary>
    public string SceneData_FileName;
    /// <summary>
    /// 기본 데이터 경로
    /// </summary>
    public string DefaultData_FileName;

    private JsonSerializerSettings serializerSettings = new JsonSerializerSettings
    {
        ObjectCreationHandling = ObjectCreationHandling.Replace
    };

    #region JSON_Inventory
    public class JSON_Inventory
    {
        public List<int> items = new List<int>();
        public List<int> weapons = new List<int>();
        public JSON_Inventory()
        {
            items = new List<int>();
            weapons = new List<int>();
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

    public JSON_Inventory m_JSON_Inventory = new JSON_Inventory();
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

    public JSON_Goods m_JSON_Goods = new JSON_Goods();
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
        public List<int> SceneDataIdxs = new List<int>();
        public int PlayerHealth;
        public float PlayTime;
        public Enemy_Count EnemyCount;
        public List<BuffData> BuffDatas = new List<BuffData>();
        public JSON_SceneData()
        {
            SceneNumber = 2;
            SceneDataIdxs =  new List<int>();
            PlayerHealth = -1;
            PlayTime = 0;
            EnemyCount = new Enemy_Count();
            BuffDatas = new List<BuffData>();
        }
        public void Print()
        {
            Debug.Log($"SceneNumber = {SceneNumber}");
            Debug.Log($"SceneDataIdx = {SceneDataIdxs}");
            Debug.Log($"PlayerHealth = {PlayerHealth}");
            Debug.Log($"PlayTime = {PlayTime}");
            Debug.Log($"EnemyCount.Normal_Enemy_Count = {EnemyCount.Normal_Enemy_Count}");
            Debug.Log($"EnemyCount.Elete_Enemy_Count = {EnemyCount.Elete_Enemy_Count}");
            Debug.Log($"EnemyCount.Boss_Enemy_Count = {EnemyCount.Boss_Enemy_Count}");
        }
    }
    public JSON_SceneData m_JSON_SceneData = new JSON_SceneData();
    #endregion

    #region JSON_DefaultData
    public class JSON_DefaultData
    {
        public List<int> SkipCutSceneList = new List<int>();
        public List<int> SkipBossCutScene = new List<int>();
        public List<int> UnlockItemIdxs = new List<int>() { 0, 1, 2, 3, 4, 5, 6 };
        public List<int> WaitUnlockItemIdxs = new List<int>();

        public int hammer_piece = 0;
        /// <summary>
        /// agg, def, speed, critical, elemental
        /// </summary>
        public List<int> Bless = new List<int>() { 0, 0, 0, 0, 0 };
        public JSON_DefaultData()
        {
            SkipCutSceneList = new List<int>();
            SkipBossCutScene = new List<int>();
            UnlockItemIdxs = new List<int>() { 0, 1, 2, 3, 4, 5, 6 };
            WaitUnlockItemIdxs = new List<int>();
            hammer_piece = 0;
            Bless = new List<int>() { 0, 0, 0, 0, 0 };
        }
        public void Print()
        {
            if (SkipCutSceneList.Count > 0)
            {
                var str = "";
                foreach (int item in SkipCutSceneList)
                {
                    str += item.ToString() + " ";
                }
                Debug.Log($"SkipCutSceneList = {str}");

            }

            if (SkipBossCutScene.Count > 0)
            {
                var str = "";
                foreach (int item in SkipBossCutScene)
                {
                    str += item.ToString() + " ";
                }
                Debug.Log($"SkipBossCutScene = {str}");

            }

            if (UnlockItemIdxs.Count > 0)
            {
                var str = "";
                foreach (int idx in UnlockItemIdxs)
                {
                    str += idx.ToString() + " ";
                }
                Debug.Log($"UnlockItemIdxs = {str}");
            }

            if (WaitUnlockItemIdxs.Count > 0)
            {
                var str = "";
                foreach (int idx in WaitUnlockItemIdxs)
                {
                    str += idx.ToString() + " ";
                }
                Debug.Log($"WaitUnlockItemIdxs = {str}");
            }
            Debug.Log($"hammer_piece = {hammer_piece}");
            Debug.Log($"BlessStatsData.Bless_Agg_Lv = {Bless[0]}");
            Debug.Log($"BlessStatsData.Bless_Def_Lv = {Bless[1]}");
            Debug.Log($"BlessStatsData.Bless_Speed_Lv = {Bless[2]}");
            Debug.Log($"BlessStatsData.Bless_Critical_Lv = {Bless[3]}");
            Debug.Log($"BlessStatsData.Bless_Elemental_Lv = {Bless[4]}");
        }
    }

    public JSON_DefaultData m_JSON_DefaultData
    {
        get
        {
            if (GameManager.Inst?.StageManager?.player != null)
            {
                GameManager.Inst.StageManager.player.Core.CoreUnitStats.BlessStats.Bless_Agg_Lv = m_temp_DefaultData.Bless[0];
                GameManager.Inst.StageManager.player.Core.CoreUnitStats.BlessStats.Bless_Def_Lv = m_temp_DefaultData.Bless[1];
                GameManager.Inst.StageManager.player.Core.CoreUnitStats.BlessStats.Bless_Speed_Lv = m_temp_DefaultData.Bless[2];
                GameManager.Inst.StageManager.player.Core.CoreUnitStats.BlessStats.Bless_Critical_Lv = m_temp_DefaultData.Bless[3];
                GameManager.Inst.StageManager.player.Core.CoreUnitStats.BlessStats.Bless_Elemental_Lv = m_temp_DefaultData.Bless[4];
            }
            return m_temp_DefaultData;
        }
        set
        {
            m_temp_DefaultData = value;
        }
    }
    private JSON_DefaultData m_temp_DefaultData = new JSON_DefaultData();
    public List<int> lockItemList = new List<int>();
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
                try
                {
                    byte[] data = new byte[stream.Length];
                    stream.Read(data, 0, data.Length);
                    stream.Close();
                    string jsonData = Encoding.UTF8.GetString(data);
                    JSON_Inventory jtest2 = JsonConvert.DeserializeObject<JSON_Inventory>(jsonData, serializerSettings);
                    jtest2.Print();
                    m_JSON_Inventory = jtest2;
                }
                catch
                {
                    stream.Close();
                    stream = new FileStream(Application.dataPath + UnitInventoryData_FilePath, FileMode.Open, FileAccess.ReadWrite);
                    byte[] data = new byte[stream.Length];
                    stream.Read(data, 0, data.Length);
                    stream.Close();
                    string jsonData = Encoding.UTF8.GetString(data);
                    JSON_Inventory jtest2 = JsonConvert.DeserializeObject<JSON_Inventory>(jsonData, serializerSettings);
                    jtest2.Print();
                    m_JSON_Inventory = jtest2;
                }
            }
            else
            {
                FileStream stream = new FileStream(Application.dataPath + UnitInventoryData_FilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                if (m_JSON_Inventory == null)
                {

                    m_JSON_Inventory = new JSON_Inventory();
                }
                string jsonData = JsonConvert.SerializeObject(m_JSON_Inventory);
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
                try
                {
                    byte[] data = new byte[stream.Length];
                    stream.Read(data, 0, data.Length);
                    stream.Close();
                    string jsonData = Encoding.UTF8.GetString(data);
                    JSON_Goods jtest2 = JsonConvert.DeserializeObject<JSON_Goods>(jsonData, serializerSettings);
                    jtest2.Print();
                    m_JSON_Goods = jtest2;
                }
                catch
                {
                    stream.Close();
                    stream = new FileStream(Application.dataPath + UnitGoodsData_FilePath, FileMode.Open, FileAccess.ReadWrite);
                    byte[] data = new byte[stream.Length];
                    stream.Read(data, 0, data.Length);
                    stream.Close();
                    string jsonData = Encoding.UTF8.GetString(data);
                    JSON_Goods jtest2 = JsonConvert.DeserializeObject<JSON_Goods>(jsonData, serializerSettings);
                    jtest2.Print();
                    m_JSON_Goods = jtest2;
                }
            }
            else
            {
                FileStream stream = new FileStream(Application.dataPath + UnitGoodsData_FilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                if (m_JSON_Goods == null)
                {
                    m_JSON_Goods = new JSON_Goods();
                }
                string jsonData = JsonConvert.SerializeObject(m_JSON_Goods);
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
                try
                {
                    byte[] data = new byte[stream.Length];
                    stream.Read(data, 0, data.Length);
                    stream.Close();
                    string jsonData = Encoding.UTF8.GetString(data);
                    JSON_SceneData json = JsonConvert.DeserializeObject<JSON_SceneData>(jsonData, serializerSettings);
                    json.Print();
                    m_JSON_SceneData = json;
                }
                catch
                {
                    stream.Close();
                    stream = new FileStream(Application.dataPath + SceneData_FilePath, FileMode.Open, FileAccess.ReadWrite);
                    byte[] data = new byte[stream.Length];
                    stream.Read(data, 0, data.Length);
                    stream.Close();
                    string jsonData = Encoding.UTF8.GetString(data);
                    JSON_SceneData json = JsonConvert.DeserializeObject<JSON_SceneData>(jsonData, serializerSettings);
                    json.Print();
                    m_JSON_SceneData = json;
                }
            }
            else
            {
                FileStream stream = new FileStream(Application.dataPath + SceneData_FilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                if (m_JSON_SceneData == null)
                {
                    m_JSON_SceneData = new JSON_SceneData();
                }
                string jsonData = JsonConvert.SerializeObject(m_JSON_SceneData);
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
                try
                {
                    byte[] data = new byte[stream.Length];
                    stream.Read(data, 0, data.Length);
                    stream.Close();
                    string jsonData = Encoding.UTF8.GetString(data);

                    JSON_DefaultData jtest2 = JsonConvert.DeserializeObject<JSON_DefaultData>(jsonData, serializerSettings);
                    jtest2.Print();
                    m_JSON_DefaultData = jtest2;
                }
                catch
                {
                    stream.Close();
                    stream = new FileStream(Application.dataPath + DefaultData_FilePath, FileMode.Open, FileAccess.ReadWrite);
                    byte[] data = new byte[stream.Length];
                    stream.Read(data, 0, data.Length);
                    stream.Close();
                    string jsonData = Encoding.UTF8.GetString(data);
                    JSON_DefaultData jtest2 = JsonConvert.DeserializeObject<JSON_DefaultData>(jsonData, serializerSettings);
                    jtest2.Print();
                    m_JSON_DefaultData = jtest2;
                }
            }
            else
            {
                if (m_JSON_DefaultData == null)
                    m_JSON_DefaultData = new JSON_DefaultData();
                FileStream stream = new FileStream(Application.dataPath + DefaultData_FilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                string jsonData = JsonConvert.SerializeObject(m_JSON_DefaultData);
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
                JSON_Inventory json = JsonConvert.DeserializeObject<JSON_Inventory>(jsonData, serializerSettings);
                json.Print();
                m_JSON_Inventory = json;
                return json;
            }
            else
            {
                FileStream stream = new FileStream(Application.dataPath + UnitInventoryData_FilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                JSON_Inventory json = new JSON_Inventory();
                m_JSON_Inventory = json;
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
                JSON_Goods json = JsonConvert.DeserializeObject<JSON_Goods>(jsonData, serializerSettings);
                json.Print();
                m_JSON_Goods = json;
                return json;
            }
            else
            {
                FileStream stream = new FileStream(Application.dataPath + UnitGoodsData_FilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                JSON_Goods json = new JSON_Goods();
                m_JSON_Goods = json;
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
                JSON_SceneData json = JsonConvert.DeserializeObject<JSON_SceneData>(jsonData, serializerSettings);
                json.Print();
                m_JSON_SceneData = json;
                return json;
            }
            else
            {
                FileStream stream = new FileStream(Application.dataPath + SceneData_FilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                JSON_SceneData json = new JSON_SceneData();
                m_JSON_SceneData = json;
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
                JSON_DefaultData json = JsonConvert.DeserializeObject<JSON_DefaultData>(jsonData, serializerSettings);
                json.Print();
                m_JSON_DefaultData = json;
                return json;
            }
            else
            {
                FileStream stream = new FileStream(Application.dataPath + DefaultData_FilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                JSON_DefaultData json = new JSON_DefaultData();
                m_JSON_DefaultData = json;
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
        try
        {
            m_JSON_Inventory.Print();
            if (!Directory.Exists(Application.dataPath + UnitData_DirectoryPath))
            {
                Directory.CreateDirectory(Application.dataPath + UnitData_DirectoryPath);
            }
            if (File.Exists(Application.dataPath + UnitInventoryData_FilePath))
            {
                string jsonData = JsonUtility.ToJson(m_JSON_Inventory);
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
        try
        {
            m_JSON_Goods.Print();
            if (!Directory.Exists(Application.dataPath + UnitData_DirectoryPath))
            {
                Directory.CreateDirectory(Application.dataPath + UnitData_DirectoryPath);
            }
            if (File.Exists(Application.dataPath + UnitGoodsData_FilePath))
            {
                string jsonData = JsonUtility.ToJson(m_JSON_Goods);
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
        m_JSON_SceneData.PlayTime = GameManager.Inst.PlayTime;
        try
        {
            m_JSON_SceneData.Print();
            if (!Directory.Exists(Application.dataPath + UnitData_DirectoryPath))
            {
                Directory.CreateDirectory(Application.dataPath + UnitData_DirectoryPath);
            }
            if (File.Exists(Application.dataPath + SceneData_FilePath))
            {
                string jsonData = JsonUtility.ToJson(m_JSON_SceneData);
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
        try
        {
            m_JSON_DefaultData.Print();
            if (!Directory.Exists(Application.dataPath + UnitData_DirectoryPath))
            {
                Directory.CreateDirectory(Application.dataPath + UnitData_DirectoryPath);
            }
            if (File.Exists(Application.dataPath + DefaultData_FilePath))
            {
                string jsonData = JsonUtility.ToJson(m_JSON_DefaultData);
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
