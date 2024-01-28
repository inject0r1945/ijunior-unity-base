using System;
using UnityEngine;
using TMPro;

namespace HealthVisualization
{
    public class TextHealthIndicator<T> : HealthIndicator<T>
        where T : struct, IFormattable
    {
        [SerializeField] private TMP_Text _currentValueText;
        [SerializeField] private TMP_Text _maxValueText;

        protected override void UpdateHealthVisualization()
        {
            _currentValueText.text = HealthMediator.Health.CurrentValue.ToString();
            _maxValueText.text = HealthMediator.Health.MaxValue.ToString();
        }
    }
}
