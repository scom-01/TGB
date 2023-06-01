using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class EffectController : MonoBehaviour
{
    public bool isDestroy = false;
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
