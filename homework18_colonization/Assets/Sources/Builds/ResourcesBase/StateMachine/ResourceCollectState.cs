using RTS.Core;
using RTS.Detectors;
using RTS.StateMachine;
using RTS.Units;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS.Builds.ResourcesBaseBuild.StateMachine
{
    public class ResourceCollectState : BaseState
    {
        private Coroutine _collectResourceCoroutine;

        public ResourceCollectState(IStateSwitcher stateSwitcher, IEnumerable<BuildFlag> buildFlags, ResourcesDetector resourcesDetector,
            ResourceCollectorsGarage unitsGarage, BuildResourceBaseContractor buildResourceBaseContractor, ICoroutine coroutiner) 
            : base(stateSwitcher, buildFlags, resourcesDetector, unitsGarage, buildResourceBaseContractor, coroutiner)
        {
        }

        public override void Exit()
        {
            base.Exit();
            StopCollectResourcesBehaviour();
        }

        public override void Update()
        {
            base.Update();

            StartCollectResourcesBehaviour();

            if (IsRequiredBuild)
                SwtchState<BuildNewBaseState>();
        }

        private void StartCollectResourcesBehaviour()
        {
            if (ResourcesCount == 0)
                return;

            if (_collectResourceCoroutine == null)
                _collectResourceCoroutine = StartCoroutine(CollectResourcesCoroutine());
        }

        private IEnumerator CollectResourcesCoroutine()
        {
            float delaySeconds = 0.5f;
            var delay = new WaitForSeconds(delaySeconds);

            while (ResourcesCount > 0)
            {
                if (TryGetFreeUnit(out ResourcesCollectorUnit freeResourceCollector) == false)
                {
                    yield return delay;

                    continue;
                }

                SendUnitToResource(freeResourceCollector);

                yield return delay;
            }

            _collectResourceCoroutine = null;
        }

        private void StopCollectResourcesBehaviour()
        {
            if (_collectResourceCoroutine == null)
                return;

            StopCoroutine(_collectResourceCoroutine);
            _collectResourceCoroutine = null;
        }
    }
}
