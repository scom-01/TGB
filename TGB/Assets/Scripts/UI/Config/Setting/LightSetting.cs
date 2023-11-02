using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightSetting : MonoBehaviour
{
    private Light2D light2D;

    private void Awake()
    {
        light2D = this.GetComponent<Light2D>();
    }

    private void OnEnable()
    {
        if (GameManager.Inst != null)
        {

        }
        light2D.enabled = true;
    }

    private void OnDisable()
    {
        light2D.enabled = false;
    }
}