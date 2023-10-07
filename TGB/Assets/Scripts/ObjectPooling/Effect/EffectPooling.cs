using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class EffectPooling : ObjectPooling
{
    public override GameObject GetObejct(Vector3 pos, Quaternion quaternion, float size = 0)
    {
        if(ObjectQueue.Count > 0)
        {
            var obj = ObjectQueue.Dequeue();
            if(obj != null)
            {
                obj.transform.SetPositionAndRotation(pos, quaternion);
                obj.transform.localScale = Vector3.one + new Vector3(size, size, size);
                obj.gameObject.SetActive(true);
            }
            return obj;
        }
        else
        {
            var newobj = CreateObject(size);
            newobj.transform.SetPositionAndRotation(pos, quaternion);
            newobj.gameObject.SetActive(true);
            return newobj;
        }
    }
}
