using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class BuffPanelSystem : MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField] private GameObject buffPanelPrefab;
    
    private List<BuffPanelItem> BuffPanelList
    {
        get
        {
            return this.GetComponentsInChildren<BuffPanelItem>().ToList();
        }
    }
    //[SerializeField] private int MaxBuffPanelHorizontal;

    private void Awake()
    {
        //GetComponent<GridLayoutGroup>().constraintCount = MaxBuffPanelHorizontal;
    }

    public void BuffPanelAdd(Buff buff)
    {
        if (GameManager.Inst == null)
            return;
        if (GameManager.Inst.StageManager == null)
            return;

        for (int i = 0; i < BuffPanelList.Count; i++)
        {
            if (BuffPanelList[i].buff.buffItemSO == buff.buffItemSO)
            {
                return;
            }
        }

        BuffPanelItem item = Instantiate(buffPanelPrefab, this.transform).GetComponent<BuffPanelItem>();
        item.buff = buff;
        item.Setting();
    }
}
