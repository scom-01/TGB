using SOB.CoreSystem;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace SOB.Manager
{
    public class MainUIManager : MonoBehaviour
    {
        [HideInInspector] public BuffPanelSystem BuffPanelSystem;
        [HideInInspector] public EnemyPanel EnemyPanelSystem;
        [HideInInspector] public StatsPanel StatsPanelSystem;
        [HideInInspector] public MinimapPanel MinimapPanelSystem;

        private void Awake()
        {
            BuffPanelSystem = this.GetComponentInChildren<BuffPanelSystem>();
            EnemyPanelSystem = this.GetComponentInChildren<EnemyPanel>();
            StatsPanelSystem = this.GetComponentInChildren<StatsPanel>();
            MinimapPanelSystem = this.GetComponentInChildren<MinimapPanel>();
        }

        // Use this for initialization
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}