using System;
using System.IO;
using UnityEngine;


public class DataParsing : MonoBehaviour
{
    public string UnitStatData_FilePath;

    // Start is called before the first frame update
    void Start()
    {
        UnitStatData_FilePath = "Json/UnitStatData";
        TextAsset textAsset = Resources.Load<TextAsset>(UnitStatData_FilePath);        
        Debug.Log(textAsset);
        JsonParsing(UnitStatData_FilePath);
    }

    private bool JsonParsing(string jsonPath)
    {
        FileStream fs = new FileStream(jsonPath, FileMode.Open); // 경로에 있는 파일을 열어주고,
        StreamReader stream = new StreamReader(fs);

        string data = stream.ReadToEnd();
        JsonUtility.FromJson<StatsData>(data);
        stream.Close();
        return true;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
