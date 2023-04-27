using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class OpenGate : InteractiveObject
    {
    public override void Interactive()
    {
        GameManager.Inst?.ClearScene();
    }
}
