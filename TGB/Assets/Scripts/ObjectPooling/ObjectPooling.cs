using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectPooling : MonoBehaviour
{
    public GameObject Object;
    public int MaxPoolAmount;
    protected Queue<GameObject> ObjectQueue = new Queue<GameObject>();

    public virtual GameObject CreateObject(float size = 0)
    {
        var newobj = Instantiate(Object, transform);
        newobj.transform.localScale = new Vector3(size, size, size);
        newobj.gameObject.SetActive(false);
        return newobj;
    }

    public virtual void Init(GameObject _obj, int count, float size = 0)
    {
        if (_obj == null)
            return;

        Object = _obj;
        MaxPoolAmount = count;

        for (int i = 0; i < MaxPoolAmount; i++)
        {
            ObjectQueue.Enqueue(CreateObject(size));
        }
    }

    public virtual GameObject GetObejct(Vector3 pos, Quaternion quaternion, float size = 0)
    {
        if (ObjectQueue.Count > 0)
        {
            var obj = ObjectQueue.Dequeue();
            if (obj != null)
            {
                obj.transform.SetPositionAndRotation(pos, quaternion);
                obj.gameObject.SetActive(true);
            }
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

    public virtual void ReturnObject(GameObject obj)
    {
        if (ObjectQueue.Count >= MaxPoolAmount)
        {
            Destroy(obj.gameObject);
        }
        else
        {
            obj.gameObject.SetActive(false);
            ObjectQueue.Enqueue(obj);
        }
    }
}
