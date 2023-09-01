using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : ObjectPool
{
    [SerializeField] private GameObject _objectPrefab;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private float _secondsBetweenSpawn;

    private float _elapsedTime = 0;
    private Transform _spawnPoint;

    private void Start()
    {
        Initialize(_objectPrefab);
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;

        if (_elapsedTime >= _secondsBetweenSpawn && TryGetObject(out GameObject spawnedObject))
        {
            _elapsedTime = 0;

            int spawnPointNumber = Random.Range(0, _spawnPoints.Length);
            _spawnPoint = _spawnPoints[spawnPointNumber];

            SetSpawnedObject(spawnedObject, _spawnPoint.position);
        }
    }

    private void SetSpawnedObject(GameObject spawnedObject, Vector3 spawnPoint)
    {
        spawnedObject.SetActive(true);
        spawnedObject.transform.position = spawnPoint;
    }
}
