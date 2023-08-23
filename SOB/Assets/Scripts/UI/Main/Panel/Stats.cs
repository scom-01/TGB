using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Components;

public class Stats : MonoBehaviour
{
    public virtual float variable { get; private set; }
    public virtual string TypeStr { get; private set; }
    public LocalizeStringEvent LocalStringEvent;

    [ContextMenu("Set Init")]
    public void Init()
    {
        if (LocalStringEvent == null)
            LocalStringEvent = this.GetComponentInChildren<LocalizeStringEvent>();
    }
}
