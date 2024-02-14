using UnityEngine;

namespace RTS.Units
{
    public class ResourceCollectorsUnitsFabric
    {
        private UnitsConfigurations _unitsConfigurations;
        private Vector3 _nonSpawnRectangleSize;
        private Collider _unitBaseCollider;
        private LayerMask _interferingObjectsMask;

        public ResourceCollectorsUnitsFabric(UnitsConfigurations unitsConfigurations,
            Vector3 unitBaseSize, Collider unitBaseCollider, LayerMask interferingObjectsMask)
        {
            _unitsConfigurations = unitsConfigurations;
            _nonSpawnRectangleSize = unitBaseSize;
            _unitBaseCollider = unitBaseCollider;
            _interferingObjectsMask = interferingObjectsMask;
        }

        public ResourcesCollectorUnit Create(Transform parent)
        {
            ResourcesCollectorUnit unit = Object.Instantiate(_unitsConfigurations.ResourcesCollectorUnitPrefab, 
                GetAroundEmptyPosition(parent), MathUtils.GetRandomRotation(Vector3.up), parent);

            unit.Initialize(_unitBaseCollider);

            return unit;
        }

        private Vector3 GetAroundEmptyPosition(Transform parent)
        {
            Vector3 position = Vector3.zero;
            float raycastRadius = 0.5f;
            float minDeltaPosition = 1f;
            float maxDeltaPosition = 7f;

            bool isEnd = false;

            while (isEnd == false)
            {
                position = MathUtils.GetRandomRectangleOutPosition(_nonSpawnRectangleSize.x, _nonSpawnRectangleSize.y, minDeltaPosition, maxDeltaPosition);
                position += parent.position;

                Collider[] colliders = Physics.OverlapSphere(position, raycastRadius, _interferingObjectsMask, QueryTriggerInteraction.Ignore);

                if (colliders.Length == 0)
                    isEnd = true;
            }

            return position;
        }
    }
}
