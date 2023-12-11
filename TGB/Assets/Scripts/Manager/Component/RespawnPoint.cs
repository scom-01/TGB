using Unity.Mathematics;
using UnityEngine;


public class RespawnPoint : MonoBehaviour
{
    /// <summary>
    /// Spawn할 Prefab;
    /// </summary>
    public GameObject SpawnPrefab;
    /// <summary>
    /// spawn된 Object;
    /// </summary>
    private GameObject spawnObject;
    /// <summary>
    /// Spawn할 Effect Prefab;
    /// </summary>
    public GameObject SpawnEffectPrefab;
    /// <summary>
    /// Spawn된 Effect Object;
    /// </summary>
    private GameObject spawnEffectObject;

    private bool isEffectSpawn = false;
    private bool isSpawn = false;
    private bool FinishSpawn = false;
    public void Update()
    {
        if (spawnEffectObject == null && isEffectSpawn)
        {
            if(SpawnPrefab != null)
                Spawn(SpawnPrefab, this.transform.position, this.transform);
            isEffectSpawn = false;
        }

        if (spawnObject != null && !spawnObject.activeSelf && isSpawn && !FinishSpawn)
        {
            FinishSpawn = true;
            this.GetComponentInParent<SpawnCtrl>().CurrentEnemyCount--;
        }
    }

    public void SpawnEffect(GameObject effectPrefab, Vector3 pos, Transform transform)
    {
        spawnEffectObject = Instantiate(effectPrefab, pos, Quaternion.identity, transform);
        isEffectSpawn = true;
    }
    /// <summary>
    /// TimeLine
    /// </summary>
    /// <param name="effectPrefab"></param>
    public void SpawnEffect(GameObject director)
    {
        spawnEffectObject = Instantiate(director);
        isEffectSpawn = true;
    }
    public void Spawn(GameObject spawn, Vector3 pos, Transform transform)
    {
        spawnObject = Instantiate(spawn, pos, Quaternion.identity, transform);
        isSpawn = true;
    }
}
