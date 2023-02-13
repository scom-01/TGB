using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DetailUI : MonoBehaviour
{
    [Header("---Main---")]
    [SerializeField]
    private GameObject mainUI;
    [Tooltip("하위 컴포넌트 중 'MainText' Text String")]
    public string MainItemName 
    { 
        get 
        {
            return mainItemName; 
        }
        set
        {
            mainItemName = value;
            if(mainUI != null)
                mainUI.GetComponent<TextMeshProUGUI>().text = mainItemName;
        }
    }
    private string mainItemName;

    [SerializeField]
    private RawImage icon;

    [Header("---Sub---")]    
    [SerializeField]
    private GameObject subUI;
    [Tooltip("하위 컴포넌트 중 'SubText' Text String")]
    public string SubItemName
    {
        get
        {
            return subItemName;
        }
        set
        {
            subItemName = value;
            if (subUI != null)
                subUI.GetComponent<TextMeshProUGUI>().text = subItemName;
        }
    }
    private string subItemName;

    ContentSizeFitter csf;
    private void Awake()
    {
        csf = GetComponent<ContentSizeFitter>();
        Init();

        //this.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        Init();
        //처음 SetActive시 ContentSizeFitter가 먹히지않던 해결 코드
        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)csf.transform);
    }

    private void OnDisable()
    {
        
    }

    private void Init()
    {
        if (mainUI != null)
        {
            Debug.Log(mainUI.name);
            mainUI.GetComponent<TextMeshProUGUI>().text = mainItemName;
        }

        if (subUI != null)
        {
            Debug.Log(subUI.name);
            subUI.GetComponent<TextMeshProUGUI>().text = subItemName;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }
}