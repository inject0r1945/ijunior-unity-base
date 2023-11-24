using UnityEngine;
using Zenject;
using RTS.Resources;

namespace RTS.Services
{
    public class ResourcesWarehouseService : MonoInstaller
    {
        [SerializeField] private ResourcesWarehouse _prefab;

        public override void InstallBindings()
        {
            ResourcesWarehouse resourcesWarehouse = Container.InstantiatePrefabForComponent<ResourcesWarehouse>(_prefab);
            Container.Bind<ResourcesWarehouse>().FromInstance(resourcesWarehouse).AsSingle().NonLazy();
        }
    }
}
