using System.Drawing;
using System.Xml.Linq;
using UnityEngine;


public class TouchObject : MonoBehaviour, ITouch
{
    protected Transform effectContainer;
    public AudioClip SfxObject;
    public GameObject EffectObject;
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
