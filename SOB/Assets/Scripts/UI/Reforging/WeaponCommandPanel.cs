using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponCommandPanel : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private WeaponCommandDataSO WeaponCommandDataSO;

    public GameObject CommandPanelPrefeb;
    [SerializeField] private List<CommandList> GroundedCommandList
    {
        get
        {
            if (WeaponCommandDataSO != null)
            {
                return WeaponCommandDataSO.GroundedCommandList;
            }

            return null;
        }
    }
    [SerializeField] private List<CommandList> InAirCommandList
    {
        get
        {
            if (WeaponCommandDataSO != null)
            {
                return WeaponCommandDataSO.AirCommandList;
            }

            return null;
        }
    }

    public void SetWeaponData(WeaponCommandDataSO data)
    {
        WeaponCommandDataSO = data;
    }

    public void SetRendering()
    {
        
    }
}
