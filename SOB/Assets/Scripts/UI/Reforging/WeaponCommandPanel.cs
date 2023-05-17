using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponCommandPanel : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private WeaponCommandDataSO WeaponCommandDataSO;

    public Transform GroundContent;
    public Transform InAirContent;

    public GameObject CommandPanelPrefeb;
    public GameObject CommandPrimaryKeyPrefeb;
    public GameObject CommandSecondaryKeyPrefeb;
    [SerializeField]
    private List<CommandList> GroundedCommandList
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
    [SerializeField]
    private List<CommandList> InAirCommandList
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

    public Canvas Canvas
    {
        get
        {
            if (canvas == null)
            {
                canvas = GetComponent<Canvas>();
            }
            return canvas;
        }
    }
    private Canvas canvas;

    private void CommandPanel(CommandEnum command, Transform _transform)
    {
        if (command == CommandEnum.Primary)
        {
            Instantiate(CommandPrimaryKeyPrefeb, _transform.transform);
        }
        else if (command == CommandEnum.Secondary)
        {
            Instantiate(CommandSecondaryKeyPrefeb, _transform.transform);
        }
    }

    private void ClearCommandPanel()
    {
        for (int i = 0; i < GroundContent.childCount; i++)
        {
            Destroy(GroundContent.GetChild(i).gameObject);
        }

        for (int i = 0; i < InAirContent.childCount; i++)
        {
            Destroy(InAirContent.GetChild(i).gameObject);
        }
    }

    public void SetWeaponData(WeaponCommandDataSO data)
    {
        if(data == WeaponCommandDataSO)
        {
            Debug.Log("동일한 WeaponCommandDataSO 데이터");
            return;
        }    
        WeaponCommandDataSO = data;
        SetRendering();
    }

    public void SetRendering()
    {
        ClearCommandPanel();
        
        Canvas.enabled = WeaponCommandDataSO != null ? true : false;

        if (GroundedCommandList != null)
        {
            Debug.Log($"GroundedCommandList = {GroundedCommandList}");

            for (int i = 0; i < GroundedCommandList.Count; i++)
            {
                var content = Instantiate(CommandPanelPrefeb, GroundContent);
                for (int j = 0; j < GroundedCommandList[i].commands.Count; j++)
                {
                    CommandPanel(GroundedCommandList[i].commands[j].command, content.transform);
                }
            }
        }
        
        if (InAirCommandList != null)
        {
            Debug.Log($"InAirCommandList = {InAirCommandList}");
            for (int i = 0; i < InAirCommandList.Count; i++)
            {
                var content = Instantiate(CommandPanelPrefeb, InAirContent);
                for (int j = 0; j < InAirCommandList[i].commands.Count; j++)
                {
                    CommandPanel(InAirCommandList[i].commands[j].command, content.transform);
                }
            }
        }
    }
}
