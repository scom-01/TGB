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

    public PlayerInputHandler inputHandler
    {
        get
        {
            return this.GetComponent<PlayerInputHandler>();
        }
    }

    [Header("----UI----")]
    public MainUIManager MainUI;
    public SubUIManager SubUI;
    public CfgUIManager CfgUI;
    public DamageUIManager DamageUI;

    private void Awake()
    {
        if (Inst)
        {
            Destroy(this.gameObject);
            return;
        }

        Inst = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
    }
    private void Update()
    {
    }

    private void OnEnable()
    {
        UserKeySettingLoad();
    }

    private void OnDisable()
    {
        UserKeySettingSave();
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
        if(StageManager.Inst)
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
