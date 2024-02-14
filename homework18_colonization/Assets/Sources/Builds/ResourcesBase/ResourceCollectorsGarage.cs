using RTS.Resources;
using RTS.Units;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RTS.Builds.ResourcesBaseBuild
{
    public class ResourceCollectorsGarage
    {
        private ResourcesWarehouse _resourcesWarehouse;
        private int _resourceCollectorCost;
        private ResourceCollectorsUnitsFabric _unitFabric;
        private Transform _unitParent;
        private List<ResourcesCollectorUnit> _units = new List<ResourcesCollectorUnit>();

        public ResourceCollectorsGarage(ResourcesWarehouse resourcesWarehouse, int resourceCollectorCost,
            ResourceCollectorsUnitsFabric unitFabric, Transform unitParent)
        {
            _resourcesWarehouse = resourcesWarehouse;
            _resourceCollectorCost = resourceCollectorCost;
            _unitFabric = unitFabric;
            _unitParent = unitParent;
        }

        public bool CanCreateResourceCollector => _resourcesWarehouse.DetailsCount >= _resourceCollectorCost;

        public bool TryCreateResourceCollector()
        {
            if (CanCreateResourceCollector == false)
                return false;

            _resourcesWarehouse.TryRemoveDetails(_resourceCollectorCost);

            ResourcesCollectorUnit unit = _unitFabric.Create(_unitParent);
            _units.Add(unit);

            return true;
        }

        public bool TryGetFreeUnit(out ResourcesCollectorUnit resourceCollector)
        {
            resourceCollector = _units.Where(unit => !unit.IsBusy).FirstOrDefault();

            return resourceCollector != null;
        }

        public int UnitsCount(ResourceCollectorState state)
        {
            return _units.Where(x => x.State == state).Count();
        }

        public bool IsOwnedUnit(ResourcesCollectorUnit unit)
        {
            return _units.Contains(unit);
        }

        public void Take(ResourcesCollectorUnit unit)
        {
            if (_units.Contains(unit))
                throw new System.Exception("Garage already contains this unit");

            _units.Add(unit);
        }

        public void Release(ResourcesCollectorUnit unit)
        {
            if (_units.Contains(unit) == false)
                throw new System.Exception("Unit does not belong to this garage");

            _units.Remove(unit);
        }
    }
}
