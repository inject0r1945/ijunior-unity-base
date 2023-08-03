using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private Transform _pointsTree;

    private Transform[] spawnPoints;

    private void Awake()
    {
        int spawnPointsCount = _pointsTree.childCount;
        spawnPoints = new Transform[spawnPointsCount];

        for (int i = 0; i < spawnPointsCount; i++ )
        {
            spawnPoints[i] = _pointsTree.GetChild(i);
        }
    }

    private void Start()
    {
        foreach (Transform spawnPoint in spawnPoints)
            Instantiate(_prefab, spawnPoint.position, Quaternion.identity, _pointsTree);
    }
}
