using RTS.Core;
using RTS.Detectors;
using RTS.StateMachine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RTS.Builds.ResourcesBaseBuild.StateMachine
{
    public class ResourcesBaseStateMachine : IStateSwitcher, IDisposable
    {
        private List<BaseState> _states;
        private IState _currentState;

        public ResourcesBaseStateMachine(ResourcesDetector detector, IEnumerable<BuildFlag> buildFlags,
            ResourceCollectorsGarage unitsGarage, BuildResourceBaseContractor buildResourceBaseContractors, ICoroutine coroutiner)
        {
            _states = new List<BaseState>()
            {
                new ResourceCollectState(this, buildFlags, detector, unitsGarage, buildResourceBaseContractors, coroutiner),
                new BuildNewBaseState(this, buildFlags, detector, unitsGarage, buildResourceBaseContractors, coroutiner),
            };

            SwitchState<ResourceCollectState>();
        }

        public void Dispose()
        {
            foreach (BaseState state in _states)
                state.Dispose();
        }

        public void SwitchState<T>() where T : IState
        {
            IState nextState = _states.FirstOrDefault(state => state is T);

            if (nextState == null)
                throw new Exception($"Unknown state type {nameof(T)}");

            SwitchState(nextState);
        }

        public void Update()
        {
            _currentState.Update();
        }

        private void SwitchState(IState nextState)
        {
            if (_currentState != null)
                _currentState.Exit();

            _currentState = nextState;
            _currentState.Enter();
        }
    }
}
