using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Core
{
    [RequireComponent(typeof(PointsInitializer))]
    public class RandomPointsSpawner : MonoBehaviour
    {
        [SerializeField, Required, AssetsOnly] private GameObject _prefab;
        [SerializeField, Required, MinValue(0)] private int _spawnCount;

        private PointsInitializer _pointsInitializer;
        private List<Transform> _spawnPoints;
        private bool _isInitialized;

        private void Start()
        {
            ValidateInitialization();

            Spawn();
        }

        public void Initialize()
        {
            _pointsInitializer = GetComponent<PointsInitializer>();
            _pointsInitializer.Initialize();
            _spawnPoints = new List<Transform>(_pointsInitializer.Points);

            if (_spawnCount > _spawnPoints.Count)
                _spawnCount = _spawnPoints.Count;

            _isInitialized = true;
        }

        private void ValidateInitialization()
        {
            if (_isInitialized == false)
                throw new System.Exception($"{nameof(RandomPointsSpawner)} is not initialized");
        }

        private void Spawn()
        {
            List<int> usedPoints = new();
            int randomPointIndex = 0;

            for (int x = 0; x < _spawnCount; x++ )
            {
                bool isEnd = false;

                while (isEnd == false)
                {
                    randomPointIndex = GetRandomPointIndex();

                    if (usedPoints.Contains(randomPointIndex) == false || usedPoints.Count == _spawnPoints.Count)
                        isEnd = true;
                }

                InstantiatePrefab(_spawnPoints[randomPointIndex]);
                usedPoints.Add(randomPointIndex);
            }
        }

        private int GetRandomPointIndex()
        {
            return Random.Range(0, _spawnPoints.Count);
        }

        private void InstantiatePrefab(Transform transform)
        {
            Instantiate(_prefab, transform.position, Quaternion.identity, transform);
        }

    }
}
