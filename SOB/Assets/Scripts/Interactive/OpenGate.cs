using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenGate : InteractiveObject
{
    private bool isDone = false;

    private void Awake()
    {
        isDone = false;
    }
    public override void Interactive()
    {
        if (isDone)
            return;

        Debug.LogWarning("OpenGate");
        GameManager.Inst.InputHandler.ChangeCurrentActionMap(InputEnum.Cfg, false);
        GameManager.Inst?.ClearScene();
        isDone = true;
    }
}
