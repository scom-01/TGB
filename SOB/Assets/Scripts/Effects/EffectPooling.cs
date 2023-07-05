using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class EffectPooling : MonoBehaviour
{    
    public GameObject Effect;
    private Queue<GameObject> effectQueue = new Queue<GameObject>();

    public GameObject CreateObject()
    {
        var newobj = Instantiate(Effect, transform);
        newobj.gameObject.SetActive(false);
        return newobj; 
    }
    public void Init(GameObject _effect, int count)
    {
        if (_effect == null)
            return;

        Effect = _effect;

        for(int i = 0; i< count; i++)
        {
            effectQueue.Enqueue(CreateObject());
        }
    }

    public GameObject GetObejct(Vector3 pos, Quaternion quaternion)
    {
        if(effectQueue.Count > 0)
        {
            var obj = effectQueue.Dequeue();
            obj.transform.SetPositionAndRotation(pos, quaternion);
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            var newobj = CreateObject();
            newobj.transform.SetPositionAndRotation(pos, quaternion);
            newobj.gameObject.SetActive(true);
            return newobj;
        }
    }

    public void ReturnObject(GameObject obj)
    {
        obj.gameObject.SetActive(false);        
        effectQueue.Enqueue(obj);
    }
}
