using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CfgBtn : MonoBehaviour
{
    public GameObject ActiveUI;
    public void OnClickActiveUI(bool isShow)
    {
        if(ActiveUI == null)
        {
            Debug.LogWarning(this.name + " ActiveUI is Null");
            return;
        }

        ActiveUI.SetActive(isShow);
    }
}
