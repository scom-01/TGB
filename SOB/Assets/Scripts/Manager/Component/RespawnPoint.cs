using Unity.Mathematics;
using UnityEngine;


public class RespawnPoint : MonoBehaviour
{
    public GameObject SpawnPrefab;
    private GameObject spawnPrefab;
    public GameObject SpawnEffectPrefab;
    private GameObject spawnEffectPrefab;

    public bool isEffectSpawn = false;
    public bool isSpawn = false;
    public bool FinishSpawn = false;
    public void Update()
    {
        if (spawnEffectPrefab == null && isEffectSpawn)
        {
            Spawn(SpawnPrefab, this.transform.position, this.transform);
            isEffectSpawn = false;
        }

        if (spawnPrefab != null && !spawnPrefab.activeSelf && isSpawn && !FinishSpawn)
        {
            FinishSpawn = true;
            this.GetComponentInParent<SpawnCtrl>().currentCount--;
        }
    }
    public void SpawnEffect(GameObject effectPrefab, Vector3 pos, Transform transform)
    {
        spawnEffectPrefab = Instantiate(effectPrefab, pos, Quaternion.identity, transform);
        isEffectSpawn = true;
    }

    public void Spawn(GameObject spawn, Vector3 pos, Transform transform)
    {
        spawnPrefab = Instantiate(spawn, pos, Quaternion.identity, transform);
        isSpawn = true;
    }
}
