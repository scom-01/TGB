using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class EffectController : MonoBehaviour
{
    public bool isDestroy = false;
    private ParticleSystem particle;
    private EffectPooling parent;

    public void Start()
    {
        particle = this.GetComponent<ParticleSystem>();
        parent = this.GetComponentInParent<EffectPooling>();
        if (particle != null)
        {
            foreach(var childparticles in particle.GetComponentsInChildren<ParticleSystemRenderer>())
            {
                childparticles.sortingLayerName = "Effect";
            }
            var main = particle.main;
            if (isDestroy)
            {
                main.stopAction = ParticleSystemStopAction.Destroy;
            }
            else
            {                
                main.stopAction = ParticleSystemStopAction.Disable;
            }
        }
    }

    private void OnDisable()
    {
        if (particle != null)
        {
            if (parent != null)
            {
                parent.ReturnObject(this.gameObject);
            }
            else
            {
                this.gameObject.SetActive(false);
            }
        }
    }
    public void FinishAnim()
    {
        if(isDestroy)
        {
            Destroy(this.gameObject);
        }
        else
        {
            if(parent != null)
            {
                parent.ReturnObject(this.gameObject);
            }
            else
            {
                this.gameObject.SetActive(false);
            }
        }        
    }
}
