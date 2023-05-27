using System;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;


public class DataParsing : MonoBehaviour
{
    public string UnitStatData_FilePath;

    // Start is called before the first frame update
    void Start()
    {
        StatsData js = new StatsData();
        js.MaxHealth = 1010;
        string j = JsonUtility.ToJson(js);
        Debug.Log(j);
        var k = JsonUtility.FromJson<StatsData>(j);
        Debug.Log(k);
        JsonParsing(UnitStatData_FilePath);
    }

    private bool JsonParsing(string jsonPath)
    {
        if (!File.Exists(jsonPath))
        {
            Debug.Log("File Path Exception");
            return false;
        }

        FileStream fs = new FileStream(jsonPath, FileMode.Open); // 경로에 있는 파일을 열어주고,
        Debug.Log("Fs = " + fs);
        StreamReader stream = new StreamReader(fs);
        Debug.Log("stream = " + stream);

        string data = stream.ReadToEnd();
        Debug.Log("data = " + data);
        JSON_UnitParsing Player_StatsData = JsonUtility.FromJson<JSON_UnitParsing>(data);        
        Debug.Log("Player_StatsData = " + Player_StatsData.Name);
        Debug.Log("Player_StatsData = " + Player_StatsData.MaxHealth);


        stream.Close();
        return true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    [Serializable]
    public struct JSON_UnitParsing
    {
        public string Name;
        public float MaxHealth;
        [Tooltip("움직임 Velocity")]
        public float MovementVelocity;
        [Tooltip("점프 Velocity")]
        public float JumpVelocity;
        [Tooltip("기본 공격력")]
        public float DefaultPower;
        [Tooltip("추가 공격 속도 %")]
        public float AttackSpeedPer;
        [Tooltip("물리 방어력 % (max = 100)")]
        public float PhysicsDefensivePer;
        [Tooltip("마법 방어력 % (max = 100)")]
        public float MagicDefensivePer;
        [Tooltip("추가 물리공격력 %")]
        public float PhysicsAggressivePer;
        [Tooltip("추가 마법공격력 %")]
        public float MagicAggressivePer;
        [Tooltip("공격 속성")]
        public DAMAGE_ATT DamageAttiribute;

        //-------------------------Elemental Options
        [Header("Elemental Options")]
        [Tooltip("원소 속성")]
        public E_Power MyElemental;
        [Tooltip("원소 저항력 (max = 100)%")]
        public float ElementalDefensivePer;
        [Tooltip("원소 공격력 %")]
        public float ElementalAggressivePer;
    }

}
