using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTS.Resources;
using RTS.Core;
using System.Linq;
using System;

namespace RTS.Detectors
{
    public class ResourcesDetector : MonoBehaviour
    {
        [SerializeField] private float _resourcesDetectorRadius = 50f;
        [SerializeField] private float _resourceDetectionDelay = 1f;
        [SerializeField] private LayerMask _resourcesMask;

        private QueueBehaviour<Resource> _resourcesQueue = new QueueBehaviour<Resource>(hasReAdding: false);
        private Transform _transform;

        public IClosestPuller<Resource> Puller => _resourcesQueue;

        public int ResourcesCount => _resourcesQueue.Size;

        public event Action ResourceDetected;

        private void Awake()
        {
            _transform = transform;
        }

        private void OnEnable()
        {
            _resourcesQueue.QueueIncreased += OnResourceAdded;
        }

        private void OnDisable()
        {
            _resourcesQueue.QueueIncreased -= OnResourceAdded;
        }

        private void Start()
        {
            StartCoroutine(ResourceDetectionCoroutine());
        }

        private void OnResourceAdded()
        {
            ResourceDetected?.Invoke();
        }

        private IEnumerator ResourceDetectionCoroutine()
        {
            bool isEnd = false;
            var delay = new WaitForSeconds(_resourceDetectionDelay);

            while (isEnd == false)
            {
                if (TryGetResources(out List<Resource> resources))
                {
                    _resourcesQueue.Push(resources);
                }

                yield return delay;
            }
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
                if (resourceCollider.TryGetComponent(out Resource resource))
                {
                    resources.Add(resource);
                }
            }

            resources = resources.OrderBy(resource => Vector3.Distance(resource.transform.position, transform.position)).ToList();

            return true;
        }
    }
}