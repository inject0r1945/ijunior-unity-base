using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner : MonoBehaviour
{
    [SerializeField] private Enemy _templateEnemy;
    [SerializeField] private float _speed = 1f;

    private Transform[] _respawnPoints;

    private void Awake()
    {
        _respawnPoints = new Transform[transform.childCount];

        for (int childIndex = 0;  childIndex < transform.childCount; childIndex++)
            _respawnPoints[childIndex] = transform.GetChild(childIndex);

        if (_respawnPoints.Length == 0)
            throw new System.Exception("Не найдены точки появления врагов");
    }

    private void Start()
    {
        StartCoroutine(CreateEnemies());
    }

    private IEnumerator CreateEnemies()
    {
        bool isEnd = false;
        int currentRespawnPointNumber = 0;
        var waitForSpeedSeconds = new WaitForSeconds(_speed);


        while (isEnd == false)
        {
            Transform respawnPoint = _respawnPoints[currentRespawnPointNumber];
            Enemy newObject = Instantiate(_templateEnemy, 
                respawnPoint.position, Quaternion.identity);

            yield return waitForSpeedSeconds;

            currentRespawnPointNumber++;

            if (currentRespawnPointNumber >= _respawnPoints.Length)
                currentRespawnPointNumber = 0;
        }
    }
}
