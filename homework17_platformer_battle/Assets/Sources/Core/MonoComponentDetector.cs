using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Core
{
    public class MonoComponentDetector<T> where T : MonoBehaviour
    {
        private IDetector _detector;
        private List<T> _detectedComponents = new();
        private Collider2D[] _detectedColliders;

        public MonoComponentDetector(IDetector detector)
        {
            _detector = detector;
        }

        public IReadOnlyList<T> DetectedComponents => _detectedComponents;

        public void Update()
        {
            Detect();
        }

        private void Detect()
        {
            _detectedColliders = Physics2D.OverlapCircleAll(_detector.Transform.position, _detector.DetectionRadius, _detector.DetectionLayers);
            _detectedComponents.Clear();

            foreach (Collider2D collider in _detectedColliders)
            {
                if (collider.TryGetComponent(out T component) && _detectedComponents.Contains(component) == false)
                {
                    _detectedComponents.Add(component);
                }
            }
        }
    }
}