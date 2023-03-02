using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIEventHandler : MonoBehaviour
{
    [SerializeField]
    private UIDocument UIDoc;

    private Button start_btn;
    private Button load_btn;
    private Button option_btn;
    private Button exit_btn;
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

        start_btn.clickable.clicked += OnBtnClicked;
        load_btn.clickable.clicked += OnBtnClicked;
        option_btn.clickable.clicked += OnBtnClicked;
        exit_btn.clickable.clicked += OnBtnClicked;
    }
    private void OnDestroy()
    {
        start_btn.clickable.clicked -= OnBtnClicked;
        load_btn.clickable.clicked -= OnBtnClicked;
        option_btn.clickable.clicked -= OnBtnClicked;
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
    // Update is called once per frame
    void Update()
    {

    }
}
