using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class UIEventHandler : MonoBehaviour
{
    [SerializeField]
    private UIDocument UIDoc;

    private Button start_btn;
    private Button load_btn;
    private Button option_btn;
    private Button exit_btn;

    private Button[] btns;
    private Label Loading_Lb;

    public ProgressBar Loading_progressbar;

    private void Awake()
    {
        UIDoc = this.GetComponent<UIDocument>();
    }
    // Start is called before the first frame update
    void Start()
    {
        var rootElement = UIDoc.rootVisualElement;
        start_btn = rootElement.Q<Button>("Start_Btn");
        load_btn = rootElement.Q<Button>("Load_Btn");
        option_btn = rootElement.Q<Button>("Option_Btn");
        exit_btn = rootElement.Q<Button>("Exit_Btn");

        List<VisualElement> result = rootElement.Query(className: "title-button").Where(elem => elem.tooltip == "").ToList();
        foreach(VisualElement element in result)
        {
            switch(element.name)
            {
                case "Start_Btn":
                    element.AddManipulator(new Clickable(evt => Debug.Log(element.name)));
                    break;
                case "Load_Btn":
                    break;
                case "Option_Btn":
                    break;
                case "Exit_Btn":
                    break;                        
            }
            Debug.LogWarning(element.name);
        }
        Loading_Lb = rootElement.Q<Label>("Loading_Lb");
        Loading_progressbar = rootElement.Q<ProgressBar>("Loading_progress_bar");

        if (start_btn!=null)
            start_btn.clickable.clicked += (() =>
            {
                DataManager.Inst.LoadScene();
                SceneManager.LoadSceneAsync("LoadingScene");
            });
        if (load_btn != null)
            load_btn.clickable.clicked += OnBtnClicked;
        if (option_btn != null)
            option_btn.clickable.clicked += OnBtnOptionClicked;
        if (exit_btn != null)
            exit_btn.clickable.clicked += OnBtnClicked;

        if (Loading_Lb != null)
            Loading_Lb.text = "Loading...";
        //if(Loading_progressbar!= null)
        //Loading_progressbar.value=
    }
    private void OnEnable()
    {
        GameManager.Inst.MainUI.MainPanel.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        if (start_btn != null)
            start_btn.clickable.clicked -= OnBtnClicked;
        if (load_btn != null)
            load_btn.clickable.clicked -= OnBtnClicked;
        if (option_btn != null)
            option_btn.clickable.clicked -= OnBtnOptionClicked;
        if (exit_btn != null)
            exit_btn.clickable.clicked -= OnBtnClicked;
    }

    private void OnButtonCallBack(ChangeEvent<string> evt)
    {
        Debug.Log(evt);
    }
    private void OnBtnClicked()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void OnBtnOptionClicked()
    {
        GameManager.Inst?.inputHandler.ChangeCurrentActionMap("Cfg", true);
        GameManager.Inst?.CfgUI.ConfigPanelUI.gameObject.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
