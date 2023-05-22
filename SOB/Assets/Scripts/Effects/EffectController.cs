using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectController : MonoBehaviour
{
    private void FinishAnim()
    {
        this.gameObject.SetActive(false);        
    }

    private void DestroyAnim()
    {
        Destroy(gameObject);
    }
}
