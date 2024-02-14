using RTS.Builds.ResourcesBaseBuild;
using RTS.Management;
using RTS.Units;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace RTS.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField, Required, AssetsOnly] private UnitsConfigurations _unitsConfigurations;
        [SerializeField, Required, AssetsOnly] private ResourcesBase _resourcesBasePrefab;

        public override void InstallBindings()
        {
            BindConfigurations();
            BindManagements();
            InstallFabrics();
        }

        private void BindConfigurations()
        {
            Container.BindInstance(_unitsConfigurations);
        }

        private void BindManagements()
        {
            Container.BindInterfacesAndSelfTo<ScreenObjectMover>().AsSingle();
        }

        private void InstallFabrics()
        {
            Container.Bind<ResourcesBaseFabric>().AsSingle().WithArguments(_resourcesBasePrefab);
        }
    }
}
