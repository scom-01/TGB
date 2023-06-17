using UnityEngine;

public class Enemy : Unit
{
    #region State Variables
    [HideInInspector]
    public EnemyData enemyData;
    #endregion

    #region Unity Callback Func
    protected override void Awake()
    {
        base.Awake();
        enemyData = UnitData as EnemyData;
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        Inventory.Weapon.SetCore(Core);
        Init();
    }

    protected virtual void Init()
    {

    }

    public override void DieEffect()
    {
        if (enemyData.enemy_level == ENEMY_Level.Boss)
        {
            if (DataManager.Inst != null)
            {
                DataManager.Inst.UnLockItemSpawn();
            }
        }
    }

    protected override void Update()
    {
        base.Update();
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
    #endregion
}
