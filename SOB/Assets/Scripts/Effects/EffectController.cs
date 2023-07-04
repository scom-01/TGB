using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class EffectController : MonoBehaviour
{
    public bool isDestroy = false;
    private ParticleSystem particle;

    public void Start()
    {
        particle = this.GetComponent<ParticleSystem>();
        if(particle != null)
        {
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
    public void FinishAnim()
    {
        if(isDestroy)
        {
            Destroy(this.gameObject);
        }
        else
        {
            this.gameObject.SetActive(false);
        }        
    }
}
