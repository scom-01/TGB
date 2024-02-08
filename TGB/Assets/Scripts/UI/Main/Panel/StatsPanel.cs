using SCOM.CoreSystem;
using UnityEngine;

public class StatsPanel : MonoBehaviour
{
    protected Player Player;

    private UnitStats unitStats;

    private void Start()
    {
        Player = GameManager.Inst.StageManager?.player;
        if (Player)
        {
            unitStats = Player.Core.CoreUnitStats;
        }
    }
    // Start is called before the first frame update
    void OnEnable()
    {
        if (GameManager.Inst.StageManager)
        {
            Player = GameManager.Inst.StageManager.player;
        }
    }
}
