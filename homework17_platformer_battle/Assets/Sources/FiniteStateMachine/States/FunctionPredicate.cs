using System;

namespace FiniteStateMachine.States
{
    public class FunctionPredicate : IPredicate
    {
        private readonly Func<bool> _predicateFunction;

        public FunctionPredicate(Func<bool> predicateFunction)
        {
            _predicateFunction = predicateFunction;
        }

        public bool Evaluate() => _predicateFunction.Invoke();
    }
}
