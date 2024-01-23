using FiniteStateMachine.States;

namespace FiniteStateMachine.Transitions
{
    public interface ITransition
    {
        public IState To { get; }

        public bool IsFulfilledCondition();
    }
}
