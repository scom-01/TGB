using SOB.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Melee1 : Enemy
{
    #region State Variables
    public Enemy_Melee1_AttackState AttackState { get; private set; }
    public Enemy_Melee1_IdleState IdleState { get; private set; }
    public Enemy_Melee1_MoveState RunState { get; private set; }
    public Enemy_Melee1_HitState HitState { get; private set; }
    public Enemy_Melee1_DeathState DeathState { get; private set; }
    #endregion

    #region Unity Callback Func
    protected override void Awake()
    {
        base.Awake();

        AttackState = new Enemy_Melee1_AttackState(this, "action");
        IdleState = new Enemy_Melee1_IdleState(this, "idle");
        RunState = new Enemy_Melee1_MoveState(this, "run");
        HitState = new Enemy_Melee1_HitState(this, "hit");
        DeathState = new Enemy_Melee1_DeathState(this, "death");
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
    #endregion

    public override void HitEffect()
    {
        base.HitEffect();
        if(!isCCimmunity)
        {
            FSM.ChangeState(HitState);
        }
    }

    public override void DieEffect()
    {
        base.DieEffect();
        if (GameManager.Inst != null)
            GameManager.Inst.StageManager.SPM.UIEnemyCount--;
        FSM.ChangeState(DeathState);
    }

    protected override void Init()
    {
        base.Init();
        FSM.Initialize(IdleState);
    }
}

