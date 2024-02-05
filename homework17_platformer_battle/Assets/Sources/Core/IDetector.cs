using UnityEngine;

namespace Platformer.Core
{
    public interface IDetector
    {
        public float DetectionRadius { get; }

        public Transform Transform { get; }

        public LayerMask DetectionLayers { get; }
    }
}
