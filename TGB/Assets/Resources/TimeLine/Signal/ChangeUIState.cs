using UnityEngine;


/// <summary>
/// GameManager의 ChangeUI를 Signal Receiver에서 사용하기 위함
/// </summary>
public class ChangeUIState : MonoBehaviour
{
    public void ChangeUI_State(UI_State ui)
    {
        if (GameManager.Inst == null)
            return;
        GameManager.Inst.ChangeUI(ui);
    }

    public void ChangeUI_GamePlay()
    {
        if (GameManager.Inst == null)
            return;
        GameManager.Inst.ChangeUI(UI_State.GamePlay);
    }
    public void ChangeUI_Cfg()
    {
        if (GameManager.Inst == null)
            return;
        GameManager.Inst.ChangeUI(UI_State.Cfg);
    }
    public void ChangeUI_CutScene()
    {
        if (GameManager.Inst == null)
            return;
        GameManager.Inst.ChangeUI(UI_State.CutScene);
    }
    public void ChangeUI_Loading()
    {
        if (GameManager.Inst == null)
            return;
        GameManager.Inst.ChangeUI(UI_State.Loading);
    }
}
