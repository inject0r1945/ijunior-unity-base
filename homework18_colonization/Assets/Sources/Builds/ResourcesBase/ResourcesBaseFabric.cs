using RTS.AI;
using UnityEngine;
using Zenject;

namespace RTS.Builds.ResourcesBaseBuild
{
    public class ResourcesBaseFabric
    {
        private DiContainer _diContainer;
        private GridBaker _gridBaker;
        private ResourcesBase _resourcesBasePrefab;
        private Transform _parent;

        public ResourcesBaseFabric(DiContainer diContainer, ResourcesBase resourcesBasePrefab, GridBaker gridBaker)
        {
            _diContainer = diContainer;
            _gridBaker = gridBaker;
            _resourcesBasePrefab = resourcesBasePrefab;
        }

        public ResourcesBase Create(Transform parent, Vector3 position)
        {
            ResourcesBase resourceBase = _diContainer
                .InstantiatePrefabForComponent<ResourcesBase>(_resourcesBasePrefab, position, Quaternion.identity, parent);

            _gridBaker.Bake();

            return resourceBase;
        }
    }
}
