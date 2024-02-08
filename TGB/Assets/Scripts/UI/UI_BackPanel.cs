using UnityEngine;
using UnityEngine.EventSystems;

namespace SCOM
{
    public class UI_BackPanel : MonoBehaviour,IPointerDownHandler
    {
        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            GameManager.Inst?.SetSelectedObject(EventSystem.current.currentSelectedGameObject);
        }
    }
}
