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

        //씬 이동할 때마다 게임 Idx 설정 //임의의 수 10
        DataManager.Inst.JSON_DataParsing.m_JSON_SceneData.SceneDataIdxs.Clear();
        for (int i = 0; i < 10; i++)
        {
            DataManager.Inst.JSON_DataParsing.m_JSON_SceneData.SceneDataIdxs.Add(UnityEngine.Random.Range(0, GlobalValue.MaxSceneIdx));
        }

        Debug.LogWarning("OpenGate");
        GameManager.Inst.InputHandler.ChangeCurrentActionMap(InputEnum.Cfg, false);
        GameManager.Inst?.ClearScene();
        isDone = true;
    }
}
