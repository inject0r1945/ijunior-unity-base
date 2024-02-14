using UnityEngine;

namespace RTS.Resources
{
    public class ResourcesSpawner : MonoBehaviour
    {
        [SerializeField] private float _radius;
        [SerializeField] private ResourceInfo _resourceInfo;
        [SerializeField] private int _count = 5;
        [SerializeField] float _delay = 2f;

        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        private void Start()
        {
            Invoke(nameof(InstantiateResources), _delay);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _radius);
        }

        private void InstantiateResources()
        {
            for (int i = 0; i < _count; i++)
            {
                Vector3 position = Random.insideUnitSphere * _radius + _transform.position;
                position.y = 0;

                _resourceInfo.Spawn(position, Quaternion.identity, _transform);
            }
        }
    }
}