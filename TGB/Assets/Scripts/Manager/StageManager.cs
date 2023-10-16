using Cinemachine;
using TGB.Manager;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    [Header("----Manager----")]    
    public ItemManager IM;
    public SpawnManager SPM;

    public List<Transform> MasterManagerList;

    public string CurrStageName
    {
        get
        {
            return SceneManager.GetActiveScene().name;
        }
    }

    [Tooltip("true면 다음 씬은 무조건 현재씬 idx +1 ")]
    public bool isGotoNext = true;

    [Tooltip("현재 스테이지 단계")]
    [Min(1)]
    public int StageLevel = 1;
    public int CurrStageNumber
    {
        get
        {
            if (GameManager.Inst.SceneNameList.Count > 0)
            {
                return GameManager.Inst.SceneNameList.FindIndex(str => str.Equals(SceneManager.GetActiveScene().name));
            }
            return 0;
        }
    }
    public int NextStageNumber
    {
        get
        {
            if(isGotoNext)
            {
                return CurrStageNumber + 1;
            }
            return m_NextStageNumber;
        }
    }

    public int m_NextStageNumber;
    public UI_State Start_UIState;
    public Camera Cam;
    public float Cam_Distance;
    [TagField]
    [field: SerializeField] private string effectContainerTagName = "EffectContainer";
    public EffectContainer EffectContainer
    {
        get
        {
            if(effectContainer == null)
            {
                if (effectContainerTagName == "")
                {
                    effectContainerTagName = "EffectContainer";
                }

                effectContainer = GameObject.FindGameObjectWithTag(effectContainerTagName).GetComponent<EffectContainer>();
                effectContainerTransform = GameObject.FindGameObjectWithTag(effectContainerTagName).transform;
                if (effectContainer == null)
                {
                    effectContainer = effectContainerTransform.gameObject.AddComponent<EffectContainer>();
                }
            }
            return effectContainer;
        }
    }
    private EffectContainer effectContainer;
    private Transform effectContainerTransform;

    [Header("----Player----")]

    /// <summary>
    /// Player SpawnPoint
    /// </summary>
    public Transform SpawnPoint;    
    /// <summary>
    /// 
    /// </summary>
    public Transform EndPoint;
    /// <summary>    
    /// </summary>
    public GameObject PlayerPrefab;

    private GameObject playerGO;
    [HideInInspector] public Player player;

    /// <summary>
    /// Stage 시작 시 Stage이름 Fade
    /// </summary>
    [SerializeField] private GameObject SceneNameFade;

    /// <summary>
    /// Stage 클리어 시 생성
    /// </summary>
    public GameObject EndingCutSceneDirector;

    [HideInInspector] public bool isStageClear = false;
    [HideInInspector] public CinemachineVirtualCamera CVC;

    private void Awake()
    {
        Debug.LogWarning($"Stage Awake {CurrStageName}");        
        if (GameManager.Inst)
        {
            GameManager.Inst.StageManager = this;
        }
        playerGO = Instantiate(PlayerPrefab, SpawnPoint);
        player = playerGO.GetComponent<Player>();

        if (Cam == null)
            Cam = this.GetComponentsInChildren<Camera>()[0];

        var FadeIn = Resources.Load<GameObject>(GlobalValue.FadeInCutScene);
        if (FadeIn != null)
        {
            Instantiate(FadeIn);
        }
    }

    // Start is called before the first frame update
    public virtual void Start()
    {
        CVC = GameObject.Find("Player Camera").GetComponent<CinemachineVirtualCamera>();
        if (CVC != null)
        {
            CVC.Follow = player.transform;
            CVC.m_Lens.OrthographicSize = (Cam_Distance <= 0) ? 5 : Cam_Distance;
        }
        GameManager.Inst.ChangeUI(UI_State.GamePlay);
        isStageClear = false;
    }

    private void OnEnable()
    {
        Init();
    }

    private void Init()
    {
        if (DataManager.Inst != null)
        {
            if(MasterManagerList.Count > 0)
            {
                var idx = DataManager.Inst.JSON_DataParsing.m_JSON_SceneData.SceneDataIdx % MasterManagerList.Count;

                if (idx == MasterManagerList.Count)
                {
                    MasterManagerList[idx - 1].gameObject.SetActive(true);
                    IM = MasterManagerList[idx - 1].GetComponentInChildren<ItemManager>();
                    SPM = MasterManagerList[idx - 1].GetComponentInChildren<SpawnManager>();
                }
                else
                {
                    MasterManagerList[idx].gameObject.SetActive(true);
                    IM = MasterManagerList[idx].GetComponentInChildren<ItemManager>();
                    SPM = MasterManagerList[idx].GetComponentInChildren<SpawnManager>();
                }
            }

            if (IM == null)
                IM = GameObject.Find("ItemManager").GetComponent<ItemManager>();

            if (SPM == null)
                SPM = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        }

        if (SpawnPoint == null)
            SpawnPoint = GameObject.Find("SpawnPoint").GetComponent<Transform>();
        GameManager.Inst.InputHandler.ChangeCurrentActionMap(InputEnum.GamePlay, false);
        GameManager.Inst.ChangeUI(Start_UIState);
        //Loading시 ESC를 눌러서 Pause가 됐을 때 생기는 오류 방지
        GameManager.Inst.Continue();
        GameManager.Inst.LoadData();
        GameManager.Inst.Data_Save();

        //씬 이름 애니메이션 Instantiate
        if (SceneNameFade != null)
            Instantiate(SceneNameFade);
    }

    public void OpenGate(bool isClear)
    {
        if (!isClear)
            return;
        DataManager.Inst?.NextStage(NextStageNumber);
        
        //End 애니메이션
        if (EndPoint.GetComponentInChildren<Animator>() != null)
        {
            if (GlobalValue.ContainParam(EndPoint.GetComponentInChildren<Animator>(), "Action"))
            {
                EndPoint.GetComponentInChildren<Animator>()?.SetBool("Action", true);            
            }
        }
        if (EndPoint.GetComponent<BoxCollider2D>() != null)
            EndPoint.GetComponent<BoxCollider2D>().enabled = true;
    }


    public void CutSceneStart()
    {
        GameManager.Inst.InputHandler.ChangeCurrentActionMap(InputEnum.Cfg, false);
    }

    public void CutSceneEnd()
    {
        GameManager.Inst.InputHandler.ChangeCurrentActionMap(InputEnum.GamePlay, false);
    }
}
