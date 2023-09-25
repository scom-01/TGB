using SOB.CoreSystem;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.Util;

[Serializable]
public class Pattern_Data
{
    /// <summary>
    /// 패턴 사용 여부
    /// </summary>
    public bool Used = false;
    /// <summary>
    /// 패턴의 감지 범위 (적과의 점 By 점 길이)
    /// </summary>
    public float Detected_Distance;
    /// <summary>
    /// 현재 패턴의 체력 경계선(이 이하면 해당 패턴 벗어남)
    /// Of이면 패턴의 경계를 나누지 않음
    /// </summary>
    [Min(0f)]
    [Tooltip("Of이면 패턴의 경계를 나누지 않음")]
    public float Boundary;
}

public class Enemy : Unit
{
    #region State Variables
    [HideInInspector]
    public EnemyData enemyData;

    [SerializeField] private float Test_Distance;
    [SerializeField] public List<Pattern_Data> Pattern_Idx;
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
    public virtual void EnemyPattern() { }
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
                    DataManager.Inst.JSON_DataParsing.m_JSON_SceneData.EnemyCount.Normal_Enemy_Count++;
                    break;
                case ENEMY_Level.EleteEnemy:
                    DataManager.Inst.JSON_DataParsing.m_JSON_SceneData.EnemyCount.Elete_Enemy_Count++;
                    break;
                case ENEMY_Level.BossEnemy:
                    DataManager.Inst.JSON_DataParsing.m_JSON_SceneData.EnemyCount.Boss_Enemy_Count++;
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(Core.CoreCollisionSenses.GroundCenterPos + new Vector3(0, CC2D.offset.y, 0),
            Core.CoreCollisionSenses.GroundCenterPos + new Vector3(0, CC2D.offset.y, 0) + Vector3.right * Core.CoreMovement.FancingDirection * Test_Distance);
    }
}
