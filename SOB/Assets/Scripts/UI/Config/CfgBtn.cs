using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CfgBtn : MonoBehaviour
{
    public GameObject ActiveUI;
    public void OnClickActiveUI(bool _isShow)
    {
        if(ActiveUI == null)
        {
            Debug.LogWarning(this.name + " ActiveUI is Null");
            return;
        }

        ActiveUI.GetComponent<Canvas>().enabled = _isShow;
    }
}
