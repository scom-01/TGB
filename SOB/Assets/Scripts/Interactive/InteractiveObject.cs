using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractiveObject : MonoBehaviour, IInteractive
{
    public LayerMask Interactive_Layer;
    public GameObject BtnObj;

    private void Start()
    {
        SetActiveBtnObj(false);
    }

    public virtual void SetActiveBtnObj(bool isShow)
    {
        if (BtnObj != null)
        {
            BtnObj.gameObject.SetActive(isShow);
        }
    }
    public virtual void Interactive()
    {

    }

    public virtual void UnInteractive()
    {
    }
}
