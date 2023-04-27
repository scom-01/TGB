using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SOB.Manager;
using Cinemachine;
using SOB.CoreSystem;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    [Header("----Manager----")]
    public ItemManager IM;
    public SpawnManager SPM;
    public string CurrStageName;
    public string NextStageName;

    [Header("----Player----")]
    [SerializeField]
    public Transform respawnPoint;
    [SerializeField]
    public Transform EndPoint;
    [SerializeField]
    public GameObject Player;
    private GameObject playerGO;
    public Player player;
    [SerializeField]
    public float respawnTime;
    [SerializeField]
    public float DeadLine;

    private float respawnTimeStart;

    public bool isStageClear = false;


    [HideInInspector] public CinemachineVirtualCamera CVC;

    private void Awake()
    {
        if (GameManager.Inst)
        {
            GameManager.Inst.StageManager = this;
        }
        GameManager.Inst?.MainUI.MainPanel.gameObject.SetActive(true);
        //if (Inst)
        //{
        //    Destroy(this.gameObject);
        //    return;
        //}

        Application.targetFrameRate = 60;
        playerGO = Instantiate(Player, respawnPoint);
        player = playerGO.GetComponent<Player>();
        //Inst = this;
        //DontDestroyOnLoad(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        CVC = GameObject.Find("Player Camera").GetComponent<CinemachineVirtualCamera>();
        CVC.Follow = player.transform;

        isStageClear = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckRespawn();
    }

    private void OnEnable()
    {
        Init();
    }

    public void Respawn()
    {
        respawnTimeStart = Time.time;
    }

    private void CheckRespawn()
    {
        
    }

    private void Init()
    {
        IM = GameObject.Find("ItemManager").GetComponent<ItemManager>();
        SPM = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        respawnPoint = GameObject.Find("SpawnPoint").GetComponent<Transform>();
        GameManager.Inst.inputHandler.ChangeCurrentActionMap("GamePlay", false);
        GameManager.Inst.LoadData();
        GameManager.Inst.SaveData();
    }

    public void OpenGate(bool isClear)
    {
        if (!isClear)
            return;

        //End 애니메이션
        EndPoint.GetComponentInChildren<Animator>().SetBool("Action", true);
        EndPoint.GetComponent<BoxCollider2D>().enabled = true;
    }


    public void CutSceneStart()
    {
        GameManager.Inst.inputHandler.ChangeCurrentActionMap("Cfg", false);
    }

    public void CutSceneEnd()
    {
        GameManager.Inst.inputHandler.ChangeCurrentActionMap("GamePlay", false);
    }
}
