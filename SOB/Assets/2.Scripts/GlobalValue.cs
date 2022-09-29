using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalValue : MonoBehaviour
{
    private static GlobalValue Inst = null;

    public static bool CanJump = true;
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

    public bool GetJump() { return CanJump; }
    public void SetJump(bool jump)
    {
        if (CanJump != jump)
        {
            CanJump = jump;
        }
    }
}

