using UnityEngine;
using Zenject;
using RTS.Resources;

namespace RTS.Services
{
    public class ResourcesStatisticsService : MonoInstaller
    {
        [SerializeField] private ResourcesStatistics _prefab;

        public override void InstallBindings()
        {
            ResourcesStatistics resourcesStatistics = Container.InstantiatePrefabForComponent<ResourcesStatistics>(_prefab);
            Container.Bind<ResourcesStatistics>().FromInstance(resourcesStatistics).AsSingle().NonLazy();
        }
    }
}
