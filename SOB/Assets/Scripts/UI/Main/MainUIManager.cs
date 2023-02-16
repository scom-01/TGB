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
        protected Player Player;

        [SerializeField] private TextMeshProUGUI CurrentHealthText;
        [SerializeField] private Image CurrentHealthFillImg;

        [SerializeField]    private TextMeshProUGUI AttackSpeedStat;
        [SerializeField]    private TextMeshProUGUI AttackPowerStat;
        [SerializeField]    private TextMeshProUGUI JumpPowerStat;
        [SerializeField]    private TextMeshProUGUI MoveSpeedStat;
        [SerializeField]    private TextMeshProUGUI ElementalPowerStat;
        [SerializeField]    private TextMeshProUGUI DefensivePowerStat;

        [HideInInspector] public BuffPanelSystem BuffPanelSystem;

        private void Awake()
        {
            Player = GameManager.Inst?.player;
            BuffPanelSystem = this.GetComponentInChildren<BuffPanelSystem>();
        }

        // Use this for initialization
        void Start()
        {
            Player = GameManager.Inst?.player;
            if (Player)
            {
                float temp = Player.Core.GetCoreComponent<UnitStats>().CurrentHealth;
                CurrentHealthText.text = temp.ToString("F0") + " / " + Player.Core.GetCoreComponent<UnitStats>().StatsData.MaxHealth.ToString();
                CurrentHealthFillImg.fillAmount = temp / Player.Core.GetCoreComponent<UnitStats>().StatsData.MaxHealth;

                temp = 100.0f + Player.Core.GetCoreComponent<UnitStats>().StatsData.AttackSpeedPer;
                AttackSpeedStat.text = " + " + temp.ToString("F1") + "%";
                temp = 100.0f + Player.Core.GetCoreComponent<UnitStats>().StatsData.DefaultPower;
                AttackPowerStat.text = " + " + temp.ToString("F1") + "%";
                temp = 100.0f + Player.Core.GetCoreComponent<UnitStats>().StatsData.MovementVelocity;
                MoveSpeedStat.text = " + " + temp.ToString("F1") + "%";
                temp = 100.0f + Player.Core.GetCoreComponent<UnitStats>().StatsData.ElementalAggressivePer;
                ElementalPowerStat.text = " + " + temp.ToString("F1") + "%";
                temp = 100.0f + Player.Core.GetCoreComponent<UnitStats>().StatsData.PhysicsDefensivePer;
                DefensivePowerStat.text = " + " + temp.ToString("F1") + "%";
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (Player)
            {
                float temp = Player.Core.GetCoreComponent<UnitStats>().CurrentHealth;
                CurrentHealthText.text = temp.ToString("F0")+ " / "+ Player.Core.GetCoreComponent<UnitStats>().StatsData.MaxHealth.ToString();
                CurrentHealthFillImg.fillAmount = temp / Player.Core.GetCoreComponent<UnitStats>().StatsData.MaxHealth;

                temp = 100.0f + Player.Core.GetCoreComponent<UnitStats>().StatsData.AttackSpeedPer;
                AttackSpeedStat.text = " + " + temp.ToString("F1") + "%";
                temp = 100.0f + Player.Core.GetCoreComponent<UnitStats>().StatsData.DefaultPower;
                AttackPowerStat.text = " + " + temp.ToString("F1") + "%";
                temp = 100.0f + Player.Core.GetCoreComponent<UnitStats>().StatsData.MovementVelocity;
                MoveSpeedStat.text = " + " + temp.ToString("F1") + "%";
                temp = 100.0f + Player.Core.GetCoreComponent<UnitStats>().StatsData.ElementalAggressivePer;
                ElementalPowerStat.text = " + " + temp.ToString("F1") + "%";
                temp = 100.0f + Player.Core.GetCoreComponent<UnitStats>().StatsData.PhysicsDefensivePer;
                DefensivePowerStat.text = " + " + temp.ToString("F1") + "%";
            }
            //Player = GameManager.Inst?.player;
            //AttackSpeedStat.text = " + " + (100.0f + Player?.GetComponent<Player>().playerData.commonStats.AttackSpeedPer).ToString() + "%";
        }
    }
}