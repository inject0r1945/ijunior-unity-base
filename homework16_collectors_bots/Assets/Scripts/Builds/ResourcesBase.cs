using RTS.Detectors;
using RTS.Resources;
using RTS.Units;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace RTS.Builds
{
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(BoxCollider))]
    [RequireComponent(typeof(ResourcesDetector))]
    public class ResourcesBase : MonoBehaviour, IResourceReceiver
    {
        [Header("Resource Collectors Units Settings")]
        [SerializeField] private int _maxResourceCollectors = 10;
        [SerializeField] private int _startResourceCollectorsCount = 3;
        [SerializeField] private ResourcesCollectorUnit _resourceCollectorPrefab;
        [SerializeField] private LayerMask _spawnerEnvironmentDetectionMask;

        private Vector3 _size;
        private Transform _transform;
        private List<ResourcesCollectorUnit> _resourcesCollectors = new List<ResourcesCollectorUnit>();
        private Collider _collider;
        private ResourcesWarehouse _resourcesWarehouse;
        private ResourcesDetector _resourcesDetector;
        private Coroutine _collectResourceCoroutine;

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
            _resourcesDetector = GetComponent<ResourcesDetector>();
        }

        private void OnEnable()
        {
            _resourcesDetector.ResourceDetected += OnResourceDetected;
        }

        private void OnDisable()
        {
            _resourcesDetector.ResourceDetected -= OnResourceDetected;
        }

        private void Start()
        {
            CreateResourceCollector(_startResourceCollectorsCount);
        }

        public void ReceiveResource(Resource resource)
        {
            _resourcesWarehouse.AddResource(resource);
            Destroy(resource.gameObject);
        }

        private void OnResourceDetected()
        {
            if (_collectResourceCoroutine == null)
                _collectResourceCoroutine = StartCoroutine(CollectResourcesCoroutine());
        }

        private IEnumerator CollectResourcesCoroutine()
        {
            while (_resourcesDetector.ResourcesCount > 0)
            {
                SendFreeUnitToResources();

                yield return null;
            }

            _collectResourceCoroutine = null;
        }

        private void SendFreeUnitToResources()
        {
            if (_resourcesDetector.ResourcesCount == 0)
                return;

            bool isEnd = false;

            while (isEnd == false)
            {
                if (!TryGetFreeResourceCollector(out ResourcesCollectorUnit freeResourceCollector))
                {
                    isEnd = true;
                    continue;
                }

                Resource resource = _resourcesDetector.Puller.PullClosest(freeResourceCollector.transform.position);
                freeResourceCollector.SendToResource(resource.transform);

                if (_resourcesDetector.ResourcesCount == 0)
                    isEnd = true;
            }
        }

        private bool TryGetFreeResourceCollector(out ResourcesCollectorUnit resourceCollector)
        {
            List<ResourcesCollectorUnit> freeREsourceCollector = _resourcesCollectors.Where(unit => !unit.IsBusy).ToList();
            resourceCollector = freeREsourceCollector.FirstOrDefault();

            return resourceCollector != null;
        }

        private void CreateResourceCollector(int count)
        {
            for (int i = 0; i < count; i++)
            {
                ResourcesCollectorUnit unit = Instantiate(_resourceCollectorPrefab, GetAroundEmptyPosition(), MathUtils.GetRandomRotation(Vector3.up), _transform);
                unit.Initialize(_collider);
                _resourcesCollectors.Add(unit);
            }
        }

        private Vector3 GetAroundEmptyPosition()
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
    }
}

