using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private int _capacity;

    private List<GameObject> _pool = new List<GameObject>();

    protected void Initialize(GameObject prefab)
    {
        for (int i = 0; i < _capacity; i++)
        {
            GameObject spawnedObject = Instantiate(prefab, _container);
            spawnedObject.SetActive(false);
            _pool.Add(spawnedObject);
        }
    }

    protected bool TryGetObject(out GameObject gameObject)
    {
        gameObject = _pool.FirstOrDefault(p => p.activeSelf == false);

        return gameObject != null;
    }
}
