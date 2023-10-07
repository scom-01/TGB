using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newBuffDB", menuName = "Data/DB/BuffDB")]
public class BuffDB : ScriptableObject
{
    public List<BuffItemSO> BuffDBList;
}
