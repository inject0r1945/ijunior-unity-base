using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype.Control
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private EnemyController _enemyPrefab;
        [SerializeField] private Transform _target;
        [SerializeField] private int _count = 5;
        [SerializeField] private float _delay = 1f;

        private void Start()
        {
            StartCoroutine(nameof(Spawn));
        }

        private IEnumerator Spawn()
        {
            var waitForSeconds = new WaitForSeconds(_delay);

            for (int i = 0; i < _count; i++)
            {
                EnemyController enemy = Instantiate(_enemyPrefab, transform.position, Quaternion.identity, transform);
                enemy.Init(_target);

                yield return waitForSeconds;
            }
        }
    }
}