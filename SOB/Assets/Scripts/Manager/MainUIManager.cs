using System.Collections;
using TMPro;
using UnityEngine;

namespace SOB.Manager
{
    public class MainUIManager : MonoBehaviour
    {
        private GameObject Player;

        [SerializeField]    private TextMeshProUGUI AttackSpeedStat;
        [SerializeField]    private TextMeshProUGUI AttackPowerStat;
        [SerializeField]    private TextMeshProUGUI JumpPowerStat;
        [SerializeField]    private TextMeshProUGUI MoveSpeedStat;
        [SerializeField]    private TextMeshProUGUI ElementalPowerStat;
        [SerializeField]    private TextMeshProUGUI DefensivePowerStat;

        private void Awake()
        {
            Player = GameManager.Inst?.player;            
        }

        // Use this for initialization
        void Start()
        {
            Player = GameManager.Inst?.player;
            if(Player)
            {
                float temp = 100.0f + Player.GetComponent<Player>().playerData.commonStats.AttackSpeedPer;
                AttackSpeedStat.text = " + " + temp.ToString("F1") + "%";
                temp = 100.0f + Player.GetComponent<Player>().playerData.commonStats.DefaultPower;
                AttackPowerStat.text = " + " + temp.ToString("F1") + "%";
                temp = 100.0f + Player.GetComponent<Player>().playerData.commonStats.MovementVelocity;
                MoveSpeedStat.text = " + " + temp.ToString("F1") + "%";
                temp = 100.0f + Player.GetComponent<Player>().playerData.commonStats.ElementalAggressivePer;
                ElementalPowerStat.text = " + " + temp.ToString("F1") + "%";
                temp = 100.0f + Player.GetComponent<Player>().playerData.commonStats.PhysicsDefensivePer;
                DefensivePowerStat.text = " + " + temp.ToString("F1") + "%";
            }            
        }

        // Update is called once per frame
        void Update()
        {
            //Player = GameManager.Inst?.player;
            //AttackSpeedStat.text = " + " + (100.0f + Player?.GetComponent<Player>().playerData.commonStats.AttackSpeedPer).ToString() + "%";
        }
    }
}