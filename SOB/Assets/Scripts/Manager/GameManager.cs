using SOB.CoreSystem;
using SOB.Manager;
using System;
using Unity.VisualScripting;
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
    public CutSceneManagerUI CutSceneUI;
    public PlayTimeManagerUI PlayTimeUI;

    [HideInInspector]    public UI_State Old_UIState;
    [HideInInspector]    public UI_State Curr_UIState;

    [HideInInspector]
    public float PlayTime;

    public event Action SaveAction;

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

        if (CutSceneUI == null)
            CutSceneUI = this.GetComponentInChildren<CutSceneManagerUI>();

        if (PlayTimeUI == null)
            PlayTimeUI = this.GetComponentInChildren<PlayTimeManagerUI>();

        SetSaveData();
    }

    private void Update()
    {
        if(StageManager != null)
        {
            PlayTime += Time.deltaTime;
        }

        if (inputHandler.ESCInput)
        {
            //사망 시엔 ResultUI의 Title누르는 것 외엔 작동하지않도록
           
        }
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

    public void CheckPause(InputEnum inputEnum, bool pause)
    {
        if (pause)
            Pause(inputEnum);
        else
            Continue(inputEnum);
    }
    public void CheckPause(InputEnum inputEnum)
    {
        if (!isPause)
            Pause(inputEnum);
        else
            Continue(inputEnum);
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        isPause = true;
    }
    private void Pause(InputEnum inputEnum)
    {
        Time.timeScale = 0f;
        switch (inputEnum)
        {
            //InventoryOpen
            case InputEnum.UI:
                ChangeUI(UI_State.Inventory);
                break;
            case InputEnum.Cfg:
                ChangeUI(UI_State.Cfg);
                
                break;
        }
        isPause = true;
    }
    private void Continue(InputEnum inputEnum)
    {
        if (!isPause)
            return;
                
        Time.timeScale = 1f;
        ChangeUI(UI_State.GamePlay);
        var _TitleManager = FindObjectOfType(typeof(TitleManager)) as TitleManager;
        if (_TitleManager != null)
        {
            if (_TitleManager.buttons.Count > 0)
                EventSystem.current.SetSelectedGameObject(_TitleManager.buttons[0].gameObject);
        }
        isPause = false;
    }

    //UI Button On Click
    public void Continue()
    {
        Time.timeScale = 1f;

        ChangeUI(UI_State.GamePlay);

        var _TitleManager = FindObjectOfType(typeof(TitleManager)) as TitleManager;
        if (_TitleManager != null)
        {
            if(_TitleManager.buttons.Count > 0)
                EventSystem.current.SetSelectedGameObject(_TitleManager.buttons[0].gameObject);
        }
        if (StageManager != null)
            inputHandler.playerInput.currentActionMap = inputHandler.playerInput.actions.FindActionMap(InputEnum.GamePlay.ToString());
        
        isPause = false;
    }

    #region UI

    public void ChangeUI(UI_State ui)
    {
        var _ui = ui;
        if (Curr_UIState == UI_State.Cfg && Curr_UIState != Old_UIState)
        {
            _ui = Old_UIState;
        }
        Old_UIState = Curr_UIState;
        Debug.Log($"Switch UI {_ui}");
        UI_Init();

        switch (_ui)
        {            
            case UI_State.GamePlay:
                if (StageManager != null)
                {
                    PlayTimeUI.Canvas.enabled = true;
                }
                EventSystem.current.SetSelectedGameObject(null);
                if (StageManager != null)
                    MainUI.Canvas.enabled = true;
                else
                    MainUI.Canvas.enabled = false;

                Curr_UIState = UI_State.GamePlay;
                break;
            case UI_State.Inventory:
                PlayTimeUI.Canvas.enabled = true;
                SubUI.InventorySubUI.PutInventoryItem();
                SubUI.InventorySubUI.Canvas.enabled = true;
                if (SubUI.InventorySubUI.InventoryItems.CurrentSelectItem == null)
                {
                    EventSystem.current.SetSelectedGameObject(SubUI.InventorySubUI.InventoryItems.items[0].gameObject);
                }
                else
                {
                    EventSystem.current.SetSelectedGameObject(SubUI.InventorySubUI.InventoryItems.CurrentSelectItem.gameObject);
                }
                Curr_UIState = UI_State.Inventory;
                break;
            case UI_State.Reforging:
                ReforgingUI.EnabledChildrensCanvas(true);
                ReforgingUI.Canvas.enabled = true;
                EventSystem.current.SetSelectedGameObject(ReforgingUI.equipWeapon.gameObject);
                Curr_UIState = UI_State.Reforging;
                break;
            case UI_State.Cfg:
                if(StageManager != null)
                {
                    PlayTimeUI.Canvas.enabled = true;
                }
                CfgUI.Canvas.enabled = true;
                EventSystem.current.SetSelectedGameObject(CfgUI.ConfigPanelUI.cfgBtns[0].gameObject);
                Curr_UIState = UI_State.Cfg;
                break;
            case UI_State.CutScene:
                CutSceneUI.Canvas.enabled = true;
                //CutSceneUI.Director_SetAsset(CutSceneUI.FadeIn);
                Curr_UIState = UI_State.CutScene;
                break;
            case UI_State.Result:
                if (StageManager != null)
                {
                    ResultUI.resultPanel.UpdateResultPanel(StageManager.player.Core.GetCoreComponent<UnitStats>().StatsData);
                }
                ResultUI.Canvas.enabled = true;
                EventSystem.current.SetSelectedGameObject(ResultUI.GoTitleBtn.gameObject);
                Curr_UIState = UI_State.Result;
                break;
            case UI_State.Loading:
                Curr_UIState = UI_State.Loading;
                break;
        }
    }

    private void UI_Init()
    {
        MainUI.Canvas.enabled = false;
        if (CfgUI.ConfigPanelUI.cfgBtns.Length > 0)
        {
            for (int i = 0; i < CfgUI.ConfigPanelUI.cfgBtns.Length; i++)
            {
                if (CfgUI.ConfigPanelUI.cfgBtns[i].ActiveUI != null)
                    CfgUI.ConfigPanelUI.cfgBtns[i].ActiveUI.GetComponent<SettingUI>().Canvas.enabled = false;
            }
        }
        CfgUI.Canvas.enabled = false;
        SubUI.InventorySubUI.Canvas.enabled = false;
        SubUI.InventorySubUI.NullCheckInput();
        SubUI.DetailSubUI.Canvas.enabled = false;
        ResultUI.Canvas.enabled = false;
        ReforgingUI.EnabledChildrensCanvas(false);
        ReforgingUI.Canvas.enabled = false;
        CutSceneUI.Canvas.enabled = false;
        PlayTimeUI.Canvas.enabled = false;
        //CutSceneUI.Director_SetAsset(CutSceneUI.FadeOut);
    }

    /// <summary>
    /// Inspector EventTrigger 에서 ChangeUI를 사용하기 위함
    /// 1. EventTrigger를 사용할 Object에 ChangeUIPanel.cs를 AddComponent한다
    /// 2. ChangeUIPanel의 UI_State를 사용하고자 하는 UI_State로 변경한다.
    /// 3. EventTrigger에 GameManager의 ChangeUI(ChangeUIPanel panel)를 넣고 1.에 Add한 ChangeUIPanel을 할당한다.
    /// </summary>
    /// <param name="panel"></param>
    public void ChangeUI(ChangeUIPanel panel)
    {
        ChangeUI(panel.UI_State);
    }

    public void EventSystemSetSelectedNull()
    {
        EventSystem.current.SetSelectedGameObject(null);
    }
    #endregion


    public void LoadData()
    {
        if (DataManager.Inst == null)
            return;

        if (GameManager.Inst.StageManager == null)
        {
            return;
        }

        DataManager.Inst?.PlayerInventoryDataLoad(StageManager.player.Inventory);
        DataManager.Inst?.PlayerCurrHealthLoad(StageManager.player.Core.GetCoreComponent<UnitStats>());
        DataManager.Inst?.PlayerBuffLoad(StageManager.player.GetComponent<BuffSystem>());
        DataManager.Inst?.LoadPlayTime();
        DataManager.Inst?.GameGoldLoad();
        DataManager.Inst?.GameElementalsculptureLoad();
    }
    public void SaveData()
    {
        SaveAction?.Invoke();
        DataManager.Inst?.UnlockItemList.Clear();
    }

    private void Data_Save()
    {
        if (DataManager.Inst == null)
            return;

        if (StageManager == null)
        {
            return;
        }

        DataManager.Inst?.SavePlayTime(PlayTime);
        DataManager.Inst?.SaveScene(StageManager.CurrStageName);
        DataManager.Inst?.NextStage(StageManager.NextStageName);
        DataManager.Inst.PlayerInventoryDataSave(
            GameManager.Inst.StageManager.player.Inventory.Weapon,
            GameManager.Inst.StageManager.player.Inventory._items);
        DataManager.Inst?.PlayerCurrHealthSave(
            (int)GameManager.Inst.StageManager.player.Core.GetCoreComponent<UnitStats>().CurrentHealth);
        DataManager.Inst?.PlayerBuffSave(
            GameManager.Inst.StageManager.player.GetComponent<BuffSystem>().buffs
            );
        DataManager.Inst?.GameGoldSave(DataManager.Inst.GoldCount);
        DataManager.Inst?.GameElementalsculptureSave(DataManager.Inst.ElementalsculptureCount);

    }

    public void SetSaveData()
    {
        SaveAction += Data_Save;
        SaveAction += DataManager.Inst.PlayerUnlockItem;
    }

    public void ClearData()
    {
        if (DataManager.Inst == null)
            return;
                
        DataManager.Inst.PlayerInventoryDataSave(null, null);
        DataManager.Inst?.GameGoldSave(0);
        DataManager.Inst?.GameElementalsculptureSave(0);

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
        Time.timeScale = 1f;
        isPause = false;
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
        ChangeUI(UI_State.Loading);
        SaveData();
        AsyncOperation operation = SceneManager.LoadSceneAsync("LoadingScene");
    }
    public void MoveTitle()
    {
        ChangeUI(UI_State.Loading);
        ClearData();
        AsyncOperation operation = SceneManager.LoadSceneAsync("LoadingScene");
    }
}
