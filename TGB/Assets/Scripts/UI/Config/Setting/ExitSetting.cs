using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitSetting : MonoBehaviour
{
    public void OnClickExitBtn()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
