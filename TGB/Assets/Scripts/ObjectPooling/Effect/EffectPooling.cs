using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class EffectPooling : ObjectPooling
{
    public override GameObject GetObejct(Vector3 pos, Quaternion quaternion, float size = 0)
    {
        var _size = size;
        if(size == 0)
        {
            _size = 1f;
        }
        if(ObjectQueue.Count > 0)
        {
            var obj = ObjectQueue.Dequeue();
            if(obj != null)
            {
                obj.transform.SetPositionAndRotation(pos, quaternion);
                obj.transform.localScale = new Vector3(_size, _size, _size);
                obj.GetComponent<EffectController>().isInit = true;
                obj.gameObject.SetActive(true);
            }
            return obj;
        }
        else
        {
            var newobj = CreateObject(_size);
            newobj.transform.SetPositionAndRotation(pos, quaternion);
            newobj.GetComponent<EffectController>().isInit = true;
            newobj.gameObject.SetActive(true);
            return newobj;
        }
    }
}
