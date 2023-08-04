using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Purchasing;
using UnityEngine;


public class TouchObject : MonoBehaviour, ITouch
{
    [TagField]
    public string Tag;

    protected Transform effectContainer;

    private void Awake()
    {
        effectContainer = GameObject.FindGameObjectWithTag("EffectContainer").transform;
    }
    public virtual void Touch()
    {

    }
    public virtual void UnTouch()
    {

    }
    public virtual void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == Tag)
            return;
    }
}
