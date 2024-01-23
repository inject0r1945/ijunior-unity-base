using Platformer.Core;
using Platformer.Playing;
using UnityEngine;

namespace Platformer.Enemies
{
    public class PlayerDetector
    {
        private const int MaxObjectDetectCount = 30;
        private IDetector _detector;
        private Vector2 _detectorBox;
        private Collider2D[] _detectedColliders = new Collider2D[MaxObjectDetectCount];
        private int _detectedCollidersCount;
        private Player _player;

        public PlayerDetector(IDetector detector)
        {
            _detector = detector;
            _detectorBox = new Vector2(detector.DetectionRadius, detector.DetectionRadius);
        }

        public bool IsDetected { get; private set; }

        public ITransform DetectedPlayer => _player;

        public void Update()
        {
            _detectedCollidersCount = Physics2D.OverlapBoxNonAlloc(_detector.Transform.position, _detectorBox, 0, _detectedColliders);

            if (_detectedCollidersCount == 0)
            {
                Reset();

                return;
            }

            for (int x = 0; x < _detectedCollidersCount; x++)
            {
                if (_detectedColliders[x].gameObject.TryGetComponent(out Player player))
                {
                    IsDetected = true;
                    _player = player;

                    return;
                }
            }

            Reset();
        }

        public float GetDistanceToPlayer(Transform point)
        {
            float distanceToPlayer = Mathf.Infinity;

            if (DetectedPlayer != null)
                distanceToPlayer = Vector3.Distance(point.position, DetectedPlayer.Transform.position);

            return distanceToPlayer;
        }

        private void Reset()
        {
            IsDetected = false;
            _player = null;
        }
    }
}
