using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnEnemy : MonoBehaviour
{
    [SerializeField] private Enemy _templateEnemy;
    [SerializeField] private float _speed = 1f;

    private void Start()
    {
        StartCoroutine(CreateEnemy());
    }

    private IEnumerator CreateEnemy()
    {
        bool isEnd = false;

        while (isEnd == false)
        {
            Enemy newObject = Instantiate(_templateEnemy, transform.position, Quaternion.identity);

            yield return new WaitForSeconds(_speed);
        }
    }
}
