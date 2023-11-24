using UnityEngine;

namespace RTS.Resources
{
    [RequireComponent(typeof(SphereCollider))]
    public class Resource : MonoBehaviour
    {
        private bool _isCollected;
        private bool _isReserved;
        private Transform _transform;
        private SphereCollider _collider;

        private void Awake()
        {
            _transform = transform;
            _collider = GetComponent<SphereCollider>();
        }

        public bool IsCollected()
        {
            return _isCollected;
        }

        public bool IsReserved()
        {
            return _isReserved;
        }

        public void Take(Transform target)
        {
            _transform.SetParent(target);
            _transform.position = target.position;
            _collider.enabled = false;
            _isCollected = true;
        }

        public void Reserve()
        {
            _isReserved = true;
        }

        public void Unreserve()
        {
            _isReserved = false;
        }
    }
}