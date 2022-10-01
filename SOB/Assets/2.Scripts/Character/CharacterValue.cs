using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterValue : MonoBehaviour
{
    private static CharacterValue Inst = null;

    /*Param*/
    private static bool     CanJump     = true;
    private static bool     CanMove     = true;
    private static bool     CanAttack   = true;
    private static bool     Death       = false;

    private static float    JumpPower   = 1.0f; /*Max = 5.0,    Min = 0.0*/
    private static float    MoveSpeed   = 1.0f; /*Max = 3.0,    Min = 0.0*/
    private static float    AttackSpeed = 1.0f; /*Max = 10.0,   Min = 0.0*/
    private static int      AttackDamage = 5;   /*Min = 0*/
    private static int      Health      = 5;    /*Max = 100,    Min = 0*/
    private static float    CriticalPer = 10.0f;/*Max = 100.0,  Min = 0.0*/
    private static float    EnhancePer  = 100.0f;/*Max = 100.0,    Min = 0.0*/


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

    public static CharacterValue Instance
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
    public bool GetJump() { return CanJump; }
    public void SetJump(bool jump)
    {
        if (CanJump != jump)
        {
            CanJump = jump;
        }
    }

    public bool GetMove() { return CanMove; }
    public void SetMove(bool move)
    {
        if (CanMove != move)
        {
            CanMove = move;
        }
    }

    public bool GetAttack() { return CanAttack; }
    public void SetAttack(bool attack)
    {
        if (CanAttack != attack)
        {
            CanAttack = attack;
        }
    }

    public bool GetDeath() { return Death; }
    public void SetDeath(bool death)
    {
        if (Death != death)
        {
            Death = death;
        }
    }
}
