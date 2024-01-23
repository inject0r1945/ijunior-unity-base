using System;

namespace FiniteStateMachine.States
{
    public interface IPredicate
    {
        public bool Evaluate();
    }
}
