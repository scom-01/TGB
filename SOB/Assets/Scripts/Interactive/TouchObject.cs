using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Purchasing;
using UnityEngine;


public class TouchObject : MonoBehaviour, ITouch
{
    protected Transform effectContainer;

    private void Awake()
    {
        effectContainer = GameObject.FindGameObjectWithTag("EffectContainer").transform;
        this.gameObject.layer = LayerMask.NameToLayer("Area");
    }
    public virtual void Touch()
    {

    }
    public virtual void UnTouch()
    {

    }
    public virtual void OnTriggerStay2D(Collider2D collision)
    {
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
    }

    public virtual void OnTriggerExit2D(Collider2D collision)
    {
    }
}
