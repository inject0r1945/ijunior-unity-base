using System;

namespace HealthVisualization
{
    public class HealthMediator<T> : IDisposable
        where T : struct, IFormattable
    {
        private IHealth<T> _health;

        public HealthMediator(IHealth<T> health)
        {
            _health = health;
            _health.Changed += OnHealthChanged;
        }

        public event Action<T> Changed;

        public IHealth<T> Health => _health;

        public void Dispose()
        {
            _health.Changed -= OnHealthChanged;
        }

        private void OnHealthChanged(T value)
        {
            Changed?.Invoke(value);
        }
    }
}
