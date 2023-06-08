using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayTimeManagerUI : MonoBehaviour
{
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

}
