using SOB.Manager;
using UnityEngine;
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
    public StageManager _stageManager = null;

    [Header("----UI----")]
    public MainUIManager MainUI;
    public SubUIManager SubUI;
    public CfgUIManager CfgUI;
    public DamageUIManager DamageUI;

    private void Start()
    {
        //해당 함수들은 GameManager에서 참조할 변수들이 있어 GameManager에서 선언
        DataManager.Inst.UserKeySettingLoad();
        DataManager.Inst.PlayerCfgBGMLoad();
        DataManager.Inst.PlayerCfgSFXLoad();
        DataManager.Inst.PlayerCfgQualityLoad();
        DataManager.Inst.PlayerCfgLanguageLoad();
    }
    private void Update()
    {
    }
    private void OnEnable()
    {
    }

    private void OnDisable()
    {
        if (Inst != null)
        {
            DataManager.Inst.UserKeySettingSave();
            return;
        }
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
            case "UI":
                CfgUI.ConfigPanelUI.gameObject.SetActive(false);
                SubUI.InventorySubUI.gameObject.SetActive(true);
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

    public void ClearScene()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("LoadingScene");
    }
}
