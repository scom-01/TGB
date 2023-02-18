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

    [Header("----Player----")]
    [SerializeField]
    private Transform respawnPoint;
    [SerializeField]
    public Player player;
    [SerializeField]
    private float respawnTime;
    [SerializeField]
    private float deadLine;

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
    }
    private void Update()
    {
        CheckRespawn();
    }

    public void Respawn()
    {
        respawnTimeStart = Time.time;
        respawn = true;
    }

    private void CheckRespawn()
    {
        CheckDeadLine();

        CheckLife(player);

        if (Time.time >= respawnTimeStart + respawnTime && respawn)
        {
            var playerTemp = Instantiate(player, respawnPoint);
            CVC.m_Follow = playerTemp.transform;
            respawn = false;
        }

    }

    private void CheckDeadLine()
    {
        if (player != null)
        {
            if (player.transform.position.y < deadLine)
            {
                player.Core.GetCoreComponent<Movement>().SetVelocityZero();
                player.gameObject.transform.position = respawnPoint.transform.position;
                var amount = player.Core.GetCoreComponent<UnitStats>().DecreaseHealth(ElementalPower.Normal, DamageAttiribute.Fixed, 50);
                player.Core.GetCoreComponent<DamageReceiver>().RandomParticleInstantiate(player.Core.GetCoreComponent<DamageReceiver>().DefaultEffectPrefab, 0.5f, amount, 50, DamageAttiribute.Fixed);
                //player.GetComponent<Player>().Core.GetCoreComponent<Death>().Die();
                //respawn = true;
            }
        }
    }
    private void CheckLife(Player player)
    {
        player.IsAlive = player.gameObject.activeSelf;
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
