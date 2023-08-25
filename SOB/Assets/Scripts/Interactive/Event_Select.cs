using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// UI에 부가적인 스크립트
/// 현재 UI가 OnSelect되었을 때 TartgetObjec의 Select(go)함수 호출
/// TargetObject에는 IUI_Select인터페이스가 포함되어야한다.
/// </summary>
public class Event_Select : MonoBehaviour, ISelectHandler
{
    public GameObject TargetObject;

    //OnSelect
    void ISelectHandler.OnSelect(BaseEventData eventData)
    {
        if (eventData.selectedObject == this.gameObject && TargetObject != null)
        {
            if(TargetObject.TryGetComponent(out IUI_Select uI_Select))
            {
                uI_Select.Select(this.gameObject);
            }
        }
    }
}
