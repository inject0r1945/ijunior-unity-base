using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace HealthVisualization
{
    public class SmoothHealthBar<T> : HealthIndicator<T>
        where T : struct, IFormattable
    {
        [SerializeField] private Slider _healthSlider;
        [SerializeField] private float _speed = 7;

        private Coroutine _healthbarCoroutine;

        private void OnDisable()
        {
            if (_healthbarCoroutine != null)
                StopCoroutine(_healthbarCoroutine);
        }

        protected override void UpdateHealthVisualization()
        {
            if (enabled == false)
                return;

            if (_healthbarCoroutine != null)
                StopCoroutine(_healthbarCoroutine);

            _healthbarCoroutine = StartCoroutine(HeatlhBarCoroutine());
        }

        private IEnumerator HeatlhBarCoroutine()
        {
            float currentHealthPercent = HealthMediator.Health.Percent;

            while (currentHealthPercent != _healthSlider.value)
            {
                _healthSlider.value = Mathf.MoveTowards(_healthSlider.value, currentHealthPercent, _speed * Time.deltaTime);

                yield return null;
            }
        }
    }
}
