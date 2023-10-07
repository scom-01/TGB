using UnityEngine;
using UnityEngine.UI;

public class UnlockItemData : MonoBehaviour
{
    [SerializeField]    private int m_Index;
    [SerializeField]    private bool m_locked;

    private Image m_Img
    {
        get
        {
            if(m_img == null)
            {
                m_img = this.GetComponent<Image>();
            }
            return m_img;
        }
    }
    private Image m_img;

    public Image ItemImg;
    public Image ItemLockImg;
    private void Awake()
    {
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    [ContextMenu("Edit SetUp")]
    private void EditSetUp()
    {
        SetData(m_Index);
    }

    public void SetData(int _idx, bool _islock)
    {
        m_Index = _idx;
        
        m_locked = _islock;
        ItemLockImg.gameObject.SetActive(m_locked);

        if (ItemImg != null)
        {
            if(m_Index < GlobalValue.All_ItemDB.ItemDBList.Count)
            {
                ItemImg.sprite = GlobalValue.All_ItemDB.ItemDBList[m_Index].itemData.ItemSprite;            
            }
        }
    }
    public void SetData(int _idx)
    {
        m_Index = _idx;

        //UnlockItemList안에 해당 Item이 있다면
        if (DataManager.Inst.JSON_DataParsing.m_JSON_DefaultData.UnlockItemIdxs.Contains(GlobalValue.All_ItemDB.ItemDBList[m_Index].ItemIdx))
        {
            m_locked = false;
        }
        else
        {
            m_locked = true;
        }

        ItemLockImg.gameObject.SetActive(m_locked);

        if (ItemImg != null)
        {
            if (m_Index < GlobalValue.All_ItemDB.ItemDBList.Count)
            {
                ItemImg.sprite = GlobalValue.All_ItemDB.ItemDBList[m_Index].itemData.ItemSprite;
            }
        }
    }
}
