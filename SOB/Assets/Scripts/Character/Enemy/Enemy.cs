using SOB.CoreSystem;
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
        var item = Inventory.Items;
        int count = item.Count;

        if(count > 0)
        {
            for (int i = 0; i < count; i++)
            {
                Inventory.RemoveInventoryItem(item[i]);
            }
        }
        else
        {
            if (enemyData.enemy_level == ENEMY_Level.BossEnemy)
            {
                if (DataManager.Inst != null)
                {
                    DataManager.Inst.UnLockItemSpawn(transform.position);
                }
            }
        }

        var goods = this.GetComponentsInChildren<GoodsItem>();
        if (goods != null)
        {
            foreach(var good in goods)
            {
                good.DropGoods();
            }
        }

        if (GameManager.Inst != null)
        {
            GameManager.Inst.StageManager.SPM.UIEnemyCount--;

            switch(enemyData.enemy_level)
            {
                case ENEMY_Level.NormalEnemy:
                    DataManager.Inst.JSON_DataParsing.EnemyCount.Normal_Enemy_Count++;
                    break;
                case ENEMY_Level.EleteEnemy:
                    DataManager.Inst.JSON_DataParsing.EnemyCount.Elete_Enemy_Count++;
                    break;
                case ENEMY_Level.BossEnemy:
                    DataManager.Inst.JSON_DataParsing.EnemyCount.Boss_Enemy_Count++;
                    break;
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
