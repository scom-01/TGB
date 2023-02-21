using SOB.Item;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOB.Manager
{
    public class SpawnManager : MonoBehaviour
    {
        public void SpawnItem(GameObject SpawnPrefab, Vector3 pos,Transform transform, StatsItemSO itemData)
        {
            var item = Instantiate(SpawnPrefab, pos, Quaternion.identity, transform);
            item.GetComponent<SOB_Item>().Item = itemData;
            item.GetComponent<SOB_Item>().Init();
        }
    }
}
