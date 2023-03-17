using UnityEngine;

public class Enemy : Unit
{
    #region State Variables

    //public EnemyIdleState IdleState { get; private set; }
    //public EnemyRunState RunState { get; private set; }
    //public EnemyAttackState AttackState { get; private set; }
    //public EnemyHitState HitState { get; private set; }
    //public EnemyDeadState DeadState { get; private set; }

    [HideInInspector]
    public EnemyData enemyData;
    #endregion

    #region Unity Callback Func
    protected override void Awake()
    {
        base.Awake();
        enemyData = UnitData as EnemyData;

        //IdleState = new EnemyIdleState(this, "idle");
        //RunState = new EnemyRunState(this, "run");
        ////AttackState = new EnemyAttackState(this, "action");
        //HitState = new EnemyHitState(this, "hit");
        //DeadState = new EnemyDeadState(this, "dead");
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        foreach (var weapon in Inventory.weapons)
        {
            weapon.SetCore(Core);
        }
        Init();        
    }

    protected virtual void Init()
    {

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        //Anim.SetFloat("yVelocity", enemyCore.Movement.RB.velocity.y);
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
    #endregion
}
