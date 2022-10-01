using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalValue : MonoBehaviour
{
    private static GlobalValue Inst = null;

    /*Param*/
    private static bool GV_Pause = false;

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
    /*~func*/
}

