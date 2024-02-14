using RTS.Resources;
using RTS.Units;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RTS.Builds.ResourcesBaseBuild
{
    public class BuildResourceBaseContractor
    {
        private ResourceCollectorsGarage _resourceCollectorsGarage;
        private ResourcesWarehouse _resourcesWarehouse;
        private int _resourceBaseCost;
        private List<PaidBuildFlag> _paidBuildFlags = new();
        private BuildFlag[] _totalBuildFlags;
        private ResourcesBaseFabric _resourcesBaseFabric;
        private Transform _baseParent;

        public BuildResourceBaseContractor(ResourceCollectorsGarage resourceCollectorsGarage, ResourcesWarehouse resourcesWarehouse,
            int resourceBaseCost, BuildFlag[] totalBuildFlags, ResourcesBaseFabric resourcesBaseFabric, Transform baseParent)
        {
            _resourceCollectorsGarage = resourceCollectorsGarage;
            _resourcesWarehouse = resourcesWarehouse;
            _resourceBaseCost = resourceBaseCost;
            _totalBuildFlags = totalBuildFlags;
            _resourcesBaseFabric = resourcesBaseFabric;
            _baseParent = baseParent;
        }

        public bool CanCreateResourceBase => _resourcesWarehouse.DetailsCount >= _resourceBaseCost;

        public void SendUnitToBuild(ResourcesCollectorUnit unit)
        {
            BuildFlag buildFlag = _totalBuildFlags.Where(x => x.State == BuildFlagState.WaitBuild).FirstOrDefault();

            if (buildFlag == null)
                throw new Exception("There are no flags for base construction");

            if (_resourceCollectorsGarage.IsOwnedUnit(unit) == false)
                throw new Exception("Unit does not belong to base");

            if (_resourcesWarehouse.TryRemoveDetails(_resourceBaseCost) == false)
                throw new Exception("There are not enough funds to build a new base");

            unit.BuildBase(buildFlag);

            Action<PaidBuildFlag, int> flagChangeStateAction = (paidBuildFlag, cost) =>
            {
                if (paidBuildFlag.BuildFlag.State == BuildFlagState.Complete)
                {
                    ResourcesBase resourceBase = BuildResourceBase(paidBuildFlag.BuildFlag.Position);
                    _resourceCollectorsGarage.Release(unit);
                    resourceBase.Take(unit);
                }
                else
                {
                    _resourcesWarehouse.AddDetails(cost);
                }

                _paidBuildFlags.Remove(paidBuildFlag);
            };

            _paidBuildFlags.Add(new PaidBuildFlag(buildFlag, _resourceBaseCost, flagChangeStateAction));
        }

        private ResourcesBase BuildResourceBase(Vector3 position)
        {
            ResourcesBase resourcesBase = _resourcesBaseFabric.Create(_baseParent, position);

            return resourcesBase;
        }

        private class PaidBuildFlag
        {
            private Action<PaidBuildFlag, int> _flagChangeStateAction;

            public PaidBuildFlag(BuildFlag buildFlag, int cost, Action<PaidBuildFlag, int> flagChangeStateAction)
            {
                BuildFlag = buildFlag;
                Cost = cost;
                _flagChangeStateAction = flagChangeStateAction;

                BuildFlag.StateChanged += OnFlagChangeState;
            }

            public BuildFlag BuildFlag { get; }

            public int Cost { get; }

            private void OnFlagChangeState(BuildFlagState flagState)
            {
                _flagChangeStateAction?.Invoke(this, Cost);
                BuildFlag.StateChanged -= OnFlagChangeState;
            }
        }
    }
}
