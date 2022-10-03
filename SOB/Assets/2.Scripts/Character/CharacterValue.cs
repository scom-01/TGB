using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class CharacterValue : MonoBehaviour
{
    private static CharacterValue Inst = null;

    private static string DebugStr = "";

    /*Param~*/
    private static bool     CanJump     = true;
    private static bool     CanMove     = true;
    private static bool     CanAttack   = true;
    private static bool     Death       = false;
    private static bool     CanTakeDamage   = true;

    private static float    JumpPower   = 1.0f;     /*Max = 5.0,    Min = 0.0*/
    private static float    MoveSpeed   = 1.0f;     /*Max = 3.0,    Min = 0.0*/
    private static float    AttackSpeed = 1.0f;     /*Max = 10.0,   Min = 0.0*/
    private static int      AttackDamage = 5;       /*Min = 0*/
    private static int      Health      = 5;        /*Max = 100,    Min = 0*/
    private static float    CriticalPer = 10.0f;    /*Max = 100.0,  Min = 0.0*/
    private static float    EnhancePer  = 100.0f;   /*Max = 100.0,    Min = 0.0*/
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

    /*func~*/
    public bool GetJump() { return CanJump; }
    public void SetJump(bool jump)
    {
        if (CanJump != jump)
        {
            CanJump = jump;
        }
    }

    public float GetJumpPower() { return JumpPower; }
    public void SetJumpPower(float jumppower)
    {
        JumpPower = Mathf.Clamp(jumppower, 0, 5.0f);
    }

    public bool GetMove() { return CanMove; }
    public void SetMove(bool move)
    {
        if (CanMove != move)
        {
            CanMove = move;
        }
    }

    public float GetMoveSpeed() { return MoveSpeed; }
    public void SetMoveSpeed(float movespeed)
    {
        MoveSpeed = Mathf.Clamp(movespeed, 0, 3.0f);
    }

    public bool GetAttack() { return CanAttack; }
    public void SetAttack(bool attack)
    {
        if (CanAttack != attack)
        {
            CanAttack = attack;
        }
    }

    public float GetAttackSpeed() { return AttackSpeed; }
    public void SetAttackSpeed(float attackspeed)
    {
        AttackSpeed = Mathf.Clamp(attackspeed, 0, 10.0f);
    }
    public int GetAttackDamage() { return AttackDamage; }
    public void SetAttackDamage(int attackdamage)
    {
        AttackDamage = Mathf.Clamp(attackdamage, 0, 5);
    }

    public int GetHealth() { return Health; }
    public void SetHealth(int health)
    {
        Health = Mathf.Clamp(health, 0, 100);
    }

    public float GetCriticalPer() { return CriticalPer; }
    public void SetCriticalPer(float criticalper)
    {
        CriticalPer = Mathf.Clamp(criticalper, 0, 100.0f);
    }

    public float GetEnhancePer() { return EnhancePer; }
    public void SetEnhancePer(float enhanceper)
    {
        EnhancePer = Mathf.Clamp(enhanceper, 0, 100.0f);
    }

    public bool GetDeath() { return Death; }
    public void SetDeath(bool death)
    {
        if (Death != death)
        {
            Death = death;
        }
    }

    public bool GetCanTakeDamage() { return CanTakeDamage; }
    public void SetCanTakeDamage(bool cantakedamage)
    {
        if(CanTakeDamage != cantakedamage)
        {
            CanTakeDamage = cantakedamage;
        }
    }
    /*~func*/

    private void OnGUI()
    {
        GUILayout.Label(DebugStr);
    }

    public void ShowDebug()
    {
        if (DebugStr.Length >= 1)
        {
            DebugStr = "";
        }

        DebugStr += "JumpPower = " + GetJumpPower().ToString() + "\n";
        DebugStr += "MoveSpeed = " + GetMoveSpeed().ToString() + "\n";
        DebugStr += "AttackSpeed = " + GetAttackSpeed().ToString() + "\n";
        DebugStr += "AttackDamage = " + GetAttackDamage().ToString() + "\n";
        DebugStr += "Health = " + GetHealth().ToString() + "\n";
        DebugStr += "CriticalPer = " + GetCriticalPer().ToString() + "\n";
        DebugStr += "EnhancePer = " + GetEnhancePer().ToString() + "\n";
    }
}
