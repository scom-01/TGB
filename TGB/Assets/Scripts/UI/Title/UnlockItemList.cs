using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockItemList : MonoBehaviour
{
    public GameObject UnlockItemPrefab;
    [HideInInspector] public List<GameObject> UnlockItems = new List<GameObject>();
    [ContextMenu("Set Init")]
    public void SetInit()
    {
        if (UnlockItemPrefab == null)
        {
            return;
        }

        #region Clean ChildTransform
        Transform[] childObjects = this.GetComponentsInChildren<Transform>();

        if (childObjects != null)
        {
            for (int i = 1; i < childObjects.Length; i++)
            {
                if (childObjects[i] != null)
                {
#if UNITY_EDITOR
                    DestroyImmediate(childObjects[i].gameObject);
#else
                    Destroy(childObjects[i].gameObject);
#endif
                }
            }
            UnlockItems?.Clear();
        }
        #endregion

        //모든 아이템 추가
        for (int i = 0; i < GlobalValue.All_ItemDB.ItemDBList.Count; i++)
        {
            var item = Instantiate(UnlockItemPrefab, this.transform);
            item.GetComponent<UnlockItemData>().SetData(i);
            UnlockItems.Add(item);
        }

        //각 Item Nav 설정
        for (int i = 0; i < GlobalValue.All_ItemDB.ItemDBList.Count; i++)
        {
            Navigation nav = UnlockItems[i].GetComponent<Button>().navigation;
            if (i > 0)
            {
                nav.selectOnLeft = UnlockItems[i - 1].GetComponent<Button>();
            }

            if (i + 1 < GlobalValue.All_ItemDB.ItemDBList.Count)
            {
                nav.selectOnRight = UnlockItems[i + 1].GetComponent<Button>();
            }

            if (i - 8 >= 0)
            {
                nav.selectOnUp = UnlockItems[i - 8].GetComponent<Button>();
            }
            if (i + 8 < GlobalValue.All_ItemDB.ItemDBList.Count)
            {
                nav.selectOnDown = UnlockItems[i + 8].GetComponent<Button>();
            }

            UnlockItems[i].GetComponent<Button>().navigation = nav;
        }
    }
}
