using FiniteStateMachine.States;
using FiniteStateMachine.Transitions;
using System.Collections.Generic;

namespace FiniteStateMachine
{
    public class StateNode
    {
        public StateNode(IState state)
        {
            State = state;
            Transitions = new HashSet<ITransition>();
        }

        public IState State { get; }

        public HashSet<ITransition> Transitions { get; private set; }

        public void Update()
        {
            State?.Update();
        }

        public void FixedUpdate()
        {
            State?.FixedUpdate();
        }

        public void AddTransition(IState to, IPredicate condition)
        {
            Transitions.Add(new Transition(to, condition));
        }

        public void EnterState()
        {
            State?.Enter();
        }

        public void ExitState()
        {
            State?.Exit();
        }

        public bool TryGetTransition(out ITransition transition)
        {
            transition = null;

            foreach (ITransition nodeTransition in Transitions)
            {
                if (nodeTransition.IsFulfilledCondition())
                {
                    transition = nodeTransition;
                    return true;
                }
            }

            return false;
        }
    }
}
