using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;


public class Event_Interactive : MonoBehaviour
{
    [SerializeField] private InteractiveObject interactiveObject;

    public void UnInteractive()
    {
        interactiveObject.UnInteractive();
    }

    public void Interactive()
    {
        interactiveObject.Interactive();
    }
}
