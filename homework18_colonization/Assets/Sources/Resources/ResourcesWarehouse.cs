using Specifications;
using System;

namespace RTS.Resources
{
    public class ResourcesWarehouse
    {
        private int _detailsCount;
        private ResourceVisiter _addingDetailsVisiter;

        public ResourcesWarehouse()
        {
            _addingDetailsVisiter = new ResourceVisiter(resourceDelta => AddDetails(resourceDelta));
        }

        public event Action<int> DetailsChanged;

        public int DetailsCount => _detailsCount;

        public void AddResource(Resource resource)
        {
            _addingDetailsVisiter.Visit(resource);
        }

        public void AddDetails(int count)
        {
            IntValidator.GreatOrEqualZero(count);

            _detailsCount += count;
            DetailsChanged?.Invoke(_detailsCount);
        }

        public bool TryRemoveDetails(int count)
        {
            IntValidator.GreatOrEqualZero(count);

            if (_detailsCount < count)
                return false;

            _detailsCount -= count;
            DetailsChanged?.Invoke(_detailsCount);

            return true;
        }

        private class ResourceVisiter : IResourceVisiter
        {
            private const int DefaultResourceDelta = 1;
            private Action<int> _resourceAction;

            public ResourceVisiter(Action<int> resourceAction)
            {
                _resourceAction = resourceAction;
            }

            public void Visit(Resource resource) => _resourceAction(DefaultResourceDelta);

            public void Visit(Detail detail) => Visit((dynamic)detail);
        }
    }
}