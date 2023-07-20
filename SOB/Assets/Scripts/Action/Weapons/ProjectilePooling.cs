using SOB;
using SOB.CoreSystem;
using SOB.Weapons.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ProjectilePooling : MonoBehaviour
{
    public Projectile ProjectilePrefab;
    private Queue<GameObject> m_projectileQueue = new Queue<GameObject>();

    public GameObject CreateObject()
    {
        Projectile obj = Instantiate(GlobalValue.Base_Projectile).GetComponent<Projectile>();
        var projectile_Data = ProjectilePrefab.ProjectileData;
        projectile_Data.Pos += ProjectilePrefab.unit.transform.position;
        obj.FancingDirection = ProjectilePrefab.unit.Core.GetCoreComponent<Movement>().fancingDirection;
        obj.SetUp(projectile_Data);
        obj.Shoot();

        obj.gameObject.SetActive(false);
        return obj.gameObject;
    }
    public void Init(Projectile _projectilePrefab, int count)
    {
        if (_projectilePrefab == null)
            return;

        ProjectilePrefab = _projectilePrefab;

        for (int i = 0; i < count; i++)
        {
            m_projectileQueue.Enqueue(CreateObject());
        }
    }

    public GameObject GetObejct(ProjectileData _projectileData)
    {
        if (m_projectileQueue.Count > 0)
        {
            var obj = m_projectileQueue.Dequeue();
            obj.transform.SetPositionAndRotation(_projectileData.Pos, Quaternion.Euler(_projectileData.Rot));
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            var newobj = CreateObject();
            newobj.transform.SetPositionAndRotation(_projectileData.Pos, Quaternion.Euler(_projectileData.Rot));
            newobj.gameObject.SetActive(true);
            return newobj;
        }
    }

    public void ReturnObject(GameObject obj)
    {
        obj.gameObject.SetActive(false);
        m_projectileQueue.Enqueue(obj);
    }
}