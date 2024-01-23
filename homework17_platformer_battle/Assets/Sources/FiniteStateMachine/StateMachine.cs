using FiniteStateMachine.States;
using FiniteStateMachine.Transitions;
using System;
using System.Collections.Generic;

namespace FiniteStateMachine
{
    public class StateMachine
    {
        private StateNode _currentStateNode;
        private Dictionary<Type, StateNode> _stateNodes = new();
        private HashSet<ITransition> _interruptingTransitions = new();

        public void Update()
        { 
            if (TryGetTransition(out ITransition transition))
                ChangeState(transition.To);

            _currentStateNode.Update();
        }

        public void FixedUpdate()
        {
            _currentStateNode.FixedUpdate();
        }

        public bool TrySetState(IState state)
        {
            StateNode stateNode = GetStateNode(state);

            if (stateNode == null)
                return false;

            if (_currentStateNode != null)
                _currentStateNode.ExitState();

            _currentStateNode = stateNode;
            _currentStateNode?.EnterState();

            return true;
        }

        public void AddTransition(IState fromState, IState toState, IPredicate condition)
        {
            if (IsExistState(fromState) == false)
                AddStateNode(fromState);

            if (IsExistState(toState) == false)
                AddStateNode(toState);

            StateNode fromStateNode = GetStateNode(fromState);

            fromStateNode.AddTransition(toState, condition);
        }

        public void AddAnyTransition(IState toState, IPredicate condition)
        {
            if (IsExistState(toState) == false)
                AddStateNode(toState);

            ITransition transition = new Transition(toState, condition);
            _interruptingTransitions.Add(transition);
        }

        private bool TryGetTransition(out ITransition transition)
        {
            transition = null;

            foreach (ITransition interruptingTransition in _interruptingTransitions)
            {
                if (interruptingTransition.IsFulfilledCondition())
                {
                    transition = interruptingTransition;

                    return true;
                } 
            }

            if (_currentStateNode.TryGetTransition(out ITransition nodeTransition))
            {
                transition = nodeTransition;

                return true;
            }

            return false;
        }

        private void ChangeState(IState state)
        {
            if (state == _currentStateNode.State)
                return;

            StateNode nextStateNode = _stateNodes[state.GetType()];

            _currentStateNode.ExitState();
            nextStateNode.EnterState();

            _currentStateNode = nextStateNode;
        }

        private bool IsExistState(IState state)
        {
            return GetStateNode(state) != null;
        }

        private StateNode GetStateNode(IState state)
        {
            return _stateNodes.GetValueOrDefault(state.GetType());
        }

        private void AddStateNode(IState state)
        {
            StateNode node = new StateNode(state);
            _stateNodes.Add(state.GetType(), node);
        }
    }
}