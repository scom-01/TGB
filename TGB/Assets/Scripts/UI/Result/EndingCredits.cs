using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingCredits : MonoBehaviour
{
    [SerializeField] 
    private Scrollbar Scrollbar;
    [SerializeField]
    private float creditSpeed;

    [SerializeField]
    private bool isInit = false;
    private bool isSkip;
    private void Awake()
    {
        Scrollbar = this.GetComponent<Scrollbar>();
        if (Scrollbar != null)
        {
            Scrollbar.direction = Scrollbar.Direction.TopToBottom;
            Scrollbar.value = 1;
        }
        Init();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isInit)
            return;

        if (Scrollbar == null)
            return;

        if (Scrollbar.value > 0f)
        {
            isSkip = GameManager.Inst.InputHandler.InteractionInput ? true : false;
            if(isSkip)
            {
                Debug.Log("Skip");
            }
            Scrollbar.value -= Time.fixedDeltaTime * creditSpeed * (isSkip ? 10f : 1f) ;
            if(Scrollbar.value <= 0f)
            {
                Scrollbar.value = 0f;                
                StartCoroutine(GoTitle());
            }
        }
    }
    void Init()
    {
        GameManager.Inst.InputHandler.ChangeCurrentActionMap(InputEnum.CutScene, true);
    }

    IEnumerator GoTitle()
    {
        yield return new WaitForSecondsRealtime(2.0f);
        //GameManager.Inst.GoTitleScene();
        GameManager.Inst.ChangeUI(UI_State.Result);
        GameManager.Inst.InputHandler.ChangeCurrentActionMap(InputEnum.Cfg, false);
        GameManager.Inst.ResetData();
        GameManager.Inst.ResultUI.resultPanel.UpdateResultPanel();
        yield return null;
    }
}
