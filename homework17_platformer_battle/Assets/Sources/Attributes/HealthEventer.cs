using MonoUtils;
using UnityEngine;
using UnityEngine.Events;

namespace Platformer.Attributes
{
    [RequireComponent(typeof(Health))]
    public class HealthEventer : InitializedMonobehaviour
    {
        private Health _health;

        public UnityEvent Heal;

        public UnityEvent Died;

        public UnityEvent Damaged;

        public UnityEvent<int> Changed;

        private void OnEnable()
        {
            _health.Died += OnDie;
            _health.Damaged += OnDamage;
            _health.Changed += OnChanged;
            _health.Healed += OnHeal;
        }

        private void OnDisable()
        {
            _health.Died += OnDie;
            _health.Damaged += OnDamage;
            _health.Changed += OnChanged;
            _health.Healed += OnHeal;
        }

        public void Initialize()
        {
            _health = GetComponent<Health>();
            IsInitialized = true;
        }

        private void OnHeal()
        {
            Heal?.Invoke();
        }

        private void OnChanged(int value)
        {
            Changed?.Invoke(value);
        }

        private void OnDamage()
        {
            Damaged?.Invoke();
        }

        private void OnDie()
        {
            Died?.Invoke();
        }
    }
}
