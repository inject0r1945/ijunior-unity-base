using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemiesSpawner : ObjectPool
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private float _delay = 3f;

    private SpawnPoint[] _spawnPoints;
    private System.Random _random;
    private float _spawnTimer;

    private void Awake()
    {
        _spawnPoints = GetComponentsInChildren<SpawnPoint>();
        _random = new System.Random();
    }

    private void Start()
    {
        Initialize(_enemyPrefab.gameObject);
    }

    private void Update()
    {
        if (_spawnTimer > _delay)
        {
            Spawn();
            _spawnTimer = 0;
        }
            
        _spawnTimer += Time.deltaTime;
    }

    protected override void DoAfterResetPool()
    {
        UnspawnAllPoints();
    }

    private void Spawn()
    {
          bool isExistEmptySpawnPoint = TryGetEmptySpawnPoint(out SpawnPoint spawnPoint);

        if (!isExistEmptySpawnPoint)
             return;

        bool isExistNewEnemy = TryGetObject(out GameObject enemyGameObject);

        if (!isExistNewEnemy)
            return;

        Health enemyHealth = enemyGameObject.GetComponent<Health>();
        enemyHealth.Died += OnEnemyDie;

        spawnPoint.Spawn(enemyGameObject);
    }

    private bool TryGetEmptySpawnPoint(out SpawnPoint emptySpawnPoint)
    {
        emptySpawnPoint = null;
        IEnumerable<SpawnPoint> emptySpawnPoints = _spawnPoints.Where(spawnPoint => !spawnPoint.IsSpawned);

        if (emptySpawnPoints.Count() == 0)
            return false;

        int randomPointIndex = _random.Next(0, emptySpawnPoints.Count());
        emptySpawnPoint = emptySpawnPoints.ElementAt(randomPointIndex);

        return true;
    }

    private void OnEnemyDie(Health enemyHealth)
    {
        enemyHealth.Died -= OnEnemyDie;
        IEnumerable enemySpawnPoints = _spawnPoints.Where(spawnPoint => spawnPoint.GetSpawnedObjectInstanceId() == enemyHealth.gameObject.GetInstanceID());

        foreach (SpawnPoint spawnPoint in enemySpawnPoints)
        {
            spawnPoint.Unspawn();
        }
    }

    private void UnspawnAllPoints()
    {
        foreach (SpawnPoint spawnPoint in _spawnPoints)
        {
            spawnPoint.Unspawn();
        }
    }
}
