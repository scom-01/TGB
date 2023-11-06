using UnityEngine;


public class InstantCutScene : InteractiveObject
{
    private bool isDone = false;
    [SerializeField]
    private GameObject CutScene;

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

        if (CutScene != null)
        {
            Debug.LogWarning("CutScene");
            Instantiate(CutScene);
        }
        isDone = true;
    }
}
