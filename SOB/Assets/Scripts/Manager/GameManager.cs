using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using SOB.Manager;
using SOB.CoreSystem;
using static UnityEditor.Experimental.GraphView.GraphView;
using System.Numerics;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class GameManager : MonoBehaviour
{
    public static GameManager Inst
    {
        get
        {
            if (_Inst == null)
            {
                _Inst = FindObjectOfType(typeof(GameManager)) as GameManager;
                if (_Inst == null)
                    Debug.Log("no Singleton obj");
            }
            return _Inst;
        }
    }
    private static GameManager _Inst = null;
    private void Awake()
    {
        if (_Inst)
        {
            Destroy(this.gameObject);
            return;
        }

        _Inst = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public PlayerInputHandler inputHandler
    {
        get
        {
            return this.GetComponent<PlayerInputHandler>();
        }
    }

    public StageManager StageManager
    {
        get
        {
            return _stageManager;
        }
        set
        {
            _stageManager = value;
        }
    }
    public StageManager _stageManager;

    [Header("----UI----")]
    public MainUIManager MainUI;
    public SubUIManager SubUI;
    public CfgUIManager CfgUI;
    public DamageUIManager DamageUI;

    private void Start()
    {
        DataManager.Inst.UserKeySettingLoad();
    }
    private void Update()
    {
    }
    private void OnEnable()
    {
    }

    private void OnDisable()
    {        
    }

    private void OnDestroy()
    {
        DataManager.Inst.UserKeySettingSave();        
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
        if (StageManager)
        {
            MainUI.MainPanel.gameObject.SetActive(true);
            SubUI.InventorySubUI.gameObject.SetActive(false);
        }
        CfgUI.ConfigPanelUI.gameObject.SetActive(false);
    }
    private void Continue()
    {
        Time.timeScale = 1f;
        SubUI.InventorySubUI.gameObject.SetActive(false);
    }
}
