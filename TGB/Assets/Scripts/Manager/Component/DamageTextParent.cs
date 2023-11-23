using UnityEngine;


[RequireComponent(typeof(RectTransform))]
public class DamageTextParent : MonoBehaviour
{
    private bool isOn =false;
    private Vector3 lastCamPos = new Vector3();
    private RectTransform RT
    {
        get
        {
            if(rectTransform==null)
            {
                rectTransform = this.GetComponent<RectTransform>();
            }
            return rectTransform;
        }
        set
        {
            rectTransform = value;
        }
    }
    private RectTransform rectTransform = null;
    private Camera Cam
    {
        get
        {
            if(cam ==null)
            {
                cam = Camera.main;
            }
            return cam;
        }
    }
    private Camera cam = null;
    private void OnEnable()
    {
        isOn = true;
        lastCamPos = Cam.transform.position;
    }
    private void LateUpdate()
    {
        if (!isOn)
            return;
        Vector3 deltaMovement = Cam.transform.position - lastCamPos;

        //현재 위치를 WorldSpace 위치로 전환
        var ScreenPos = Cam.ScreenToWorldPoint(RT.position);

        //전환한 위치를 카메라가 이동한만큼 빼주어 카메라가 볼 때 제자리에 있는 것 처럼
        ScreenPos -= deltaMovement;
        //ScreenPos를 viewport로 전환
        var _pos = new Vector2(
            (Cam.WorldToViewportPoint(ScreenPos).x * GameManager.Inst.DamageUI.GetComponent<RectTransform>().sizeDelta.x) - (GameManager.Inst.DamageUI.GetComponent<RectTransform>().sizeDelta.x * 0.5f),
            (Cam.WorldToViewportPoint(ScreenPos).y * GameManager.Inst.DamageUI.GetComponent<RectTransform>().sizeDelta.y) - (GameManager.Inst.DamageUI.GetComponent<RectTransform>().sizeDelta.y * 0.5f)
                            );
        //전환한 Viewport 좌료를 RectTransform에 연결
        RT.anchoredPosition = _pos;

        lastCamPos = Cam.transform.position;
    }

    private void OnDisable()
    {
        isOn = false;
    }
}
