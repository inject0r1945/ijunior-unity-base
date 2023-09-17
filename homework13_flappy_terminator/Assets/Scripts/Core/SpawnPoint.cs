using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private GameObject _spawnedObject;

    public bool IsSpawned => _spawnedObject != null;

    private void Update()
    {
        if (!IsSpawned)
            return;

        _spawnedObject.transform.position = transform.position;
    }

    private void OnDrawGizmos()
    {
        float radius = 0.1f;

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, radius);
    }

    public void Spawn(GameObject objectForSpawn)
    {
        _spawnedObject = objectForSpawn;
        _spawnedObject.transform.position = transform.position;
        _spawnedObject.SetActive(true);
    }

    public void Unspawn()
    {
        if (_spawnedObject == null)
            return;

        _spawnedObject.SetActive(false);
        _spawnedObject = null;
    }

    public int GetSpawnedObjectInstanceId()
    {
        int instanceId = -1;

        if (_spawnedObject != null)
            instanceId = _spawnedObject.GetInstanceID();

        return instanceId;
    }
}
