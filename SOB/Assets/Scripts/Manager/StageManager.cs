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
    public GameObject Player;
    private GameObject playerGO;
    public Player player;
    [SerializeField]
    public float respawnTime;
    [SerializeField]
    public float DeadLine;

    private float respawnTimeStart;
    private bool respawn;


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

    public void LoadData()
    {
        if (DataManager.Inst == null)
            return;

        DataManager.Inst?.PlayerInventoryDataLoad(player.Inventory); 
        DataManager.Inst?.PlayerStatLoad(player.Core.GetCoreComponent<UnitStats>());
        DataManager.Inst?.PlayerBuffLoad(player.GetComponent<BuffSystem>());
    }

    public void SaveData()
    {
        if (DataManager.Inst == null)
            return;

        DataManager.Inst?.SaveScene(this.CurrStageName);
        DataManager.Inst?.NextStage(this.NextStageName);
        DataManager.Inst.PlayerInventoryDataSave(
            GameManager.Inst.StageManager.player.Inventory.weapons,
            GameManager.Inst.StageManager.player.Inventory.items);
        DataManager.Inst?.PlayerStatSave(
            GameManager.Inst.StageManager.player.Core.GetCoreComponent<UnitStats>());
        DataManager.Inst?.PlayerBuffSave(
            GameManager.Inst.StageManager.player.GetComponent<BuffSystem>().buffs
            );

    }

    public void Respawn()
    {
        respawnTimeStart = Time.time;
        respawn = true;
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
        LoadData();
        SaveData();
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
