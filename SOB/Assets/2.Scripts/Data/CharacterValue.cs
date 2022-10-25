using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class CharacterValue : MonoBehaviour
{
    private CharacterValue Inst = null;

    private string DebugStr = "";

    /*Param~*/
    [SerializeField]
    private bool     CanJump     = true;
    [SerializeField]
    private bool     CanMove     = true;
    [SerializeField]
    private bool     CanAttack   = true;
    [SerializeField]
    private bool     Death       = false;
    [SerializeField]
    private bool     CanTakeDamage   = true;

    private float    JumpPower   = 1.0f;     /*Max = 5.0,    Min = 0.0*/
    private float    MoveSpeed   = 1.0f;     /*Max = 3.0,    Min = 0.0*/
    private float    AttackSpeed = 1.0f;     /*Max = 10.0,   Min = 0.0*/
    private int      AttackDamage = 5;       /*Min = 0*/
    private int      Health      = 5;        /*Max = 100,    Min = 0*/
    private float    CriticalPer = 10.0f;    /*Max = 100.0,  Min = 0.0*/
    private float    EnhancePer  = 100.0f;   /*Max = 100.0,    Min = 0.0*/
    /*~Param*/

    private void Awake()
    {
/*        if (Inst == null)
        {
            Inst = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }*/
    }

/*    public CharacterValue Instance
    {
        get
        {
            if (null == Inst)
            {
                return null;
            }
            return Inst;
        }
    }*/

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
