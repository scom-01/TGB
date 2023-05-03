using SOB.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class StatsPanel : MonoBehaviour
{
    protected Player Player;

    private UnitStats unitStats;

    [SerializeField] private TextMeshProUGUI CurrentHealthText;
    [SerializeField] private Image CurrentHealthFillImg;

    [SerializeField] private TextMeshProUGUI MovementVelocityStat;
    [SerializeField] private TextMeshProUGUI JumpVelocityStat;
    [SerializeField] private TextMeshProUGUI DefaultPowerStat;
    [SerializeField] private TextMeshProUGUI AttackSpeedPerStat;
    [SerializeField] private TextMeshProUGUI PhysicsDefensivePerStat;
    [SerializeField] private TextMeshProUGUI MagicDefensivePerStat;
    [SerializeField] private TextMeshProUGUI PhysicsAggressivePerStat;
    [SerializeField] private TextMeshProUGUI MagicAggressivePerStat;
    [SerializeField] private TextMeshProUGUI ElementalDefensivePerStat;
    [SerializeField] private TextMeshProUGUI ElementalAggressivePerStat;


    private void Start()
    {
        Player = GameManager.Inst.StageManager?.player;
        if (Player)
        {
            unitStats = Player.Core.GetCoreComponent<UnitStats>();
        }
    }
    // Start is called before the first frame update
    void OnEnable()
    {
        if (GameManager.Inst.StageManager)
        {
            Player = GameManager.Inst.StageManager.player;
        }
        if (Player)
        {
            UpdateStat();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Player)
        {
            UpdateStat();
        }
        else
        {
            Player = GameManager.Inst.StageManager?.player;
            if (Player)
            {
                unitStats = Player.Core.GetCoreComponent<UnitStats>();
                unitStats.StatsData = Player.Core.GetCoreComponent<UnitStats>().StatsData;
            }
            
        }
    }

    private void UpdateStat()
    {
        if (Player == null)
            return;
        if (Player)
        {
            unitStats = Player.Core.GetCoreComponent<UnitStats>();
            unitStats.StatsData = Player.Core.GetCoreComponent<UnitStats>().StatsData;
        }
        float temp = Player.Core.GetCoreComponent<UnitStats>().CurrentHealth;
        if (CurrentHealthText != null)
            CurrentHealthText.text = temp.ToString("F0") + " / " + unitStats.StatsData.MaxHealth.ToString();

        if (CurrentHealthFillImg != null)
            CurrentHealthFillImg.fillAmount = temp / unitStats.StatsData.MaxHealth;
        temp = 100.0f + unitStats.StatsData.MovementVelocity;
        if (MovementVelocityStat != null)
            MovementVelocityStat.text = " + " + temp.ToString("F1") + "%";
        temp = 100.0f + unitStats.StatsData.JumpVelocity;
        if (JumpVelocityStat != null)
            JumpVelocityStat.text = " + " + temp.ToString("F1") + "%";

        temp = 100.0f + unitStats.StatsData.DefaultPower;
        if (DefaultPowerStat != null)
            DefaultPowerStat.text = " + " + temp.ToString("F1") + "%";

        temp = 100.0f + unitStats.StatsData.AttackSpeedPer;
        if (AttackSpeedPerStat != null)
            AttackSpeedPerStat.text = " + " + temp.ToString("F1") + "%";

        temp = 100.0f + unitStats.StatsData.PhysicsDefensivePer;
        if (PhysicsDefensivePerStat != null)
            PhysicsDefensivePerStat.text = " + " + temp.ToString("F1") + "%";

        temp = 100.0f + unitStats.StatsData.MagicDefensivePer;
        if (MagicDefensivePerStat != null)
            MagicDefensivePerStat.text = " + " + temp.ToString("F1") + "%";

        temp = 100.0f + unitStats.StatsData.PhysicsAggressivePer;
        if (PhysicsAggressivePerStat != null)
            PhysicsAggressivePerStat.text = " + " + temp.ToString("F1") + "%";

        temp = 100.0f + unitStats.StatsData.MagicAggressivePer;
        if (MagicAggressivePerStat != null)
            MagicAggressivePerStat.text = " + " + temp.ToString("F1") + "%";

        temp = 100.0f + unitStats.StatsData.ElementalDefensivePer;
        if (ElementalDefensivePerStat != null)
            ElementalDefensivePerStat.text = " + " + temp.ToString("F1") + "%";

        temp = 100.0f + unitStats.StatsData.ElementalAggressivePer;
        if (ElementalAggressivePerStat != null)
            ElementalAggressivePerStat.text = " + " + temp.ToString("F1") + "%";

    }
}
