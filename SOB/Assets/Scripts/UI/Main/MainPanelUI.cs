using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOB.Manager
{
    public class MainPanelUI : MonoBehaviour
    {
        [HideInInspector] public BuffPanelSystem BuffPanelSystem;
        [HideInInspector] public EnemyPanel EnemyPanelSystem;
        [HideInInspector] public StatsPanel StatsPanelSystem;
        [HideInInspector] public MinimapPanel MinimapPanelSystem;
        // Start is called before the first frame update
        void Awake()
        {
            if (BuffPanelSystem == null)
                BuffPanelSystem = this.GetComponentInChildren<BuffPanelSystem>();
            if (EnemyPanelSystem == null)
                EnemyPanelSystem = this.GetComponentInChildren<EnemyPanel>();
            if (StatsPanelSystem == null)
                StatsPanelSystem = this.GetComponentInChildren<StatsPanel>();
            if (MinimapPanelSystem == null)
                MinimapPanelSystem = this.GetComponentInChildren<MinimapPanel>();

        }
    }
}
