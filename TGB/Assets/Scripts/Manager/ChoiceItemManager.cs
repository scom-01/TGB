using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChoiceItemManager : MonoBehaviour
{
    public List<GameObject> ItemList = new List<GameObject>();
    [HideInInspector]
    public int ChoiceItemAmount;
    private int CurrChoiceItemAmount = 0;

    private void Awake()
    {
        CurrChoiceItemAmount = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (ItemList.Count == 0)
            return;

        for (int i = 0; i < ItemList.Count; i++)
        {
            if (ItemList[i] == null)
            {
                ItemList.RemoveAt(i);
                CurrChoiceItemAmount++;
            }
            if(CurrChoiceItemAmount >= ChoiceItemAmount)
            {
                DestroyChoiceItemList();
                break;
            }
        }
    }

    private void DestroyChoiceItemList()
    {
        for (int i = 0; i < ItemList.Count; i++)
        {
            if (ItemList[i].gameObject != null)
                Destroy(ItemList[i].gameObject);
        }
        ItemList.Clear();
    }
}
