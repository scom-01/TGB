using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newWeaponDB", menuName = "Data/DB/WeaponDB")]
public class WeaponDB : ScriptableObject
{
    public List<WeaponCommandDataSO> WeaponDBList;
}

