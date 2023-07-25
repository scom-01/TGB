using SOB;
using SOB.CoreSystem;
using SOB.Weapons.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ProjectilePooling : MonoBehaviour
{
    public GameObject Effect;
    public ProjectileData m_ProjectileData;
    public int MaxPoolAmount;
    private Queue<GameObject> m_projectileQueue = new Queue<GameObject>();

    public GameObject CreateObject()
    {
        Projectile obj = Instantiate(GlobalValue.Base_Projectile, transform).GetComponent<Projectile>();
        var projectile_Data = m_ProjectileData;
        obj.Init(projectile_Data);
        obj.gameObject.SetActive(false);
        return obj.gameObject;
    }

    public void Init(ProjectileData _projectilePrefab, int count)
    {
        m_ProjectileData = _projectilePrefab;
        Effect = _projectilePrefab.ProjectilePrefab;
        MaxPoolAmount = count;

        for (int i = 0; i < MaxPoolAmount; i++)
        {
            m_projectileQueue.Enqueue(CreateObject());
        }
    }

    public GameObject GetObejct(Unit unit, ProjectileData _projectilData)
    {
        if (m_projectileQueue.Count > 0)
        {
            var obj = m_projectileQueue.Dequeue();
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

    public void ReturnObject(GameObject obj)
    {
        if (m_projectileQueue.Count >= MaxPoolAmount)
        {
            Destroy(obj.gameObject);
        }
        else
        {
            obj.gameObject.SetActive(false);
            m_projectileQueue.Enqueue(obj);
        }
    }
}