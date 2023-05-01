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

    //[SerializeField] private int MaxBuffPanelHorizontal;

    private void Awake()
    {
        //GetComponent<GridLayoutGroup>().constraintCount = MaxBuffPanelHorizontal;
    }

    public void BuffPanelAdd(Buff buff)
    {
        BuffPanelItem item = Instantiate(buffPanelPrefab, this.transform).GetComponent<BuffPanelItem>();
        item.buff = buff;
        item.buffItem = buff.buffItem;
        item.Setting();
    }
}
