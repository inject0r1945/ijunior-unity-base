using MonoUtils;
using System;

namespace HealthVisualization
{
    public abstract class HealthIndicator<T> : InitializedMonobehaviour
        where T : struct, IFormattable
    {
        protected HealthMediator<T> HealthMediator { get; private set; }

        private void OnEnable()
        {
            HealthMediator.Changed += OnHealthChange;
        }

        private void OnDisable()
        {
            HealthMediator.Changed -= OnHealthChange;
        }

        public void Initialize(HealthMediator<T> healthMediator)
        {
            HealthMediator = healthMediator;
            IsInitialized = true;
        }

        protected abstract void UpdateHealthVisualization();

        private void OnHealthChange(T value)
        {
            UpdateHealthVisualization();
        }
    }
}