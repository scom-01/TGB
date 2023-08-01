using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStageManager : StageManager
{
    public List<Transform> TeleportPoint = new List<Transform>();
    public override void Start()
    {
        base.Start();
        GameManager.Inst.ChangeUI(UI_State.CutScene);
    }
}
