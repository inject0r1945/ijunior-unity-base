using UnityEngine;

namespace RTS.Resources
{
    [RequireComponent(typeof(SphereCollider))]
    public class Resource : MonoBehaviour
    {
        private Transform _transform;
        private SphereCollider _collider;

        private void Awake()
        {
            _transform = transform;
            _collider = GetComponent<SphereCollider>();
        }

        public void Take(Transform target)
        {
            _transform.SetParent(target);
            _transform.position = target.position;
            _collider.enabled = false;
        }
    }
}