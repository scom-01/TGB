using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SOB.Manager;
using Cinemachine;
using SOB.CoreSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class StageManager : MonoBehaviour
{
    [Header("----Manager----")]
    public ItemManager IM;
    public SpawnManager SPM;
    public string CurrStageName
    {
        get
        {
            return SceneManager.GetActiveScene().name;
        }
    }
    public int CurrStageNumber;
    public int NextStageNumber;
    public UI_State Start_UIState;

    [Header("----Player----")]
    [SerializeField]
    public Transform respawnPoint;
    [SerializeField]
    public Transform EndPoint;
    [SerializeField]
    public GameObject PlayerPrefab;
    private GameObject playerGO;
    public Player player;
    [SerializeField]
    public float respawnTime;

    [SerializeField] private GameObject SceneNameFade;

    public bool isStageClear = false;
    public PlayableDirector CutSceneDirector;

    [HideInInspector] public CinemachineVirtualCamera CVC;

    private void Awake()
    {
        Debug.LogWarning($"Stage Awake {CurrStageName}");
        if (GameManager.Inst)
        {
            GameManager.Inst.StageManager = this;
        }

        Application.targetFrameRate = 60;
        playerGO = Instantiate(PlayerPrefab, respawnPoint);
        player = playerGO.GetComponent<Player>();

        var FadeIn = Resources.Load<GameObject>(GlobalValue.FadeInCutScene);
        if (FadeIn != null)
        {
            var Fadeobject = Instantiate(FadeIn);
            Fadeobject.GetComponent<PlayableDirector>().Play();
            Destroy(Fadeobject, (float)Fadeobject.GetComponent<PlayableDirector>().duration);
        }
        //if (CutSceneDirector != null)
        //    CutSceneDirector.Play();
    }
    // Start is called before the first frame update
    public virtual void Start()
    {
        CVC = GameObject.Find("Player Camera").GetComponent<CinemachineVirtualCamera>();
        CVC.Follow = player.transform;

        isStageClear = false;
    }

    private void OnEnable()
    {
        Init();
    }

    private void Init()
    {
        IM = GameObject.Find("ItemManager").GetComponent<ItemManager>();
        SPM = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        respawnPoint = GameObject.Find("SpawnPoint").GetComponent<Transform>();
        GameManager.Inst.inputHandler.ChangeCurrentActionMap(InputEnum.GamePlay, false);
        GameManager.Inst.ChangeUI(Start_UIState);
        //Loading시 ESC를 눌러서 Pause가 됐을 때 생기는 오류 방지
        GameManager.Inst.Continue();
        GameManager.Inst.LoadData();
        GameManager.Inst.SaveData();

        //씬 이름 애니메이션 Instantiate
        if (SceneNameFade != null)
            Instantiate(SceneNameFade);
    }

    public void OpenGate(bool isClear)
    {
        if (!isClear)
            return;

        //End 애니메이션
        EndPoint.GetComponentInChildren<Animator>()?.SetBool("Action", true);
        EndPoint.GetComponent<BoxCollider2D>().enabled = true;
    }


    public void CutSceneStart()
    {
        GameManager.Inst.inputHandler.ChangeCurrentActionMap(InputEnum.Cfg, false);
    }

    public void CutSceneEnd()
    {
        GameManager.Inst.inputHandler.ChangeCurrentActionMap(InputEnum.GamePlay, false);
    }
}
