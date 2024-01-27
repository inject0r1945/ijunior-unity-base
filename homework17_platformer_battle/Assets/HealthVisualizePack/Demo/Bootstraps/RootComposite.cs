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

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            _health.Initialize();
            IntegerHealthMediator healthMediator = new IntegerHealthMediator(_health);
            _textHealthIndicator.Initialize(healthMediator);
            _healthBar.Initialize(healthMediator);
            _smoothHealthBar.Initialize(healthMediator);
        }
    }
}