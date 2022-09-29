using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalValue : MonoBehaviour
{
    private static GlobalValue Inst = null;

    /*Param*/
    public static bool GV_CanJump = true;
    public static bool GV_CanMove = true;
    public static bool GV_CanAttack = true;
    public static bool GV_Death = false;
    public static bool GV_Pause = false;

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
    public bool GetJump() { return GV_CanJump; }
    public void SetJump(bool jump)
    {
        if (GV_CanJump != jump)
        {
            GV_CanJump = jump;
        }
    }

    public bool GetMove() { return GV_CanMove; }
    public void SetMove(bool move)
    {
        if (GV_CanMove != move)
        {
            GV_CanMove = move;
        }
    }

    public bool GetAttack() { return GV_CanAttack; }
    public void SetAttack(bool attack)
    {
        if (GV_CanAttack != attack)
        {
            GV_CanAttack = attack;
        }
    }

    public bool GetDeath() { return GV_Death; }
    public void SetDeath(bool death)
    {
        if (GV_Death != death)
        {
            GV_Death = death;
        }
    }

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

