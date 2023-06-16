using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class PlayTimeUI : MonoBehaviour
{
    private TextMeshProUGUI TimeTextUI;

    private int hour;
    private int min;
    private int sec;

    private void Awake()
    {
        TimeTextUI = this.GetComponent<TextMeshProUGUI>();
    }
    private void Update()
    {
        if (TimeTextUI == null)
        {
            TimeTextUI = this.GetComponent<TextMeshProUGUI>();
        }

        if (GameManager.Inst == null)
            return;
        CalculatePlayTime(GameManager.Inst.PlayTime);
        TimeTextUI.text = string.Format("{0:D2} : {1:D2} : {2:D2}", hour, min, sec);
    }

    private void CalculatePlayTime(float _time)
    {
        hour = ((int)_time / 3600);
        min = ((int)_time / 60 % 60);
        sec = (int)_time % 60;
    }
}