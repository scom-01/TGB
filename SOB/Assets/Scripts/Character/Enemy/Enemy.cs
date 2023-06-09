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
            //spawnItem
            if (DataManager.Inst != null)
            {
                var idx = Random.Range(0, DataManager.Inst.Lock_ItemDB.ItemDBList.Count);
                var itemData = DataManager.Inst.Lock_ItemDB.ItemDBList[idx];
                if (GameManager.Inst.StageManager.SPM.SpawnItem(GameManager.Inst.StageManager.IM.InventoryItem, transform.position, GameManager.Inst.StageManager.IM.transform, itemData))
                {
                    DataManager.Inst.Lock_ItemDB.ItemDBList.RemoveAt(idx);
                }
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
