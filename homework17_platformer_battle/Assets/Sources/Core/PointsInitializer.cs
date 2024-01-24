using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Platformer.Core
{
    public class PointsInitializer : MonoBehaviour
    {
        [SerializeField, Required] private Transform _pointsParrent;

        private float _pointVisualRadius = 0.2f; 
        private List<Transform> _patrolPoints;
        private bool _isInitialized;

        public IReadOnlyList<Transform> Points => _patrolPoints;

        private void Awake()
        {
            ValidateInitialization();
        }

        private void OnDrawGizmos()
        {
            if (Points == null)
                return;

            Gizmos.color = Color.green;

            foreach (Transform point in Points)
                Gizmos.DrawSphere(point.position, _pointVisualRadius);
        }

        public void Initialize()
        {
            _patrolPoints = _pointsParrent.GetComponentsInChildren<Transform>()
                .Where(x => x != _pointsParrent)
                .ToList();

            _isInitialized = true;
        }

        private void ValidateInitialization()
        {
            if (_isInitialized == false)
                throw new System.Exception($"{nameof(PointsInitializer)} is not initialized");
        }
    }
}