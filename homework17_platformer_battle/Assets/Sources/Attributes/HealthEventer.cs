using System;
using UnityEngine;
using UnityEngine.Events;

namespace Platformer.Attributes
{
    [RequireComponent(typeof(Health))]
    public class HealthEventer : MonoBehaviour
    {
        private bool _isInitialized;

        private Health _health;

        public UnityEvent Heal;

        public UnityEvent Died;

        public UnityEvent Damaged;

        public UnityEvent<int> Changed;

        private void Awake()
        {
            ValidateInitialization();
        }

        private void OnEnable()
        {
            _health.Died += OnDie;
            _health.Damaged += OnDamage;
            _health.Changed += OnChanged;
            _health.Healed += OnHeal;
        }

        private void OnDisable()
        {
            _health.Died -= OnDie;
            _health.Damaged -= OnDamage;
            _health.Changed -= OnChanged;
            _health.Healed -= OnHeal;
        }

        public void Initialize()
        {
            _health = GetComponent<Health>();
            _isInitialized = true;
        }

        private void ValidateInitialization()
        {
            if (_isInitialized == false)
                throw new Exception($"{nameof(HealthEventer)} is not initialized");
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
