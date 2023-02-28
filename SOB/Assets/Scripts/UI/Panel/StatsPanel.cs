using SOB.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsPanel : MonoBehaviour
{
    protected Player Player;

    [SerializeField] private TextMeshProUGUI CurrentHealthText;
    [SerializeField] private Image CurrentHealthFillImg;

    [SerializeField] private TextMeshProUGUI AttackSpeedStat;
    [SerializeField] private TextMeshProUGUI AttackPowerStat;
    [SerializeField] private TextMeshProUGUI JumpPowerStat;
    [SerializeField] private TextMeshProUGUI MoveSpeedStat;
    [SerializeField] private TextMeshProUGUI ElementalPowerStat;
    [SerializeField] private TextMeshProUGUI DefensivePowerStat;

    private void Awake()
    {
        Player = GameManager.Inst?.player;
    }
    // Start is called before the first frame update
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
}
