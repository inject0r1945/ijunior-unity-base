using System;

namespace HealthVisualization
{
    public interface IHealth<T> where T : struct, IFormattable
    {
        public T MaxValue { get; }

        public T CurrentValue { get; }

        public float Percent { get; }

        public event Action<T> Changed;
    }
}