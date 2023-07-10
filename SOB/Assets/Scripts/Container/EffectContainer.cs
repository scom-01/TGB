using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine.Animations;
using UnityEngine;
using Unity.VisualScripting;
using SOB.CoreSystem;

public class EffectContainer : MonoBehaviour
{
    public List<GameObject> EffectList = new List<GameObject>();
    public List<RuntimeAnimatorController> EffectControllerList = new List<RuntimeAnimatorController>();

    private GameObject EffectPoolingBase
    {
        get
        {
            if (effectPoolingBase == null)
            {
                effectPoolingBase = Resources.Load<GameObject>("Prefabs/Effects/Base_EffectPooling");
            }
            return effectPoolingBase;
        }
    }
    private GameObject effectPoolingBase;
    public List<EffectPooling> EffectPoolList = new List<EffectPooling>();
    public GameObject AddEffectList(GameObject effectPrefab, Vector3 vector, Quaternion position, Transform transform)
    {
        if (EffectControllerList.Contains(effectPrefab.GetComponent<Animator>().runtimeAnimatorController))
        {
            var idx = EffectControllerList.FindIndex(x => x == effectPrefab.GetComponent<Animator>().runtimeAnimatorController);
            if (idx != -1)
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


    /// <summary>
    /// EffectPoolingList에 Effect가 존재하는 지 판단 후 return
    /// 없다면 생성 후 return
    /// </summary>
    /// <param name="_effect"></param>
    /// <returns></returns>
    public EffectPooling CheckEffect(GameObject _effect, Transform transform = null)
    {
        if (transform == this.transform)
        {
            if (EffectPoolList.Count == 0)
            {
                var effect = AddEffect(_effect, 5, transform);
                return effect.GetComponent<EffectPooling>();
            }

            for (int i = 0; i < EffectPoolList.Count; i++)
            {
                if (EffectPoolList[i].Effect == _effect)
                {
                    return EffectPoolList[i];
                }
            }

            var newEffect = AddEffect(_effect, 5, transform).GetComponent<EffectPooling>();
            return newEffect;
        }
        else
        {
            var _EffectPoolList = transform.GetComponent<EffectManager>().EffectPoolList;

            if (_EffectPoolList.Count == 0)
            {
                var effect = AddEffect(_effect, _EffectPoolList, 5, transform);
                return effect.GetComponent<EffectPooling>();
            }

            for (int i = 0; i < _EffectPoolList.Count; i++)
            {
                if (_EffectPoolList[i].Effect == _effect)
                {
                    return _EffectPoolList[i];
                }
            }

            var newEffect = AddEffect(_effect, _EffectPoolList, 5, transform).GetComponent<EffectPooling>();
            return newEffect;
        }        
    }

    /// <summary>
    /// EffectPoolingList에 EffectPoolingBase를 기반으로 EffectPooling 생성 후 amount 수 만큼 effectPool 생성
    /// </summary>
    /// <param name="_effect"></param>
    /// <returns></returns>
    private GameObject AddEffect(GameObject _effect, int amount = 5, Transform transform = null)
    {
        var effectPool = Instantiate(EffectPoolingBase, transform ? transform : this.transform);
        effectPool.GetComponent<EffectPooling>().Init(_effect, amount);
        EffectPoolList.Add(effectPool.GetComponent<EffectPooling>());
        return effectPool;
    }
    private GameObject AddEffect(GameObject _effect, List<EffectPooling> effectPoolList, int amount = 5, Transform transform = null)
    {
        var effectPool = Instantiate(EffectPoolingBase, transform ? transform : this.transform);
        effectPool.GetComponent<EffectPooling>().Init(_effect, amount);
        effectPoolList.Add(effectPool.GetComponent<EffectPooling>());
        return effectPool;
    }
}