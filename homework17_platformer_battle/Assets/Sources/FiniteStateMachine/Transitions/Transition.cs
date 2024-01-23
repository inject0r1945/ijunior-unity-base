using FiniteStateMachine.States;

namespace FiniteStateMachine.Transitions
{
    public class Transition : ITransition
    {
        public Transition(IState to, IPredicate predicate)
        {
            To = to;
            Condition = predicate;
        }

        public IState To { get; }

        protected IPredicate Condition { get; }

        public bool IsFulfilledCondition()
        {
            return Condition.Evaluate();
        }
    }
}