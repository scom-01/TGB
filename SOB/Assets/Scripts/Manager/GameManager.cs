using SOB.CoreSystem;
using SOB.Manager;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Localization.Plugins.XLIFF.V12;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

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
                {
                    Debug.Log("no Singleton obj");
                }
                else
                {
                    DontDestroyOnLoad(_Inst.gameObject);
                }
            }
            return _Inst;
        }
    }
    private static GameManager _Inst = null;

    private bool isPause = false;
    public PlayerInputHandler inputHandler
    {
        get
        {
            if (_inputHandler == null)
            {
                _inputHandler = this.GetComponent<PlayerInputHandler>();
            }

            return _inputHandler;
        }
    }
    private PlayerInputHandler _inputHandler;

    public StageManager StageManager
    {
        get
        {
            if (Inst == null)
                return null;

            if (_stageManager == null)
                return null;

            return _stageManager;
        }
        set
        {
            _stageManager = value;
        }
    }
    private StageManager _stageManager = null;

    [Header("----UI----")]
    public MainUIManager MainUI;
    public SubUIManager SubUI;
    public CfgUIManager CfgUI;
    public ReforgingUIManager ReforgingUI;
    public ResultUIManager ResultUI;
    public DamageUIManager DamageUI;

    private void Awake()
    {
        if (_Inst)
        {
            var managers = Resources.FindObjectsOfTypeAll(typeof(GameManager));
            for (int i = 0; i < managers.Length; i++)
            {
                Debug.Log($"{managers[i]} = {i}");
                if (i > 0)
                {
                    Destroy(managers[i].GameObject());
                }
            }
            return;
        }

        _Inst = this;
        DontDestroyOnLoad(this.gameObject);
        GraphicsSettings.useScriptableRenderPipelineBatching = true;
        if (MainUI == null)
            MainUI = this.GetComponentInChildren<MainUIManager>();

        if (SubUI == null)
            SubUI = this.GetComponentInChildren<SubUIManager>();

        if (CfgUI == null)
            CfgUI = this.GetComponentInChildren<CfgUIManager>();

        if (ReforgingUI == null)
            ReforgingUI = this.GetComponentInChildren<ReforgingUIManager>();

        if (DamageUI == null)
            DamageUI = this.GetComponentInChildren<DamageUIManager>();

        if (ResultUI == null)
            ResultUI = this.GetComponentInChildren<ResultUIManager>();
    }

    private void Start()
    {
        //해당 함수들은 GameManager에서 참조할 변수들이 있어 GameManager에서 선언
        DataManager.Inst.UserKeySettingLoad();
        DataManager.Inst.PlayerCfgBGMLoad();
        DataManager.Inst.PlayerCfgSFXLoad();
        DataManager.Inst.PlayerCfgQualityLoad();
        DataManager.Inst.PlayerCfgLanguageLoad();
        DataManager.Inst.GameGoldLoad();
        DataManager.Inst.GameElementalsculptureLoad();
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
    public void CheckPause(string actionMap)
    {
        if (!isPause)
            Pause(actionMap);
        else
            Continue(actionMap);
    }

    private void Pause(string switchActionMap)
    {
        Time.timeScale = 0f;
        MainUI.Canvas.enabled = false;
        switch (switchActionMap)
        {
            //InventoryOpen
            case "UI":
                CfgUI.Canvas.enabled = false;                
                SubUI.InventorySubUI.Canvas.enabled = true;
                if (SubUI.InventorySubUI.InventoryItems.CurrentSelectItem == null)
                {
                    EventSystem.current.SetSelectedGameObject(SubUI.InventorySubUI.InventoryItems.items[0].gameObject);
                }
                else
                {
                    EventSystem.current.SetSelectedGameObject(SubUI.InventorySubUI.InventoryItems.CurrentSelectItem.gameObject);
                }
                break;
            case "Cfg":
                SubUI.InventorySubUI.Canvas.enabled = false;
                CfgUI.Canvas.enabled = true;
                EventSystem.current.SetSelectedGameObject(CfgUI.ConfigPanelUI.cfgBtns[0].gameObject);
                break;
            case "Empty":
                SubUI.InventorySubUI.Canvas.enabled = false;
                CfgUI.Canvas.enabled = true;
                EventSystem.current.SetSelectedGameObject(CfgUI.ConfigPanelUI.cfgBtns[0].gameObject);
                break;
        }
        isPause = true;
    }
    private void Pause()
    {
        Time.timeScale = 0f;
        MainUI.Canvas.enabled = false;
        SubUI.InventorySubUI.Canvas.enabled = true;
        isPause = true;
    }
    private void Continue(string switchActionMap)
    {
        if (!isPause)
            return;

        Time.timeScale = 1f;

        if (StageManager)
        {
            MainUI.Canvas.enabled = true;
            SubUI.InventorySubUI.Canvas.enabled = false;
        }

        //SettingUI들 Canvas.enabled = false
        if (CfgUI.ConfigPanelUI.cfgBtns.Length>0)
        {
            for(int i = 0;i < CfgUI.ConfigPanelUI.cfgBtns.Length;i++)
            {
                if (CfgUI.ConfigPanelUI.cfgBtns[i].ActiveUI != null)
                    CfgUI.ConfigPanelUI.cfgBtns[i].ActiveUI.GetComponent<SettingUI>().Canvas.enabled = false;
            }
        }

        CfgUI.Canvas.enabled = false;
        isPause = false;
    }
    public void Continue()
    {
        Time.timeScale = 1f;

        if (StageManager)
        {
            MainUI.Canvas.enabled = true;
            SubUI.InventorySubUI.Canvas.enabled = false;
        }
        //SettingUI들 Canvas.enabled = false
        if (CfgUI.ConfigPanelUI.cfgBtns.Length > 0)
        {
            for (int i = 0; i < CfgUI.ConfigPanelUI.cfgBtns.Length; i++)
            {
                if (CfgUI.ConfigPanelUI.cfgBtns[i].ActiveUI != null)
                    CfgUI.ConfigPanelUI.cfgBtns[i].ActiveUI.GetComponent<SettingUI>().Canvas.enabled = false;
            }
        }
        if (StageManager != null)
            inputHandler.playerInput.currentActionMap = inputHandler.playerInput.actions.FindActionMap("GamePlay");
        CfgUI.Canvas.enabled = false;
        isPause = false;
    }

    public void LoadData()
    {
        if (DataManager.Inst == null)
            return;

        if (GameManager.Inst.StageManager == null)
        {
            return;
        }

        DataManager.Inst?.PlayerInventoryDataLoad(StageManager.player.Inventory);
        DataManager.Inst?.PlayerStatLoad(StageManager.player.Core.GetCoreComponent<UnitStats>());
        DataManager.Inst?.PlayerBuffLoad(StageManager.player.GetComponent<BuffSystem>());
    }
    public void SaveData()
    {
        if (DataManager.Inst == null)
            return;

        if (StageManager == null)
        {
            return;
        }

        DataManager.Inst?.SaveScene(StageManager.CurrStageName);
        DataManager.Inst?.NextStage(StageManager.NextStageName);
        DataManager.Inst.PlayerInventoryDataSave(
            GameManager.Inst.StageManager.player.Inventory.weapons,
            GameManager.Inst.StageManager.player.Inventory.items);
        DataManager.Inst?.PlayerStatSave(
            GameManager.Inst.StageManager.player.Core.GetCoreComponent<UnitStats>());
        DataManager.Inst?.PlayerBuffSave(
            GameManager.Inst.StageManager.player.GetComponent<BuffSystem>().buffs
            );
    }

    public void ClearData()
    {
        if (DataManager.Inst == null)
            return;
        DataManager.Inst?.NextStage("Title");
    }

    public void ClearScene()
    {
        var FadeOut = Resources.Load<GameObject>(GlobalValue.FadeOutCutScene);
        if (FadeOut != null)
        {
            var Fadeobject = Instantiate(FadeOut);
            Fadeobject.GetComponent<PlayableDirector>().Play();
        }
        Invoke("MoveScene", 1.5f);
    }
    public void GoTitleScene()
    {
        var FadeOut = Resources.Load<GameObject>(GlobalValue.FadeOutCutScene);
        if (FadeOut != null)
        {
            var Fadeobject = Instantiate(FadeOut);
            Fadeobject.GetComponent<PlayableDirector>().Play();
        }
        Invoke("MoveTitle", 1.5f);
    }

    private void MoveScene()
    {
        SaveData();
        AsyncOperation operation = SceneManager.LoadSceneAsync("LoadingScene");
    }
    private void MoveTitle()
    {
        ClearData();
        AsyncOperation operation = SceneManager.LoadSceneAsync("LoadingScene");
    }
}
