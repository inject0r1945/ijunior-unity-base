using RTS.Builds.ResourcesBaseBuild.UI;
using RTS.Core;
using RTS.Management;
using RTS.Units;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace RTS.Builds.ResourcesBaseBuild.Bootstraps
{
    public class ResourceBaseBootstrap : MonoBehaviour
    {
        [SerializeField, Required] private ResourcesBase _resourcesBase;
        [SerializeField, Required] private ResourceBaseMenu _resourceBaseView;
        private ResourcesBaseMediator _resourcesBaseMediator;

        private void OnDestroy()
        {
            _resourcesBaseMediator.Dispose();
        }

        [Inject]
        public void Initialize(UnitsConfigurations unitsConfigurations, ScreenObjectMover screenObjectMover, 
            ResourcesBaseFabric resourcesBaseFabric, ICoroutine coroutiner)
        {
            _resourcesBase.Initialize(unitsConfigurations, screenObjectMover, resourcesBaseFabric, coroutiner);

            _resourcesBaseMediator = new ResourcesBaseMediator(_resourcesBase, _resourceBaseView);
            _resourceBaseView.Initialize(_resourcesBaseMediator);
        }
    }
}