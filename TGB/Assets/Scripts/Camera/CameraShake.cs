using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraShake : MonoBehaviour
{
    /// <summary>
    /// 메소드 실행까지의 시간
    /// </summary>
    [field: SerializeField] public float time;
    /// <summary>
    /// 몇 초마다 실행시킬 것인가
    /// </summary>
    [field: SerializeField] public float repeatRate;
    /// <summary>
    /// 실행할 메소드 명
    /// </summary>
    [field: SerializeField] public float shakeRange = 0.5f;
    [field: SerializeField] public float duration;

    public Camera mainCamera;

    Vector3 cameraPos;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.U))
        {
            Shake(repeatRate, shakeRange, duration);
        }

    }

    public void Shake(float _repeatRate = 0.0f, float _shakeRange = 0.0f, float _duration = 0.1f)
    {
        cameraPos = mainCamera.transform.position;
        shakeRange = _shakeRange;
        InvokeRepeating("StartShake", 0f, _repeatRate);
        Invoke("StopShake", _duration);
    }
    void StartShake()
    {
        float cameraPosX = Random.value + shakeRange * 2 - shakeRange;
        float cameraPosY = Random.value + shakeRange * 2 - shakeRange;
        Vector3 cameraPos = mainCamera.transform.position;
        cameraPos.x += cameraPosX;
        cameraPos.y += cameraPosY;
        mainCamera.transform.position = cameraPos;
    }
    void StopShake()
    {
        CancelInvoke("StartShake");
        mainCamera.transform.position = cameraPos;
    }
}