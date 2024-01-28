using System;
using UnityEngine;
using UnityEngine.UI;

namespace HealthVisualization
{
    public class HealthBar<T> : HealthIndicator<T>
        where T : struct, IFormattable
    {
        [SerializeField] private Slider _healthSlider;

        protected override void UpdateHealthVisualization()
        {
            _healthSlider.value = HealthMediator.Health.Percent;
        }
    }
}
