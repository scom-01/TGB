using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using SOB.Manager;
using SOB.CoreSystem;
using static UnityEditor.Experimental.GraphView.GraphView;
using System.Numerics;
using UnityEngine.InputSystem;

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

    public PlayerInputHandler inputHandler
    {
        get
        {
            return this.GetComponent<PlayerInputHandler>();
        }
    }
    private float respawnTimeStart;
    private bool respawn;


    [Header("----UI----")]
    public MainUIManager MainUI;
    public SubUIManager SubUI;
    public CfgUIManager CfgUI;
    public DamageUIManager DamageUI;


    private CinemachineVirtualCamera CVC;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        playerGO = Instantiate(Player, respawnPoint);
        player = playerGO.GetComponent<Player>();

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

    private void OnEnable()
    {
        UserKeySettingLoad();
    }

    private void OnDisable()
    {
        UserKeySettingSave();
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
    public void CheckPause(string actionMap, bool pause)
    {
        if (pause)
            Pause(actionMap);
        else
            Continue(actionMap);
    }

    private void Pause(string switchActionMap)
    {
        Time.timeScale = 0f;
        MainUI.MainPanel.gameObject.SetActive(false);
        switch (switchActionMap)
        {
            case "UI":
                CfgUI.ConfigPanelUI.gameObject.SetActive(false);
                SubUI.InventorySubUI.gameObject.SetActive(true);
                break;
            case "Cfg":
                SubUI.InventorySubUI.gameObject.SetActive(false);
                CfgUI.ConfigPanelUI.gameObject.SetActive(true);
                break;
        }
    }
    private void Pause()
    {
        Time.timeScale = 0f;
        MainUI.MainPanel.gameObject.SetActive(false);
        SubUI.InventorySubUI.gameObject.SetActive(true);
    }
    private void Continue(string switchActionMap)
    {
        Time.timeScale = 1f;
        MainUI.MainPanel.gameObject.SetActive(true);
        SubUI.InventorySubUI.gameObject.SetActive(false);
        CfgUI.ConfigPanelUI.gameObject.SetActive(false);
    }
    private void Continue()
    {
        Time.timeScale = 1f;
        SubUI.InventorySubUI.gameObject.SetActive(false);
    }

    public void UserKeySettingLoad()
    {
        string rebinds = PlayerPrefs.GetString(GlobalValue.RebindsKey, string.Empty);
        if (string.IsNullOrEmpty(rebinds))
        {
            Debug.LogWarning("Load Fails");
            return;
        }
        inputHandler.playerInput.actions.LoadBindingOverridesFromJson(rebinds);
        Debug.LogWarning("Load Success");
    }
    public void UserKeySettingSave()
    {
        string rebinds = inputHandler.playerInput.actions.SaveBindingOverridesAsJson();
        PlayerPrefs.SetString(GlobalValue.RebindsKey, rebinds);
        Debug.LogWarning("Save Success");
    }
}
