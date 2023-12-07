using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

    public bool BuffPanelAdd(Buff buff)
    {
        if (GameManager.Inst == null)
            return false;
        if (GameManager.Inst.StageManager == null)
            return false;

        //동일한 버프가 이미 BuffPanel UI창에 있다면 return
        for (int i = 0; i < BuffPanelList.Count; i++)
        {
            if (BuffPanelList[i].buff.buffItemSO == buff.buffItemSO)
            {
                return false;
            }
        }

        //BuffPanel UI창에 버프 추가
        BuffPanelItem item = Instantiate(buffPanelPrefab, this.transform).GetComponent<BuffPanelItem>();
        item.buff = buff;
        item.Setting();
        return true;
    }

    public bool BuffPanelRemove(Buff buff)
    {
        if (GameManager.Inst == null)
            return false;
        if (GameManager.Inst.StageManager == null)
            return false;

        for (int i = 0; i < BuffPanelList.Count; i++)
        {
            if (BuffPanelList[i].buff.buffItemSO == buff.buffItemSO)
            {
                Destroy(BuffPanelList[i].gameObject);
                break;
            }
        }
        return true;
    }
}
