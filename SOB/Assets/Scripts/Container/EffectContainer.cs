using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.Animations;
using UnityEngine;
using static UnityEditor.PlayerSettings;


public class EffectContainer : MonoBehaviour
{
    public List<GameObject> EffectList = new List<GameObject>();
    public List<RuntimeAnimatorController> EffectControllerList = new List<RuntimeAnimatorController>();

    public GameObject AddEffectList(GameObject effectPrefab , Vector3 vector, Quaternion position,Transform transform)
    {
        if(EffectControllerList.Contains(effectPrefab.GetComponent<Animator>().runtimeAnimatorController))
        {
            var idx = EffectControllerList.FindIndex(x => x == effectPrefab.GetComponent<Animator>().runtimeAnimatorController);
            if(idx != -1)
            {                
                EffectList[idx].transform.SetPositionAndRotation(vector, position);
                EffectList[idx].gameObject.SetActive(true);                
                //EffectList[idx].GetComponent<ParticleSystem>().Play();
                return EffectList[idx];
            }
            return null;
        }
        else
        {
            var effect = Instantiate(effectPrefab, vector, position, transform);
            EffectList.Add(effect);
            EffectControllerList.Add(effect.GetComponent<Animator>().runtimeAnimatorController);
            return effect;
        }
    }
}
