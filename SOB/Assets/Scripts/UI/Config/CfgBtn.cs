using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CfgBtn : MonoBehaviour
{
    public GameObject ActiveUI;
    [SerializeField] private bool isShow;
    public void OnClickActiveUI()
    {
        if(ActiveUI == null)
        {
            Debug.LogWarning(this.name + " ActiveUI is Null");
            return;
        }

        ActiveUI.SetActive(isShow);
    }
    public void OnClickActiveUI(bool _isShow)
    {
        if(ActiveUI == null)
        {
            Debug.LogWarning(this.name + " ActiveUI is Null");
            return;
        }

        ActiveUI.SetActive(_isShow);
    }
}
