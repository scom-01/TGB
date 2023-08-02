using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStageManager : StageManager
{
    public List<Transform> TeleportPoint = new List<Transform>();
    public List<GameObject> Pattern = new List<GameObject>();
    public override void Start()
    {
        base.Start();
        GameManager.Inst.ChangeUI(UI_State.GamePlay);
    }

    public void PlayPattern(GameObject Pattern)
    {
        if (Pattern == null)
            return;

        Instantiate(Pattern, this.transform);
    }
}
