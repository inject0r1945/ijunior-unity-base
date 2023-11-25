using RTS.Resources;
using RTS.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace RTS.Builds
{
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(BoxCollider))]
    public class ResourceBase : MonoBehaviour, IResourceReceiver
    {
        [Header("Resource Collectors Units Settings")]
        [SerializeField] private int _maxResourceCollectors = 10;
        [SerializeField] private int _startResourceCollectorsCount = 3;
        [SerializeField] private ResourcesCollectorUnit _resourceCollectorPrefab;
        [SerializeField] private LayerMask _spawnerEnvironmentDetectionMask;

        [Header("Resources Detector Settings")]
        [SerializeField] private float _resourcesDetectorRadius = 50f;
        [SerializeField] private float _resourceDetectionDelay = 1f;
        [SerializeField] private LayerMask _resourcesMask;

        private Vector3 _size;
        private Transform _transform;
        private List<ResourcesCollectorUnit> _resourcesCollectors = new List<ResourcesCollectorUnit>();
        private float _resourcesDetectionTimer;
        private Collider _collider;
        private ResourcesWarehouse _resourcesWarehouse;

        [Inject]
        private void Construct(ResourcesWarehouse resourcesWarehouse)
        {
            _resourcesWarehouse = resourcesWarehouse;
        }

        private void Awake()
        {
            _transform = transform;
            _size = GetComponent<MeshFilter>().mesh.bounds.size;
            _collider = GetComponent<BoxCollider>();
            _resourcesDetectionTimer = _resourceDetectionDelay;
        }

        private void Start()
        {
            CreateResourceCollector(_startResourceCollectorsCount);
        }

        private void Update()
        {
            if (_resourcesDetectionTimer > _resourceDetectionDelay)
            {
                if (TryGetResources(out List<Resource> resources))
                {
                    SendFreeUnitToResources(resources);
                }

                _resourcesDetectionTimer = 0f;
            }

            UpdateTimers();
        }

        public void ReceiveResource(Resource resource)
        {
            _resourcesWarehouse.AddResource(resource);
            Destroy(resource.gameObject);
        }

        private void CreateResourceCollector(int count)
        {
            for (int i = 0; i < count; i++)
            {
                ResourcesCollectorUnit unit = Instantiate(_resourceCollectorPrefab, GetAroundEmptyPositionAround(), MathUtils.GetRandomRotation(Vector3.up), _transform);
                unit.Initialize(_collider);
                _resourcesCollectors.Add(unit);
            }
        }

        private Vector3 GetAroundEmptyPositionAround()
        {
            Vector3 position = Vector3.zero;
            float raycastRadius = 0.5f;
            float minDeltaPosition = 1f;
            float maxDeltaPosition = 7f;

            bool isEnd = false;

            while (isEnd == false)
            {
                position = MathUtils.GetRandomRectangleOutPosition(_size.x, _size.y, minDeltaPosition, maxDeltaPosition);

                Collider[] colliders = Physics.OverlapSphere(position, raycastRadius, _spawnerEnvironmentDetectionMask, QueryTriggerInteraction.Ignore);

                if (colliders.Length == 0)
                {
                    isEnd = true;
                }
            }

            return position;
        }

        private bool TryGetResources(out List<Resource> resources)
        {
            resources = null;
            Collider[] resourcesColliders = Physics.OverlapSphere(_transform.position, _resourcesDetectorRadius, _resourcesMask);

            if (resourcesColliders.Length == 0)
                return false;

            resources = new List<Resource>();

            foreach (Collider resourceCollider in resourcesColliders)
            {
                if (resourceCollider.TryGetComponent(out Resource resource) && !resource.IsReserved())
                {
                    resources.Add(resource);
                }
            }

            resources = resources.OrderBy(resource => Vector3.Distance(resource.transform.position, transform.position)).ToList();

            return true;
        }

        private void SendFreeUnitToResources(List<Resource> resources)
        {
            if (resources.Count == 0)
                return;

            bool isEnd = false;

            while (isEnd == false)
            {
                if (!TryGetFreeResourceCollector(out ResourcesCollectorUnit freeResourceCollector))
                {
                    isEnd = true;
                    continue;
                }

                Resource resource = GetClosestResourceToREsourceCollector(freeResourceCollector, resources);
                freeResourceCollector.SendToResource(resource.transform);
                resource.Reserve();
                resources.Remove(resource);

                if (resources.Count == 0)
                    isEnd = true;
            }
        }

        private bool TryGetFreeResourceCollector(out ResourcesCollectorUnit resourceCollector)
        {
            List<ResourcesCollectorUnit> freeREsourceCollector = _resourcesCollectors.Where(unit => !unit.IsBusy).ToList();
            resourceCollector = freeREsourceCollector.FirstOrDefault();

            return resourceCollector != null;
        }

        private Resource GetClosestResourceToREsourceCollector(ResourcesCollectorUnit resourceCollector, List<Resource> resources)
        {
            return resources.OrderBy(resource => Vector3.Distance(resource.transform.position, resourceCollector.transform.position)).FirstOrDefault();
        }

        private void UpdateTimers()
        {
            _resourcesDetectionTimer += Time.deltaTime;
        }
    }
}

