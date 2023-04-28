using SOB.CoreSystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsPanel : MonoBehaviour
{
    protected Player Player;

    private StatsData stats;

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
            stats = Player.Core.GetCoreComponent<UnitStats>().StatsData;
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
                stats = Player.Core.GetCoreComponent<UnitStats>().StatsData;
            
        }
    }

    private void UpdateStat()
    {
        if (Player == null)
            return;

        float temp = Player.Core.GetCoreComponent<UnitStats>().CurrentHealth;
        if (CurrentHealthText != null)
            CurrentHealthText.text = temp.ToString("F0") + " / " + stats.MaxHealth.ToString();

        if (CurrentHealthFillImg != null)
            CurrentHealthFillImg.fillAmount = temp / stats.MaxHealth;
        temp = 100.0f + stats.MovementVelocity;
        if (MovementVelocityStat != null)
            MovementVelocityStat.text = " + " + temp.ToString("F1") + "%";
        temp = 100.0f + stats.JumpVelocity;
        if (JumpVelocityStat != null)
            JumpVelocityStat.text = " + " + temp.ToString("F1") + "%";

        temp = 100.0f + stats.DefaultPower;
        if (DefaultPowerStat != null)
            DefaultPowerStat.text = " + " + temp.ToString("F1") + "%";

        temp = 100.0f + stats.AttackSpeedPer;
        if (AttackSpeedPerStat != null)
            AttackSpeedPerStat.text = " + " + temp.ToString("F1") + "%";

        temp = 100.0f + stats.PhysicsDefensivePer;
        if (PhysicsDefensivePerStat != null)
            PhysicsDefensivePerStat.text = " + " + temp.ToString("F1") + "%";

        temp = 100.0f + stats.MagicDefensivePer;
        if (MagicDefensivePerStat != null)
            MagicDefensivePerStat.text = " + " + temp.ToString("F1") + "%";

        temp = 100.0f + stats.PhysicsAggressivePer;
        if (PhysicsAggressivePerStat != null)
            PhysicsAggressivePerStat.text = " + " + temp.ToString("F1") + "%";

        temp = 100.0f + stats.MagicAggressivePer;
        if (MagicAggressivePerStat != null)
            MagicAggressivePerStat.text = " + " + temp.ToString("F1") + "%";

        temp = 100.0f + stats.ElementalDefensivePer;
        if (ElementalDefensivePerStat != null)
            ElementalDefensivePerStat.text = " + " + temp.ToString("F1") + "%";

        temp = 100.0f + stats.ElementalAggressivePer;
        if (ElementalAggressivePerStat != null)
            ElementalAggressivePerStat.text = " + " + temp.ToString("F1") + "%";

    }
}
