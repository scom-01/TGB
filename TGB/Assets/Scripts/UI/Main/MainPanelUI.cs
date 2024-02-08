using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SCOM.Manager
{
    public class MainPanelUI : PanelUI
    {
        [HideInInspector] public BuffPanelSystem BuffPanelSystem;
        [HideInInspector] public EnemyPanel EnemyPanelSystem;
        [HideInInspector] public StatsPanel StatsPanelSystem;
        [HideInInspector] public MinimapPanel MinimapPanelSystem;
        [HideInInspector] public SkillPanelSystem SkillPanelSystem;

        // Start is called before the first frame update
        protected override void Awake()
        {
            base.Awake();
            if (BuffPanelSystem == null)
                BuffPanelSystem = this.GetComponentInChildren<BuffPanelSystem>();
            if (EnemyPanelSystem == null)
                EnemyPanelSystem = this.GetComponentInChildren<EnemyPanel>();
            if (StatsPanelSystem == null)
                StatsPanelSystem = this.GetComponentInChildren<StatsPanel>();
            if (MinimapPanelSystem == null)
                MinimapPanelSystem = this.GetComponentInChildren<MinimapPanel>();
            if (SkillPanelSystem == null)
                SkillPanelSystem = this.GetComponentInChildren<SkillPanelSystem>();
        }

        protected override void Update()
        {
            base.Update();
        }
    }
}
