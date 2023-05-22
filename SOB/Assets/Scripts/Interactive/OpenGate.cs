using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenGate : InteractiveObject
{
    public override void Interactive()
    {
        GameManager.Inst.inputHandler.ChangeCurrentActionMap(InputEnum.Cfg, false);
        GameManager.Inst?.ClearScene();
    }
}
