using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : ObjectPool
{
    [SerializeField] private GameObject _objectPrefab;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private float _delay;

    private Transform _spawnPoint;

    private void Start()
    {
        Initialize(_objectPrefab);
        StartCoroutine(StartSpawnProcess());
    }

    private IEnumerator StartSpawnProcess()
    {
        var waitForSeconds = new WaitForSeconds(_delay);
        bool isEnd = false;

        while (!isEnd)
        {
            if (TryGetObject(out GameObject spawnedObject))
            {
                int spawnPointNumber = Random.Range(0, _spawnPoints.Length);
                _spawnPoint = _spawnPoints[spawnPointNumber];

                SetSpawnedObject(spawnedObject, _spawnPoint.position);
            }

            yield return waitForSeconds;
        }
    }

    private void SetSpawnedObject(GameObject spawnedObject, Vector3 spawnPoint)
    {
        spawnedObject.SetActive(true);
        spawnedObject.transform.position = spawnPoint;
    }
}
