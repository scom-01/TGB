using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalValue : MonoBehaviour
{
    private static GlobalValue Inst = null;

    /*Param*/
    private static bool GV_Pause = false;

    private static float GV_SystemDeltatime = 0.02f;
    private static float GV_CharacterDeltatime = 0.02f;

    /*~Param*/

    private void Awake()
    {
        if (Inst == null)
        {
            Inst = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static GlobalValue Instance
    {
        get
        {
            if (null == Inst)
            {
                return null;
            }
            return Inst;
        }
    }

    /*func*/
    public bool GetPause() { return GV_Pause; }
    public void SetPause(bool pause)
    {
        if (GV_Pause != pause)
        {
            GV_Pause = pause;
        }
    }

    public float GetSystemDelta() { return GV_SystemDeltatime; }
    public void SetSystemDelta(float deltatime)
    {
        if (GV_SystemDeltatime != deltatime)
        {
            GV_SystemDeltatime = deltatime;
        }
    }
    public float GetCharacterDelta() { return GV_CharacterDeltatime; }
    public void SetCharacterDelta(float deltatime)
    {
        if(GV_CharacterDeltatime != deltatime)
        {
            GV_CharacterDeltatime = deltatime;
        }
    }
    
    /*~func*/
}

