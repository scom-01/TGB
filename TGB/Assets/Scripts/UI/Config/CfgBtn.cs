using SCOM.Manager;
using UnityEngine;

public class CfgBtn : MonoBehaviour
{
    public GameObject ActiveUI;

    public void ActiveUIOn()
    {
        ActiveUI.GetComponent<Canvas>().enabled = true;
        GameManager.Inst.InputHandler.OnESCInput_MultiAction.Add(() => OnClickActiveUI(false));
    }
    public void OnClickActiveUI(bool _isShow = false)
    {
        if(ActiveUI == null)
        {
            Debug.LogWarning(this.name + " ActiveUI is Null");
            return;
        }

        foreach (var setting in ActiveUI.GetComponentsInChildren<SettingUI>())
        {
            setting.Canvas.enabled = _isShow;
        }        
        ActiveUI.GetComponent<Canvas>().enabled = _isShow;
    }
}
