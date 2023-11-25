using UnityEngine;
using RTS.Resources;

namespace RTS.Units
{
    [RequireComponent(typeof(Mover))]
    public class ResourcesCollectorUnit : MonoBehaviour
    {
        [SerializeField] private Transform _resourcePoint;

        private Mover _mover;
        private Transform _transform;
        private Collider _baseCollider;
        private Resource _resource;
        private ResourceCollectorState _state;
        private Transform _target;

        public bool IsBusy => _state != ResourceCollectorState.Free;

        public ResourceCollectorState State => _state;

        private void Awake()
        {
            _mover = GetComponent<Mover>();
            _transform = transform;
        }

        private void Start()
        {
            SetState(ResourceCollectorState.Free);
        }

        private void OnTriggerStay(Collider other)
        {
            OnTriggerEnter(other);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_state == ResourceCollectorState.Free)
                return;

            if (_resource == null && _state == ResourceCollectorState.FindResource && other.TryGetComponent(out Resource resource)
                && _target.position == resource.transform.position)
            {
                resource.Take(_resourcePoint);
                _mover.Move(_baseCollider.ClosestPoint(_transform.position));
                _resource = resource;

                return;
            }

            if (_resource == null)
                return;

            IResourceReceiver resourceReceiver = other.transform.GetComponentInParent<IResourceReceiver>();

            if (resourceReceiver != null)
            {
                resourceReceiver.ReceiveResource(_resource);
                _resource = null;
                SetState(ResourceCollectorState.Free);
                _mover.Stop();
            }
        }

        public void Initialize(Collider baseCollider)
        {
            _baseCollider = baseCollider;
        }

        public void SendToResource(Transform resource)
        {
            _target = resource;
            _mover.Move(resource.position);
            SetState(ResourceCollectorState.FindResource);
        }

        public Resource TakeResource()
        {
            if (_resource == null)
                return null;

            SetState(ResourceCollectorState.Free);

            return _resource;
        }

        private void SetState(ResourceCollectorState state)
        {
            _state = state;
        }
    }
}
