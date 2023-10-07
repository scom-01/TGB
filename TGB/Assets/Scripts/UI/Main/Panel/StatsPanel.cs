using TGB.CoreSystem;
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
