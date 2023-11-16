using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BossStageManager : StageManager
{
    public Transform TeleportPoint;
    [HideInInspector] public List<Transform> TeleportPoints = new List<Transform>();
    public List<GameObject> Pattern = new List<GameObject>();
    public override void Start()
    {
        base.Start();
        TeleportPoints = TeleportPoint.GetComponentsInChildren<Transform>().ToList();
        GameManager.Inst.ChangeUI(UI_State.GamePlay);
    }

    public void PlayPattern(GameObject Pattern)
    {
        if (Pattern == null)
            return;

        Instantiate(Pattern, this.transform);
    }
}
