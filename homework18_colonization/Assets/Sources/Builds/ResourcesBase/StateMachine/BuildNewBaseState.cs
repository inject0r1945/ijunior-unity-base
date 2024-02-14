using RTS.Core;
using RTS.Detectors;
using RTS.StateMachine;
using RTS.Units;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS.Builds.ResourcesBaseBuild.StateMachine
{
    public class BuildNewBaseState : BaseState
    {
        private Coroutine _buildCoroutine;

        public BuildNewBaseState(IStateSwitcher stateSwitcher, IEnumerable<BuildFlag> buildFlags, ResourcesDetector resourcesDetector,
            ResourceCollectorsGarage unitsGarage, BuildResourceBaseContractor buildResourceBaseContractor, ICoroutine coroutiner)
            : base(stateSwitcher, buildFlags, resourcesDetector, unitsGarage, buildResourceBaseContractor, coroutiner)
        {
        }

        public override void Exit()
        {
            base.Exit();
            StopBuildBehaviour();
        }

        public override void Update()
        {
            base.Update();

            StartBuildBehaviour();

            if (IsRequiredBuild == false)
                SwtchState<ResourceCollectState>();
        }

        private void StartBuildBehaviour()
        {
            if (_buildCoroutine == null)
                _buildCoroutine = StartCoroutine(BuildCoroutine());
        }

        private IEnumerator BuildCoroutine()
        {
            float delaySeconds = 0.5f;
            var delay = new WaitForSeconds(delaySeconds);

            while (IsRequiredBuild)
            {
                if (TryGetFreeUnit(out ResourcesCollectorUnit freeUnit) == false)
                {
                    yield return delay;

                    continue;
                }

                if (CanCreateResourceBase)
                    SendUnitToBuild(freeUnit);
                else
                    SendUnitToResource(freeUnit);

                yield return delay;
            }

            _buildCoroutine = null;
        }

        private void StopBuildBehaviour()
        {
            if (_buildCoroutine == null)
                return;

            StopCoroutine(_buildCoroutine);
            _buildCoroutine = null;
        }
    }
}
