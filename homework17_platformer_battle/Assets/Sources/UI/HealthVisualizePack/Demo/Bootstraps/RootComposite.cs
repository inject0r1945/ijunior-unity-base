using HealthVisualization.Integer;
using Platformer.Attributes;
using Sirenix.OdinInspector;
using UnityEngine;

namespace DemoUIHealthBars
{
    public class RootComposite : MonoBehaviour
    {
        [SerializeField, Required] private Health _health;
        [SerializeField, Required] private IntTextHealthIndicator _textHealthIndicator;
        [SerializeField, Required] private IntHealthBar _healthBar;
        [SerializeField, Required] private IntSmoothHealthBar _smoothHealthBar;

        private IntHealthMediator _healthMediator;

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            _health.Initialize();
            _healthMediator = new IntHealthMediator(_health);

            _textHealthIndicator.Initialize(_healthMediator);
            _healthBar.Initialize(_healthMediator);
            _smoothHealthBar.Initialize(_healthMediator);
        }

        private void OnDestroy()
        {
            _healthMediator.Dispose();
        }
    }
}