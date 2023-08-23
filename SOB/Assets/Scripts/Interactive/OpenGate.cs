using UnityEngine;

public class OpenGate : InteractiveObject
{
    private bool isDone = false;

    private void Awake()
    {
        isDone = false;
    }
    protected override void Start()
    {
        base.Start();
    }

    public override void Interactive()
    {
        if (isDone)
            return;

        //씬 이동할 때마다 게임 Idx 설정
        DataManager.Inst.JSON_DataParsing.m_JSON_SceneData.SceneDataIdx = UnityEngine.Random.Range(0, GlobalValue.MaxSceneIdx);

        GameManager.Inst.Data_Save();

        Debug.LogWarning("OpenGate");
        GameManager.Inst.InputHandler.ChangeCurrentActionMap(InputEnum.Cfg, false);
        GameManager.Inst?.ClearScene();
        isDone = true;
    }
}
