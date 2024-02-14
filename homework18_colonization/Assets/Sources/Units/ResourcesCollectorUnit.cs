using UnityEngine;
using RTS.Resources;
using RTS.Control;
using RTS.Builds;

namespace RTS.Units
{
    [RequireComponent(typeof(Mover))]
    public class ResourcesCollectorUnit : Unit
    {
        [SerializeField] private Transform _resourcePoint;

        private Mover _mover;
        private Transform _transform;
        private Collider _baseCollider;
        private Resource _resource;
        private ResourceCollectorState _state;
        private Transform _target;
        private BuildFlag _buildFlag;

        public bool IsBusy => _state != ResourceCollectorState.Free;

        public ResourceCollectorState State => _state;

        public void Initialize(Collider baseCollider)
        {
            _baseCollider = baseCollider;
            _transform = transform;

            _mover = GetComponent<Mover>();
            _mover.Initialize();

            SetState(ResourceCollectorState.Free);
        }

        private void OnTriggerStay(Collider triggeredCollider)
        {
            OnTriggerEnter(triggeredCollider);
        }

        private void OnTriggerEnter(Collider triggeredCollider)
        {
            if (State == ResourceCollectorState.BuildResourceBase)
            {
                BuildFlag buildFlag = triggeredCollider.GetComponentInParent<BuildFlag>();

                if (buildFlag != null)
                {
                    buildFlag.Complete();
                    _state = ResourceCollectorState.Free;

                    return;
                }
            }

            if (TryCollectResource(triggeredCollider))
                return;

            StartDeleiveResourceBehaviour(triggeredCollider);
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

        public void BuildBase(BuildFlag buildFlag)
        {
            _buildFlag = buildFlag;
            SetState(ResourceCollectorState.BuildResourceBase);
            _mover.Move(_buildFlag.Position);

            _buildFlag.StateChanged += OnFlagStateChange;
        }

        private bool TryCollectResource(Collider triggeredCollider)
        {
            if (_state == ResourceCollectorState.Free)
                return false;

            if (_resource == null && _state == ResourceCollectorState.FindResource && triggeredCollider.TryGetComponent(out Resource resource)
                && _target.position == resource.transform.position)
            {
                resource.Take(_resourcePoint);
                _mover.Move(_baseCollider.ClosestPoint(_transform.position));
                _resource = resource;

                return true;
            }

            return false;
        }

        private void StartDeleiveResourceBehaviour(Collider triggeredCollider)
        {
            if (_resource == null)
                return;

            IResourceReceiver resourceReceiver = triggeredCollider.transform.GetComponentInParent<IResourceReceiver>();

            if (resourceReceiver == null)
                return;

            if (resourceReceiver.TryReceiveResource(this, _resource) == false)
                return;

            _resource = null;
            SetState(ResourceCollectorState.Free);
            _mover.Stop();
        }

        private void SetState(ResourceCollectorState state)
        {
            _state = state;
        }

        private void OnFlagStateChange(BuildFlagState obj)
        {
            _buildFlag.StateChanged -= OnFlagStateChange;
            _buildFlag = null;

            SetState(ResourceCollectorState.Free);
        }
    }
}
