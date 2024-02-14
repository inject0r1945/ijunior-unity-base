using RTS.Core;
using RTS.Detectors;
using RTS.Resources;
using RTS.StateMachine;
using RTS.Units;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RTS.Builds.ResourcesBaseBuild.StateMachine
{
    public abstract class BaseState : IState, IDisposable
    {
        private IStateSwitcher _stateSwitcher;
        protected IEnumerable<BuildFlag> _buildFlags;
        private ResourcesDetector _resourcesDetector;
        private ResourceCollectorsGarage _unitsGarage;
        private BuildResourceBaseContractor _buildResourceBaseContractor;
        private ICoroutine _coroutiner;

        public BaseState(IStateSwitcher stateSwitcher, IEnumerable<BuildFlag> buildFlags, ResourcesDetector resourcesDetector,
            ResourceCollectorsGarage unitsGarage, BuildResourceBaseContractor buildResourceBaseContractor, ICoroutine coroutiner)
        {
            _stateSwitcher = stateSwitcher;
            _buildFlags = buildFlags;
            _resourcesDetector = resourcesDetector;
            _unitsGarage = unitsGarage;
            _buildResourceBaseContractor = buildResourceBaseContractor;
            _coroutiner = coroutiner;

            _resourcesDetector.ResourceDetected += OnResourceDetect;

            foreach (BuildFlag buildFlag in _buildFlags)
                buildFlag.StateChanged += OnFlagStateChange;
        }

        protected int ResourcesCount => _resourcesDetector.ResourcesCount;

        protected bool IsWaitBuild { get; private set; }

        protected int WaitBuildFlagsCount => _buildFlags.Where(x => x.State == BuildFlagState.WaitBuild).Count();

        protected bool CanCreateResourceBase => _buildResourceBaseContractor.CanCreateResourceBase;

        protected bool IsRequiredBuild => WaitBuildFlagsCount > UnitsCount(ResourceCollectorState.BuildResourceBase);

        public void Dispose()
        {
            _resourcesDetector.ResourceDetected -= OnResourceDetect;

            foreach (BuildFlag buildFlag in _buildFlags)
                buildFlag.StateChanged -= OnFlagStateChange;
        }

        public virtual void Enter()
        {
        }

        public virtual void Exit()
        {
        }

        public virtual void Update()
        {
        }

        protected virtual void OnResourceDetect()
        {
        }

        protected void SendUnitToResource(ResourcesCollectorUnit freeResourceCollector)
        {
            if (_resourcesDetector.ResourcesCount == 0)
                return;

            Resource resource = _resourcesDetector.Puller.PullClosest(freeResourceCollector.transform.position);
            freeResourceCollector.SendToResource(resource.transform);
        }

        protected bool TryGetFreeUnit(out ResourcesCollectorUnit unit)
        {
            return _unitsGarage.TryGetFreeUnit(out unit);
        }

        protected int UnitsCount(ResourceCollectorState state)
        {
            return _unitsGarage.UnitsCount(state);
        }

        protected void SendUnitToBuild(ResourcesCollectorUnit unit)
        {
            _buildResourceBaseContractor.SendUnitToBuild(unit);
        }

        protected Coroutine StartCoroutine(IEnumerator coroutine)
        {
            return _coroutiner.StartCoroutine(coroutine);
        }

        protected void StopCoroutine(Coroutine coroutine)
        {
            _coroutiner.StopCoroutine(coroutine);
        }

        protected void SwtchState<T>() where T: IState
        {
            _stateSwitcher.SwitchState<T>();
        }

        private void OnFlagStateChange(BuildFlagState flagState)
        {
            IsWaitBuild = WaitBuildFlagsCount > 0;
        }
    }
}
