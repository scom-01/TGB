using SOB.CoreSystem;
using SOB.Manager;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Localization.Plugins.XLIFF.V12;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Playables;
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
    public StageManager _stageManager = null;

    [Header("----UI----")]
    public MainUIManager MainUI;
    public SubUIManager SubUI;
    public CfgUIManager CfgUI;
    public DamageUIManager DamageUI;
    public ResultUIManager ResultUI;

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

        if (MainUI == null)

            MainUI = this.GetComponentInChildren<MainUIManager>();

        if (SubUI == null)

            SubUI = this.GetComponentInChildren<SubUIManager>();

        if (CfgUI == null)

            CfgUI = this.GetComponentInChildren<CfgUIManager>();

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
        MainUI.MainPanel.gameObject.SetActive(false);
        switch (switchActionMap)
        {
            //InventoryOpen
            case "UI":
                CfgUI.ConfigPanelUI.gameObject.SetActive(false);
                SubUI.InventorySubUI.gameObject.SetActive(true);
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
                SubUI.InventorySubUI.gameObject.SetActive(false);
                CfgUI.ConfigPanelUI.gameObject.SetActive(true);
                break;
            case "Empty":
                SubUI.InventorySubUI.gameObject.SetActive(false);
                CfgUI.ConfigPanelUI.gameObject.SetActive(true);
                break;
        }
        isPause = true;
    }
    private void Pause()
    {
        Time.timeScale = 0f;
        MainUI.MainPanel.gameObject.SetActive(false);
        SubUI.InventorySubUI.gameObject.SetActive(true);
        isPause = true;
    }
    private void Continue(string switchActionMap)
    {
        if (!isPause)
            return;

        Time.timeScale = 1f;
        if (StageManager)
        {
            MainUI.MainPanel.gameObject.SetActive(true);
            SubUI.InventorySubUI.gameObject.SetActive(false);
        }
        CfgUI.ConfigPanelUI.gameObject.SetActive(false);
        isPause = false;
    }
    private void Continue()
    {
        Time.timeScale = 1f;
        SubUI.InventorySubUI.gameObject.SetActive(false);
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
