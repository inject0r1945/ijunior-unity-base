using HealthVisualization;
using Sirenix.OdinInspector;
using Specifications;
using System;
using UnityEngine;

namespace Platformer.Attributes
{
    public class Health : MonoBehaviour, IDamageable, IHealing, IHealth<int>
    {
        [SerializeField, Required, MinValue(0)] private int _maxValue = 3;

        private const int MinValue = 0;
        private int _currentValue;
        private bool _isEnableDamage = true;
        private ISpecification<int> _damageSpecification;
        private ISpecification<int> _healthSpecification;
        private bool _isInitialized;

        public event Action Damaged;

        public event Action<int> Changed;

        public event Action Died;

        public event Action Healed;

        public int MaxValue => _maxValue;

        [ShowInInspector, ReadOnly]
        public int CurrentValue
        {
            get => _currentValue;

            private set
            {
                _currentValue = value;

                if (!_healthSpecification.IsSatisfiedBy(_currentValue))
                    _currentValue = MinValue;

                if (_currentValue > MaxValue)
                    _currentValue = MaxValue;
            }
        }

        public float Percent => CurrentValue / (float)MaxValue;

        public bool IsDied => CurrentValue == 0;

        private void Awake()
        {
            ValidateInitialization();
        }

        public void Initialize()
        {
            _damageSpecification = new IntGreatOrEqualZeroSpecification();
            _healthSpecification = new IntGreatOrEqualZeroSpecification();

            CurrentValue = _maxValue;
            _isInitialized = true;
        }

        public void TakeDamage(int damage)
        {
            if (IsDied)
                return;

            if (_isEnableDamage == false)
                return;

            if (_damageSpecification.IsSatisfiedBy(damage) == false)
                throw new Exception("Damage value does not match specification");

            CurrentValue -= damage;

            Changed?.Invoke(CurrentValue);
            Damaged?.Invoke();

            if (CurrentValue == 0)
                Died?.Invoke();
        }

        public void Heal(int value)
        {
            if (_damageSpecification.IsSatisfiedBy(value) == false)
                throw new Exception("Heal value does not match specification");

            CurrentValue += value;

            Changed?.Invoke(CurrentValue);
            Healed?.Invoke();
        }

        public void DisableDamage()
        {
            _isEnableDamage = true;
        }

        public void EnableDamage()
        {
            _isEnableDamage = false;
        }

        private void ValidateInitialization()
        {
            if (_isInitialized == false)
                throw new Exception($"{nameof(Health)} is not initialized");
        }
    }
}
