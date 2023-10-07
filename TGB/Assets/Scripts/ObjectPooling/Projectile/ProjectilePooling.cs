using TGB;
using TGB.CoreSystem;
using TGB.Weapons.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class ProjectilePooling : ObjectPooling
{
    public ProjectileData m_ProjectileData;

    public override GameObject CreateObject(float size = 0)
    {
        Projectile obj = Instantiate(Object, transform).GetComponent<Projectile>();
        var projectile_Data = m_ProjectileData;
        obj.Init(projectile_Data);
        obj.gameObject.SetActive(false);
        return obj.gameObject;
    }

    public void Init(ProjectileData _projectilePrefab, GameObject _obj, int count)
    {
        m_ProjectileData = _projectilePrefab;
        Object = _obj;
        MaxPoolAmount = count;

        for (int i = 0; i < MaxPoolAmount; i++)
        {
            ObjectQueue.Enqueue(CreateObject());
        }
    }

    public GameObject GetObejct(Unit unit, ProjectileData _projectilData)
    {
        if (ObjectQueue.Count > 0)
        {
            var obj = ObjectQueue.Dequeue();
            if(obj != null)
            {
                obj.GetComponent<Projectile>().SetUp(unit, _projectilData);
                obj.gameObject.SetActive(true);
                obj.GetComponent<Projectile>().Shoot();
            }            
            return obj;
        }
        else
        {
            var newobj = CreateObject();
            newobj.GetComponent<Projectile>().SetUp(unit, _projectilData);
            newobj.gameObject.SetActive(true);
            newobj.GetComponent<Projectile>().Shoot();
            return newobj;
        }
    }
}