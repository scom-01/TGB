using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using SOB.Manager;
using SOB.CoreSystem;
using static UnityEditor.Experimental.GraphView.GraphView;
using System.Numerics;

public class GameManager : MonoBehaviour
{
    public static GameManager Inst = null;

    [Header("----Manager----")]
    public ItemManager IM;
    public SpawnManager SPM;

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

    public PlayerInputHandler inputHandler { get; private set; }
    private float respawnTimeStart;
    private bool respawn;


    [Header("----UI----")]
    public MainUIManager MainUI;
    public SubUIManager SubUI;
    public DamageUIManager DamageUI;


    private CinemachineVirtualCamera CVC;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        playerGO = Instantiate(Player, respawnPoint);
        player = playerGO.GetComponent<Player>();
        inputHandler = this.GetComponent<PlayerInputHandler>();

        if (Inst == null)
        {
            Inst = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        CVC = GameObject.Find("Player Camera").GetComponent<CinemachineVirtualCamera>();
        CVC.Follow = player.transform;
    }
    private void Update()
    {
        CheckEnemy();
        CheckRespawn();
    }

    public void Respawn()
    {
        respawnTimeStart = Time.time;
        respawn = true;
    }

    private bool CheckEnemy()
    {
        if (SPM == null)
        { return false; }
        MainUI.EnemyPanelSystem.EnemyCountText.text = "Enemy : " + SPM.UIEnemyCount.ToString();
        return true;
    }

    private void CheckRespawn()
    {
        
    }

    

    public void CheckPause(bool pause)
    {
        if (pause)
            Pause();
        else
            Continue();
    }

    private void Pause()
    {
        Time.timeScale = 0f;
        SubUI.InventorySubUI.gameObject.SetActive(true);
    }
    private void Continue()
    {
        Time.timeScale = 1f;
        SubUI.InventorySubUI.gameObject.SetActive(false);
    }
}
