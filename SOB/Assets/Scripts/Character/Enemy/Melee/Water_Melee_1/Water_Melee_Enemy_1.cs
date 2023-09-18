using SOB.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water_Melee_Enemy_1 : Enemy
{
    #region State Variables
    public Water_Melee_Enemy_1_AttackState AttackState { get; private set; }
    public Water_Melee_Enemy_1_IdleState IdleState { get; private set; }
    public Water_Melee_Enemy_1_MoveState RunState { get; private set; }
    public Water_Melee_Enemy_1_HitState HitState { get; private set; }
    public Water_Melee_Enemy_1_DeathState DeathState { get; private set; }
    #endregion

    #region Unity Callback Func
    protected override void Awake()
    {
        base.Awake();

        AttackState = new Water_Melee_Enemy_1_AttackState(this, "action");
        IdleState = new Water_Melee_Enemy_1_IdleState(this, "idle");
        RunState = new Water_Melee_Enemy_1_MoveState(this, "run");
        HitState = new Water_Melee_Enemy_1_HitState(this, "hit");
        DeathState = new Water_Melee_Enemy_1_DeathState(this, "death");
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
        if (!isCC_immunity)
        {
            FSM.ChangeState(HitState);
        }
    }

    public override void DieEffect()
    {
        base.DieEffect();
        FSM.ChangeState(DeathState);
    }

    protected override void Init()
    {
        base.Init();
        FSM.Initialize(IdleState);
    }
}
