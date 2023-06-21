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
        var item = Inventory._items;
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
            if (enemyData.enemy_level == ENEMY_Level.Boss)
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
            GameManager.Inst.StageManager.SPM.UIEnemyCount--;
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
