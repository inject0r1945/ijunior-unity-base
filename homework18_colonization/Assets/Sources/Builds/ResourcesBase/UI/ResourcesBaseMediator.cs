using System;

namespace RTS.Builds.ResourcesBaseBuild.UI
{
    public class ResourcesBaseMediator : IDisposable
    {
        private ResourcesBase _resourcesBase;
        private ResourceBaseMenu _resourceBaseMenu;

        public ResourcesBaseMediator(ResourcesBase resourcesBase, ResourceBaseMenu resourceBaseMenu)
        {
            _resourcesBase = resourcesBase;
            _resourceBaseMenu = resourceBaseMenu;

            OnDetailsChanged(_resourcesBase.DetailsCount);

            _resourcesBase.DetailsChanged += OnDetailsChanged;
        }

        public void Dispose()
        {
            _resourcesBase.DetailsChanged -= OnDetailsChanged;
        }

        public void CreateResourceCollector()
        {
            _resourcesBase.TryCreateResourceCollector();
        }

        public void StartPutFlagBehaviour()
        {
            _resourcesBase.TryStartSetFlagBehaviour();
        }

        private void OnDetailsChanged(int detailsCount)
        {
            SetViewDetailsCount(detailsCount);

            if (_resourcesBase.CanCreateResourceCollector)
                _resourceBaseMenu.EnableCreateResourceCollectorButton();
            else
                _resourceBaseMenu.DisableCreateResourceCollectorButton();
        }

        private void SetViewDetailsCount(int detailsCount)
        {
            _resourceBaseMenu.SetDetailsCount(detailsCount);
        }
    }
}
